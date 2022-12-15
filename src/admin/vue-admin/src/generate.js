import {
  lock
} from './utils'
import $http from './httpClient'
import rules from './rules'
import store from './store'

const moduleStruts = {}
const cache_dic = {
  hasNumberModules: {
    is_init: false,
    values: []
  },
  hasCheckModules: {
    is_init: false,
    values: []
  },
}

/**
 * 需要编码的模块
 * @returns 
 */
export async function getHasCheckModules() {
  let localLock = await lock(cache_dic.hasCheckModules);
  try {
    if (!cache_dic.hasCheckModules.is_init) {
      const res = await $http.get(`/api/common/HaveNumberModuleList`);
      cache_dic.hasCheckModules.is_init = true;
      cache_dic.hasCheckModules.values = res;
    }

    return cache_dic.hasCheckModules.values;

  } finally {
    localLock.freed()
  }
}

/**
 * 需要编码的模块
 * @returns 
 */
export async function getHasNumberModules() {
  let localLock = await lock(cache_dic.hasNumberModules);
  try {
    if (!cache_dic.hasNumberModules.is_init) {
      const res = await $http.get(`/api/common/HaveNumberModuleList`);
      cache_dic.hasNumberModules.is_init = true;
      cache_dic.hasNumberModules.values = res;
    }

    return cache_dic.hasNumberModules.values;

  } finally {
    localLock.freed()
  }
}


/**
 * 获取模块结构
 * @param {*} name
 */
export async function getModuleStrut(name = '') {
  name = name.toLowerCase()
  let localLock = await lock(name)
  try {
    if (!moduleStruts[name]) {
      moduleStruts[name] = await $http.get(`/api/common/moduleStruts/${name}`)
    }

    return moduleStruts[name]
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
    Form,
    HasManage,
  } = await getModuleStrut(name)
  let model = Form

  if (HasManage) {
    model.Create_User_Id = store.state.currUser.Id
    model.Create_User_Value = store.state.currUser.Name
    model.Modify_User_Id = store.state.currUser.Id
    model.Modify_User_Value = store.state.currUser.Id 
  }
  return {
    ...model
  }
}

/**
 * 获取列表列
 * @param {*} name
 */
export async function getColumns(name = '') {
  let {
    FieldInfoStruts,
    Name: ModuleName,
  } = await getModuleStrut(name)
  let columns = FieldInfoStruts
    .filter(v => v.Name != "Id") 
    .filter(f => {
      return (f.Hide != 'List' && f.Hide != 'All')
    }).map(f => {
      return {
        ...f,
        ModuleName,
        sortable: true
      }
    })

  return columns.filter(r => !['Create_User_Value', 'CreateTime', 'Modify_User_Value', 'ModifyTime'].includes(r.Name))
}

/**
 * 获取枚举列表
 * @param {*} tbName 
 * @param {*} filedName 
 * @returns {Array}
 */
export async function getEnumValues(tbName, filedName) {
  let {
    FieldInfoStruts
  } = await getModuleStrut(tbName)

  return FieldInfoStruts.filter(v => v.Name == filedName).map(v => v.EnumValues).find(v => v) || []
}

/**
 * 获取表单列表
 * @param {*} name
 */
export async function getModelObjectItems(name = '') {
  let {
    FieldInfoStruts,
    Name: ModuleName
  } = await getModuleStrut(name)
  return FieldInfoStruts.filter(f => {
    return f.Hide != 'Form' && f.Hide != 'All' && f.Name != 'Id'
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

/**
 * 获取查询字段
 * @param {*} name 
 */
export async function getQueryOptions(columns) {
  let arr = []
  for (const {
    Type,
    Name,
    Description,
    Relate,
    EnumValues,
    EnumItemInfo,
    sortable
  } of columns) {

    if (Name.includes("Password") || Relate == "Resource" || !sortable)
      continue;

    else if (Number.isInteger(EnumItemInfo)) {
      arr.push({
        Description,
        Name,
        EnumItemInfo,
        compare: 'in',
        Type: 'Array',
        value: []
      })
    } else if (EnumValues && EnumValues.length > 0 || typeof EnumValues == 'function') {
      arr.push({
        Description,
        Name,
        EnumValues,
        compare: 'in',
        Type: 'Array',
        value: []
      })
    } else if (Type == 'Boolean') {
      arr.push({
        Description,
        Name,
        EnumValues: [
          { Key: 'true', Value: '是' },
          { Key: 'false', Value: '否' },
        ],
        compare: '==',
        Type
      })
    } else if (['Int32', 'Decimal', 'DateTime','Int64'].includes(Type)) {
      arr.push({
        Description: `${Description}起`,
        Name,
        Type,
        compare: '>='
      })
      arr.push({
        Description: `${Description}止`,
        Name,
        Type,
        compare: '<='
      })
    } else if (!Name.endsWith('_Id') && (Type == 'String' || !Type)) {
      arr.push({
        Type,
        Description,
        Name,
        compare: '$'
      })
    } else {
      console.error(Name)
    }
  }

  arr = arr.map(r => {
    return {
      ...r,
      value: r.value || null
    }
  });

  return arr;
}


