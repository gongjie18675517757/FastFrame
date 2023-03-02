import SparkMD5 from 'spark-md5'
import { getBidFileUploadPath } from '../../config';
import $http from '../../httpClient'
import { sleep, sum } from '../../utils';

/**
 * 默认分片大小
 */
const SLICE_SIZE = 1024 * 1024;

/**
 * 计算要文件要分隔为几份
 * @param {File} file 文件
 * @param {Number} size 一份文件大小
 */
export function calc_file_splcie_count(file, size = SLICE_SIZE) {
    const fileSize = file.size;

    if (fileSize < size)
        return 1;

    return Math.ceil(fileSize / SLICE_SIZE);
}


/**
 * 根据文件索引号,返回切割后的文件
 * @param {File} file 要切割的文件
 * @param {Number} file_index  文件索引,从0开始
 * @param {Number} size 分片大小
 * @returns 
 */
export function get_slice_file_by_index(file, file_index, size = SLICE_SIZE) {
    const start = size * file_index;
    const end = start + size
    const blob = file.slice(start, end);

    return new File([blob], file.name, {
        lastModified: file.lastModified,
        name: file.name,
        size: blob.size,
        type: file.type,
        webkitRelativePath: file.webkitRelativePath
    })
}

/**
 * 上传文件元数据
 * @param {*} f 
 * @param {*} size 
 */
export async function upload_file_metadata(file, md5, size = SLICE_SIZE) {
    const metadata = {
        name: file.name,
        size: file.size,
        contentType: file.type,
        totalChunkFiles: calc_file_splcie_count(file, size),
        FileMD5:md5
    }

    /**
     * 支持的分片大小应该由后端定义并返回,这里先由前端定义
     */
    const res = await $http.post(getBidFileUploadPath(), metadata);
    return res
}


/**
 * 大文件上传之参数
 */
export class upload_bid_file_config {
    constructor() {

        /**
         * 重试次数和延时
         */
        this.retry_delays = [0, 3000, 5000, 10000, 20000];

        /**
         * 同时发起上传的线程数量
         */
        this.total_theard_count = 5;
    }
    /**
     * 同时发起上传的线程数量
     * @param {Number} total_size  总大小
     * @param {Number} upload_size 已上传的大小
     */
    on_progress(total_size, upload_size) {
        return {
            total_size,
            upload_size
        };
    }
}





/**
 * 定义上传状态
 */
export const upload_state = {
    /**
     * 准备中
     */
    ready_ing: Symbol(),

    /**
     * 上传中
     */
    upload_ing: Symbol(),

    /**
     * 暂停中
     */
    pause_ing: Symbol(),

    /**
     * 已取消
     */
    canceled: Symbol(),

    /**
     * 已完成
     */
    finished: Symbol(),

    /**
     * 已失败
     */
    fail: Symbol()
};

/**
 * 计算文件的md5
 * @param {File} file 
 * @param {Number} size 
 */
export function calc_file_md5(file, chunkSize = SLICE_SIZE) {
    return new Promise(resolve => {
        const fileReader = new FileReader();
        const md5 = new SparkMD5();
        let index = 0;
        const loadFile = () => {
            const slice = file.slice(index, index + chunkSize);
            fileReader.readAsBinaryString(slice);
        }
        loadFile();
        fileReader.onload = e => {
            md5.appendBinary(e.target.result);
            if (index < file.size) {
                index += chunkSize;
                loadFile();
            } else {
                resolve(md5.end())
            }
        };
    })
}


/**
 * 上传大文件
 * @param {File} file 
 * @param {upload_bid_file_config} config 
 * @param {Number} size 
 */
