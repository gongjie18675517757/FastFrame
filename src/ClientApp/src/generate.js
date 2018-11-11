import {
  lock
} from '@/utils'
import $http from '@/http'
let moduleStruts = {}

/**
 * 获取模块结构
 * @param {*} name
 */
async function getModuleStrut(name = '') {
  name = name.toLowerCase()
  let localLock = await lock(name)
  try {
    if (moduleStruts.hasOwnProperty(name)) {
      return moduleStruts[name]
    } else {
      let request = await $http.get(`/api/common/moduleStruts/${name}`)
      moduleStruts[name] = request
      return request
    }
  } finally {
    localLock.freed()
  }
}

/**
 * 获取模块表单
 * @param {*} name
 */
async function getDefaultModel(name = '') {
  let {
    FieldInfoStruts
  } = await getModuleStrut(name)
  let model = {}
  for (const field of FieldInfoStruts) {
    model[field.Name] = field.DefaultValue
  }
  return model
}

/**
 * 获取列表列
 * @param {*} name
 */
async function getColumns(name = '') {
  let {
    FieldInfoStruts,
    Name: ModuleName
  } = await getModuleStrut(name)
  return FieldInfoStruts.filter(f => {
    return f.Hide != 'list' && f.Hide != 'all' && !f.Name.endsWith('Id')
  }).map(f => {
    return {
      ...f,
      ModuleName
    }
  })
}

async function getFormItems(name = '') {
  let {
    FieldInfoStruts,
    Name: ModuleName
  } = await getModuleStrut(name)
  return FieldInfoStruts.filter(f => {
    return f.Hide != 'form' && f.Hide != 'all' && !f.Name.endsWith('Id')
  }).map(f => {
    return {
      ...f,
      ModuleName
    }
  })
}

export {
  getModuleStrut,
  getDefaultModel,
  getColumns,
  getFormItems
}

export default {
  getModuleStrut,
  getDefaultModel,
  getColumns,
  getFormItems
}