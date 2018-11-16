import Vue from 'vue'
import $http from '@/http.js' 

/**
 * 事件总线
 */
const eventBus = new Vue()

/**
 * 提示框
 */
const alert = {
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
function changeChar(str = '', map = (x) => x, filter = (item, index) => index == 0) {
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
function generateArray(length) {
    Array.from(new Array(length).keys());
}
/**
 * 暂停
 * @param {*} millisecond 
 */
function sleep(millisecond = 0) {
    return new Promise((resolve, reject) => {
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
async function lock(lockObj = {}) {
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
function mapMany(arr, fn) {
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
function upload({
    accept = "",
    onProgress = () => {}
}) {
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
                formData.append('files[]', files[index]);
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
                }
            }).then(function (resp) {
                resolve(resp)
            }).catch(reject)
        }
        input.click()
    })

}



export {
    changeChar,
    generateArray,
    sleep,
    eventBus,
    alert,
    upload,
    mapMany,
    lock
}

export default {
    changeChar,
    generateArray,
    sleep,
    eventBus,
    alert,
    upload,
    mapMany,
    lock
}