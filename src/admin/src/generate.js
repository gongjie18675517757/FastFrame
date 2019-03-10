import {
  lock
} from '@/utils'
import $http from '@/http'
import rules from '@/rules'
let moduleStruts = {}

/**
 * 获取模块结构
 * @param {*} name
 */
export async function getModuleStrut(name = '') {
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
export async function getDefaultModel(name = '') {
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
export async function getColumns(name = '') {
  let {
    FieldInfoStruts,
    RelateFields,
    Name: ModuleName
  } = await getModuleStrut(name)
  let columns = FieldInfoStruts.filter(f => {
    return f.Hide != 'List' && f.Hide != 'All' && (!f.Name.endsWith('Id') || f.Relate)
  }).map(f => {
    return {
      ...f,
      ModuleName
    }
  })

  if (RelateFields.length > 0) {
    let col = columns.find(r => r.Name == RelateFields[0])
    col.IsLink = true
  }

  for (const col of columns) {
    if (col.Relate) {
      let {
        RelateFields: fields
      } = await getModuleStrut(col.Relate)
      col.Relate = {
        ModuleName: col.Relate,
        RelateFields: fields
      }
    }
  }

  return columns
}

/**
 * 获取表单列表
 * @param {*} name
 */
export async function getFormItems(name = '') {
  let {
    FieldInfoStruts,
    Name: ModuleName
  } = await getModuleStrut(name)
  return FieldInfoStruts.filter(f => {
    return f.Hide != 'Form' && f.Hide != 'All'
  }).map(f => {
    return {
      ...f,
      ModuleName
    }
  })
}

/**
 * 获取表单验证列表
 * @param {*} name
 */
export async function getRules(name = '') {
  let {
    FieldInfoStruts
  } = await getModuleStrut(name)
  let obj = {}
  for (const {
      Rules,
      Description,
      Name
    } of FieldInfoStruts) {
    let evalRules = Rules.map(r => {
      if (rules[r.RuleName]) {
        return rules[r.RuleName](Description, ...r.RulePars)
      }
    }).filter(r => !!r)
    if (Name.includes('Email')) evalRules.push(rules.email.call())
    if (Name.includes('Phone')) evalRules.push(rules.phone.call())

    obj[Name] = evalRules
  }

  return obj
}

/**
 * 是否包含管理字段
 * @param {*} name 
 */
export async function hasManage(name) {
  let {
    HasManage
  } = await getModuleStrut(name)
  return HasManage
}