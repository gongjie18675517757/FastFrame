import Vue from 'vue'
import $http from '@/http.js'
import store from '@/store.js'
import {
    getComponent
} from '@/router.js'

/**
 * 事件总线
 */
export const eventBus = new Vue()

/**
 * 提示框
 */
export const alert = {
    error(msg) {
        eventBus.$emit('alert', {
            type: 'error',
            msg
        })
    },
    success(msg) {
        eventBus.$emit('alert', {
            type: 'success',
            msg
        })
    },
    warning(msg) {
        eventBus.$emit('alert', {
            type: 'warning',
            msg
        })
    },
    info(msg) {
        eventBus.$emit('alert', {
            type: 'info',
            msg
        })
    }
}

/**
 * 改变首字母大小写
 * @param {*} str 
 * @param {*} map 
 * @param {*} filter 
 */
export function changeChar(str = '', map = (x) => x, filter = (item, index) => index == 0) {
    if (str == '')
        return ''
    let charArr = str.split('')
    for (let index = 0; index < charArr.length; index++) {
        let char = charArr[index];
        if (filter(char, index))
            charArr[index] = map(charArr[index])
    }
    return charArr.join('');
}

/**
 * 生成数组 
 * @param {*} length 
 */
export function generateArray(length) {
    return [...Array.from(new Array(length).keys())];
}

/**
 * 暂停
 * @param {*} millisecond 
 */
export function sleep(millisecond = 0) {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve()
        }, millisecond);
    })
}

let lockSet = new Set()

/**
 * 互斥锁
 * @param {*} lockObj 
 */
export async function lock(lockObj = {}) {
    while (lockSet.has(lockObj)) {
        await sleep(100)
    }
    lockSet.add(lockObj)
    return {
        freed() {
            lockSet.delete(lockObj)
        }
    }
}


/**
 * 映射
 * @param {*} arr 
 * @param {*} fn 
 */
export function mapMany(arr, fn) {
    let array = []
    for (const item of arr) {
        let brr = fn(item)
        for (const item2 of brr) {
            array.push(item2)
        }
    }
    return array
}

/**
 * 上传
 * @param {*} param0 
 */
export function upload({
    accept = "",
    onProgress = () => {}
} = {}) {
    let el = document.getElementById('uploadInput')
    if (el) {
        el.parentElement.removeChild(el)
    }
    let input = document.createElement('input')
    if (accept) {
        input.accept = accept
    }
    input.id = 'uploadInput'
    input.type = 'file'
    input.style.display = 'none'
    document.body.appendChild(input)

    return new Promise((resolve, reject) => {
        input.onchange = function (e) {
            let files = e.target.files
            let formData = new FormData()
            for (let index = 0; index < files.length; index++) {
                const file = files[index];
                formData.append('files[]', file);
            }
            $http.post('/api/resource/post', formData, {
                method: 'post',
                headers: {
                    'Content-Type': 'multipart/form-data'
                },
                transformRequest: [function (data) {
                    return data
                }],
                onUploadProgress: function (e) {
                    var percentage = Math.round((e.loaded * 100) / e.total) || 0;
                    if (percentage < 100) {
                        onProgress(percentage)
                    }
                    window.console.log(percentage)
                }
            }).then(function (resp) {
                resolve(resp)
            }).catch(reject)
        }
        input.click()
    })

}

/**
 * 弹出框 
 */
export function showDialog(component, pars = {}) {
    let key = new Date().getTime()
    if (typeof component == 'string')
        component = getComponent(component)
    let hide = () => {
        store.commit({
            type: 'hideDialog',
            key: key
        })
    }
    return new Promise((resolve, reject) => {
        let success = (e) => {
            resolve(e)
            hide()
        }
        let close = () => {
            hide()
            reject(1)
        }

        let render = {
            data() {
                return {
                    visible: true,
                    refresh: false,
                }
            },
            provide() {
                return {
                    reload: this.reload
                }
            },
            methods: {
                reload() {
                    this.refresh = true
                    this.$nextTick(function () {
                        this.refresh = false
                    })
                }
            },
            render(h) {
                let child = []
                if (!this.refresh) {
                    let props = {
                        ...pars,
                        pars,
                        success,
                        close,
                        isDialog: true
                    }
                    child = [
                        h(component, {
                            props,
                            on: {
                                success,
                                close
                            }
                        })
                    ]
                }
                return h(
                    'v-dialog', {
                        props: {
                            // persistent: true,
                            scrollable: true,
                            value: this.visible,
                            ...pars,
                        },
                        on: {
                            input: val => {
                                if (!val) {
                                    close()
                                }
                            }
                        }
                    },
                    child
                )
            }
        }

        store.commit({
            type: 'showDialog',
            render,
            key
        })
    })

}

