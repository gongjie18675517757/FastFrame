function required(value) {
  return !!value || '必填项'
}

function email(value) {
  const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return !value || pattern.test(value) || '不是有效的Email值';
}

function length(min, max) {
  return v => !v || (v && v.length >= min && v.length < max) || `长度不正确,(${min}-${max})`
}

function phone(value) {
  const pattern = /^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/;
  return !value || pattern.test(value) || '不是有效的手机号码';
}


export default {
  required,
  email,
  length,
  phone
}