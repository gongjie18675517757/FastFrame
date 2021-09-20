import Vue from 'vue'
import $http from '../httpClient'
import { throttle as _throttle, debounce as _debounce } from 'lodash'
import { getIcon } from './fileIcons'
import { getUploadPath } from '../config';

export const getIconFunc = getIcon;

export const throttle = _throttle;

export const debounce = _debounce;

/**
 * 事件总线
 */
export const eventBus = new Vue()


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
 * 上传文件
 * @param {*} files 
 */
export function postFiles(files = [], pars) {
    pars = {
        onProgress: () => { },
        ...pars || {},
    }
    let formData = new FormData()
    for (let index = 0; index < files.length; index++) {
        const file = files[index];
        formData.append(file.name, file);
    }
    return $http.post(getUploadPath(), formData, {
        method: 'post',
        headers: {
            'Content-Type': 'multipart/form-data'
        },
        transformRequest: [function (data) {
            return data
        }],
        onUploadProgress: function (e) {
            var percentage = Math.round((e.loaded * 100) / e.total) || 0;
            if (percentage <= 100) {
                pars.onProgress(percentage)
            }
        }
    })
}

/**
 * 上传
 * @param {*} param0 
 */
export function upload(pars) {
    let {
        accept = "",
        onProgress = () => { },
        verifyFileFunc = () => true,
    } = (pars || {})
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
            if (!verifyFileFunc(files)) {
                reject()
            }
            postFiles(files, { onProgress }).then(function (resp) {
                resolve(resp)
            }).catch(reject)
        }
        input.click()
    })

}


/**
 * 保存文件
 * @param {*} res 
 * @param {*} fileName 
 */
export async function saveFile(resFunc, fileName, type = 'application/octet-stream') {
    let res = await resFunc()
    const blob = new Blob([res], {
        type
    });

    if ("download" in document.createElement("a")) {
        const elink = document.createElement("a");
        elink.download = fileName;
        elink.style.display = "none";
        elink.href = URL.createObjectURL(blob);
        document.body.appendChild(elink);
        elink.click();
        URL.revokeObjectURL(elink.href); // 释放URL 对象
        document.body.removeChild(elink);
    } else {
        // IE10+下载
        navigator.msSaveBlob(blob, fileName);
    }
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

/**
 * 
 * @param {*} arr 
 * @param {*} keySelector 
 * @param {*} valueSelector 
 */
export function createObject(arr = [], keySelector = v => v.Key, valueSelector = v => v.Value) {
    let obj = {

    };
    for (const r of arr) {
        obj[keySelector(r)] = valueSelector(r)
    }

    return obj;
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

/**
 * 跳过指定个数
 * @param {*} arr 
 * @param {*} skipCount 
 */
export function skip(arr = [], skipCount = 0) {
    return arr.filter((_val, index) => index + 1 > skipCount)
}

/**
 * 返回指定个数
 * @param {*} arr 
 * @param {*} takeCount 
 */
export function take(arr = [], takeCount = 0) {
    return arr.filter((_val, index) => index < takeCount)
}


/**
 * 生成GUID
 * @returns {string}
 */
export function guid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
        var r = (Math.random() * 16) | 0,
            v = c == "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
    });
}

/**
 * 格式化请求数据
 * @param {*} data 
 */
export function fmtRequestPars(key, value) {
    if (!value) {
        return undefined;
    }
    else if (Array.isArray(value)) {
        return value.length > 0 ? value : undefined
    } else {
        return value
    }
}
