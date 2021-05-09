/**
 * 生成基础管理权限
 * @param {*} moduleName 
 * @param {*} moreNameArr 
 * @returns 
 */
export function makeMangePermission(moduleName, moreNameArr = []) {
    return ['Delete', 'Get', 'Add', 'Update', ...moreNameArr]
        .filter(v => !!v)
        .map(v => `${moduleName}.${v}`)
}