export async function upload_bid_file(file, config, size = SLICE_SIZE) {
    config = {
        ...new upload_bid_file_config(),
        ...(config || {})
    };

    /**
     * 文件的上传状态
     */
    let file_state = upload_state.ready_ing;

    /**
     * 总共的包数
     */
    const total = calc_file_splcie_count(file, size);



    /**
     * 存放分割好的文件
     */
    const upload_item_arr = new Array(total).fill(null).map((_, index) => {
        const _file = get_slice_file_by_index(file, index, size);
        return ({
            index,
            state: upload_state.ready_ing,
            upload_size: 0,
            file: _file,
            total_size: file.size
        })
    });


    /**
     * 上传文件的任务
     * @param {String} file_id 文件id
     * @param {String} task_index 任务索引
     * @param {Function} on_change  状态更新事件回调
     */
    async function start_upload_task(file_id, task_index, on_change) {
        /**
         * 只要状态是上传中,则不停的从数组中取一个分片出来上传
         */
        while (file_state == upload_state.upload_ing) {
            const upload_item_index = upload_item_arr.findIndex(v => v.state == upload_state.ready_ing);
            if (upload_item_index == -1)
                break;

            /**
             * 要上传的项
             */
            const upload_item = upload_item_arr[upload_item_index];

            /**
             * 更新状态
             */
            upload_item.state = upload_state.upload_ing;
            upload_item.upload_size = 0;
            on_change(upload_item)

            /**
             * 组装提交内容
             */
            const splice_file = upload_item.file;
            const form_data = new FormData();
            form_data.append('file_id', file_id);
            form_data.append('chunk_file', splice_file);
            form_data.append('file_index', upload_item_index);


            /**
             * 记录重试次数
             * 从-1开始,
             * 第一次-1,不用等待
             * 第二次为0,则需要按指定的秒数指定
             */
            let retry_count = -1
            while (retry_count <= config.retry_delays.length - 1) {
                /**
                 * 重试时等待指定的毫秒数
                 */
                const retry_delay = config.retry_delays[retry_count];
                if (retry_delay)
                    await sleep(retry_delay);

                try {
                    await $http.post(getBidFileUploadPath(file_id), form_data, {
                        method: 'post',
                        headers: {
                            'Content-Type': 'multipart/form-data'
                        },
                        transformRequest: [function (data) {
                            return data
                        }],
                        onUploadProgress: function (e) {
                            /**
                             * 更新上传进度
                             */
                            if (e && e.loaded) {
                                upload_item.upload_size = e.loaded;
                                on_change(upload_item)
                            }
                        }
                    })
                    window.console.log(`file_id:${file_id},分片索引:#${upload_item.index},task_index:${task_index},第${retry_count + 2}次上传成功`)

                    /**
                     * 上传成功后,退出死循环
                     */
                    break;
                } catch (error) {
                    window.console.error(error)
                    window.console.error(`file_id:${file_id},分片索引:#${upload_item.index},task_index:${task_index},第${retry_count + 2}次上传失败`)


                    /**
                     * 说明到达重试次数后,还是失败
                     */
                    if (retry_count == config.retry_delays.length - 1) {
                        throw error;
                    }
                }

                retry_count++;
            }
        }

        window.console.log(`file_id:${file_id},task_index:${task_index},全部处理完成`)
    }

    return {
        /**
         * 文件的md5
         */
        md5: null,

        /**
         * 文件上传结果
         */
        file_model: null,

        /**
         * 开始上传
         */
        async start() {
            try {
                /**
                 * 开始计算md5
                 */
                if (!this.md5) {
                    this.md5 = await calc_file_md5(file, size);
                }

                /**
                 * 开始上传元数据
                 */
                if (!this.file_model) {
                    this.file_model = await upload_file_metadata(file, this.md5, size);
                }

                /**
                 * 后端未命中缓存
                 */
                if (this.file_model.HasUpload) {
                    file_state = upload_state.upload_ing;

                    /**
                     * 重置文件分片状态
                     */
                    for (const upload_item of upload_item_arr) {
                        if (upload_item.state != upload_state.finished) {
                            upload_item.state = upload_state.ready_ing;
                        }
                    }

                    /**
                     * 多个上传任务同时上传
                     */
                    const tasks = new Array(config.total_theard_count)
                        .fill(null)
                        .map((_, index) => start_upload_task(this.file_model.Id, index, function () {
                            const total = file.size;
                            const upload_size = sum(upload_item_arr, v => v.upload_size)
                            config.on_progress(total, upload_size)
                        }));
                    await Promise.all(tasks)
                }



                file_state = upload_state.finished;

                /**
                 * 全部上传完成,返回文件
                 */
                return this.file_model;
            } catch (error) {
                file_state = upload_state.fail;
                throw error;
            }
        },
        file_state: {
            get() {
                return file_state
            }
        }
        // /**
        //  * 暂停上传
        //  */
        // pause() {
        //     file_state = upload_state.upload_ing;
        // },
        // /**
        //  * 取消上传
        //  */
        // cancel() {
        //     file_state = upload_state.canceled;
        // },
        // /**
        //  * 继续上传
        //  */
        // re_start() {
        //     return this.start()
        // }
    }
}