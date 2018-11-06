import Vue from 'vue'

const eventBus = new Vue()

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


const changeChar = (str = '', map = (x) => x, filter = (item, index) => index == 0) => {
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

const generateArray = (length) => Array.from(new Array(length).keys());

const sleep = (millisecond = 0) => {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve()
        }, millisecond);
    })
}


export {
    changeChar,
    generateArray,
    sleep,
    eventBus,
    alert
}
export default {
    changeChar,
    generateArray,
    sleep,
    eventBus,
    alert
}