/**
 * 取数组中随机值
 * @param {*} arr 
 */
export const randomElement = (arr = []) => {
    return arr[Math.floor(Math.random() * arr.length)];
};

/**
 * 根据路径访问对象值
 * @param {*} obj 
 * @param {*} prop 
 */
export function getValue(obj = {}, prop = "") {
    if (!prop)
        throw new Error('属性名称配置不正确')
    let paths = prop.split('.')

    let temp = obj
    for (let index = 0; index < paths.length - 1; index++) {
        const path = paths[index];
        temp = temp[path] || {}
    }
    return temp[paths[paths.length - 1]]
}

/**
 * 根据路径设置值
 * @param {*} obj 
 * @param {*} prop 
 */
export function setValue(obj = {}, prop = "", val = null) {
    if (!prop)
        throw new Error('属性名称配置不正确')
    let paths = prop.split('.')
    let temp = obj
    for (let index = 0; index < paths.length - 1; index++) {
        const path = paths[index];
        temp = temp[path] || {}
    }
    temp[paths[paths.length - 1]] = val

}

/**
 * 聚合
 * @param {*} arr 
 * @param {*} keySelector 
 */
export function groupBy(arr = [], keySelector = r => r, keyCompare = (a, b) => a == b) {
    let brr = []
    for (const item of arr) {
        let key = keySelector(item)
        let brrItem = brr.find(r => keyCompare(r.key, key))
        if (!brrItem) {
            brrItem = {
                key,
                values: []
            }
            brr.push(brrItem);
        }

        brrItem.values.push(item)
    }

    return brr;
}

/**
 * 选择展开
 */
export function selectMany(arr = [], selector = r => [r]) {
    let brr = []
    for (const item of arr) {
        let values = selector(item)
        brr.push(...values)
    }

    return brr;
}


export function exportExcel(fileName, headers = [], rows = []) {
    let cells = {}
    let ref = 'A1:' + String.fromCharCode(headers.length + 1 + 65) + (rows.length + 1)
    for (let index = 0; index < headers.length; index++) {
        let header = headers[index];
        let rowName = String.fromCharCode(65 + index);
        cells[`${rowName}1`] = {
            v: header.Description,
            t: 's'
        }
    }
    for (let r = 0; r < rows.length; r++) {
        let row = rows[r];
        for (let c = 0; c < headers.length; c++) {
            let header = headers[c];
            let rowName = String.fromCharCode(65 + c);
            let colIndex = r + 2;
            cells[`${rowName}${colIndex}`] = {
                v: header.fn(row, header) || ''
            }
        }
    }

    cells['!ref'] = ref

    let wb = {
        SheetNames: ["sheet1"],
        Sheets: {
            "sheet1": cells
        },
        SSF: {
            "0": "General",
            "1": "0",
            "2": "0.00",
            "3": "#,##0",
            "4": "#,##0.00",
            "9": "0%",
            "10": "0.00%",
            "11": "0.00E+00",
            "12": "# ?/?",
            "13": "# ??/??",
            "14": "m/d/yy",
            "15": "d-mmm-yy",
            "16": "d-mmm",
            "17": "mmm-yy",
            "18": "h:mm AM/PM",
            "19": "h:mm:ss AM/PM",
            "20": "h:mm",
            "21": "h:mm:ss",
            "22": "m/d/yy h:mm",
            "37": "#,##0 ;(#,##0)",
            "38": "#,##0 ;[Red](#,##0)",
            "39": "#,##0.00;(#,##0.00)",
            "40": "#,##0.00;[Red](#,##0.00)",
            "45": "mm:ss",
            "46": "[h]:mm:ss",
            "47": "mmss.0",
            "48": "##0.0E+0",
            "49": "@",
            "56": '"上午/下午 "hh"時"mm"分"ss"秒 "',
            "65535": "General"
        },
        Props: {
            SheetNames: ["sheet1"],
            Worksheets: 1,
            Application: "SheetJS"
        }
    };
    XLSX.writeFile(wb, fileName + ".xlsx");
}

/**
 * 去重
 * @param {*} arr  要去重的数组
 * @param {*} keyFunc  键选取
 * @param {*} mergefunc  合并
 */
export function distinct(arr = [], keyFunc = v => v, mergefunc = (a, b) => b) {
    let dic = {}
    for (const item of arr) {
        let key = keyFunc(item)
        if (dic[key]) {
            dic[key] = mergefunc(dic[key], item)
        } else {
            dic[key] = item
        }
    }
    return Object.values(dic)
}