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
    return f.Hide != 'list' && f.Hide != 'all' && (!f.Name.endsWith('Id') || f.Relate)
  }).map(f => {
    return {
      ...f,
      ModuleName
    }
  })

  for (const col of columns) {
    if (col.Relate) {
      let relate = col.Relate
      let strut = await getModuleStrut(col.Relate)
      col.Relate = strut.RelateFields
      try {
        if (strut.RelateFields.length > 0) {
          col.IsLink = true
        }
      } catch (error) {
        console.log(error, relate, strut.RelateFields)
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
    return f.Hide != 'form' && f.Hide != 'all'
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