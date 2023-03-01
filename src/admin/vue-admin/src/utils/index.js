import Vue from 'vue'
import $http from '../httpClient'
import { throttle as _throttle, debounce as _debounce, get, set } from 'lodash'
import { getIcon } from './fileIcons'
import { getUploadPath } from '../config';
import _queryBuild from './queryBuild'
import JSEncrypt from 'jsencrypt';

export const getIconFunc = getIcon;

export const throttle = _throttle;

export const debounce = _debounce;

export const queryBuild = _queryBuild;

/**
 * 事件总线
 */
export const eventBus = new Vue()

/**
 * 格式化文件大小
 * @param {Number} v 
 */
export function formatter_file_size(v) {
    if (!v)
        return `0KB`
    else if (v < 1024)
        return `${(v / 1024).toFixed(2)}KB`
    else if (v < 1024 * 1024)
        return `${(v / 1024).toFixed(2)}KB`
    else if (v < 1024 * 1024 * 1024)
        return `${(v / 1024 / 1024).toFixed(2)}MB`
    else
        return `${(v / 1024 / 1024 / 1024).toFixed(2)}GB`
}

/**
 * 求和
 * @param {Array} arr 
 * @param {Function} func 
 */
export function sum(arr, func = v => v) {
    return arr.map(func).reduce((a, b) => a + b);
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
     
    fn = fn || function (v) {
        if (Array.isArray(v))
            return v
        return [v];
    }
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
                return;
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
    return get(obj, prop)
}


/**
 * 根据路径设置值
 * @param {*} obj 
 * @param {*} prop 
 */
export function setValue(obj = {}, prop = "", val = null) {
    return set(obj, prop, val)
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
    if (key != 'value')
        return value;
    if (!value) {
        return null;
    }
    else if (Array.isArray(value)) {
        return value.length > 0 ? value.join(',') : null
    } else {
        return value
    }
}

export function toHexString(text) {
    return encodeUtf8(text).map(v => v.toString(16)).join('')
}

export function fromHexString(text) {
    if (!text)
        return text;

    if (!/^[0-9a-f]+$/.test(text))
        return text;

    var length = text.length / 2;
    var bytes = new Array(length);

    for (var i = 0; i < length; i++) {
        var s = [text[i * 2], text[i * 2 + 1]].join('')
        var b = parseInt(s, 16);

        bytes[i] = b;
    }

    return decodeUtf8(bytes);
}

export function encodeUtf8(text) {
    const code = encodeURIComponent(text);
    const bytes = [];
    for (var i = 0; i < code.length; i++) {
        const c = code.charAt(i);
        if (c === '%') {
            const hex = code.charAt(i + 1) + code.charAt(i + 2);
            const hexVal = parseInt(hex, 16);
            bytes.push(hexVal);
            i += 2;
        } else bytes.push(c.charCodeAt(0));
    }
    return bytes;
}

export function decodeUtf8(bytes) {
    var encoded = "";
    for (var i = 0; i < bytes.length; i++) {
        encoded += '%' + bytes[i].toString(16);
    }
    return decodeURIComponent(encoded);
}

/**
 * 生成Vue的上下文件,使用适配函数式组件与普通组件
 * @param {*} param0 
 * @returns 
 */
export function makeVueContext({
    inject = []
}) {
    return {
        listeners: this.$listeners,
        props: this.$props,
        data: {
            attrs: this.$attrs
        },
        injections: createObject(inject, v => v, v => this[v])
    }
}

/**
 * RSA加密
 * @param {String} input_string 要加密的字符串 
 * @param {String} publicKey 公钥，为空时，从后台获取
 */
export async function RSAEncrypt(input_string, publicKey) {
    const encrypt = new JSEncrypt();
    const PUBLIC_KEY = publicKey || await $http.get('/api/common/getPublicKey')
    encrypt.setPublicKey(PUBLIC_KEY);
    return encrypt.encrypt(input_string);
}

/**
 * 将HTML转换为text
 * @param {*} html 
 * @returns 
 */
export function convertHtmlToText(html) {
    if (!html)
        return html;
    const el = document.createElement('div');
    el.innerHTML = html;
    const txt = el.textContent;
    return txt;
}
