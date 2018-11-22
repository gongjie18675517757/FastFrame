import $http from '@/http'
import {
  lock
} from '@/utils'

let permission = []

async function initPermission() {
  let localLock = await lock('Permissions') 
  try {
    if (permission.length == 0) {
      let items = await $http.get('/api/Permission/Permissions') 
      let loadTree = parentId => {
        return items.filter(r => r.Parent_Id == parentId).map(r => {
          return {
            ...r,
            children: loadTree(r.Id)
          }
        })
      }
      permission = loadTree(null)
    } 
  } catch (error) {

  } finally {
    localLock.freed()
  } 
}

/**
 * 验证按钮权限
 * @param {*} moduleName 
 * @param {*} name 
 */
export async function existBtn(moduleName, name) {
  await initPermission()
  let modulePermission = permission.find(r => r.EnCode == moduleName)
  if (modulePermission && modulePermission.children.find(r => r.EnCode == name)) {
    return true
  } else {
    return false
  }
}

/**
 * 验证模块权限
 * @param {*} moduleName 
 */
export async function existModult(moduleName) {
  await initPermission()
  return !!permission.find(r => r.EnCode == moduleName)
}