import $http from './httpClient'
/**
 * 必填验证
 * @param {*} fieldDescription 
 */
function required(fieldDescription = '') {
  return function (value) {
    return !!value || `${fieldDescription}是必填的`
  }
}
/**
 * 邮箱验证
 * @param {*} fieldDescription 
 */
function email(fieldDescription = '邮箱地址无效') {
  const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return function (value) {
    return !value || pattern.test(value) || fieldDescription;
  }
}

/**
 * 长度验证
 * @param {*} fieldDescription 
 * @param {*} min 
 * @param {*} max 
 */
function stringLength(fieldDescription = '', min, max) {
  return function (value) {
    return !value || (value && value.length >= min && value.length < max) || `${fieldDescription}长度要求在[${min},${max}]之间`
  }
}

/**
 * 手机号验证
 * @param {*} fieldDescription 
 */
function phone(fieldDescription = '手机号码无效') {
  const pattern = /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;
  return function (value) {
    return !value || pattern.test(value) || fieldDescription;
  }
}

/**
 * 唯一验证
 * @param {*} fieldDescription 
 * @param {*} moduleName 
 * @param {*} name 
 */
function unique(fieldDescription, moduleName, name) {
  return function (value) {
    if (!value)
      return true

    let postData = {
      Id: this.Id,
      ModuleName: moduleName,
      KeyValues: [{
        Key: name,
        Value: value
      }]
    }
    return new Promise((resolve, reject) => {
      $http.post(`/api/${moduleName}/VerififyUnique`, postData).then(data => {
        if (data) {
          resolve(`${fieldDescription}重复!`)
        } else {
          resolve(true)
        }
      }).catch(reject)
    })
  }
}



export default {
  required,
  email,
  stringLength,
  phone,
  unique
}