import {makeMangePermission} from './comm'
export default {
    title: '基础资料',
    icon: 'settings',
    items: [
        {
            title: '系统设置',
            path: '/Setting',
            permission: 'Setting.Get',
            childPermission: ['Setting.Update']
        },
        {
            title: '用户管理',
            path: '/user/list',
            permission: 'User.List',
            childPermission: [...makeMangePermission('User', ['ToogleAdminIdentity', 'ToogleDisabled'])]
        },
        {
            title: '登录记录',
            path: '/LoginLog/list',
            permission: 'LoginLog.List',
            childPermission: ['LoginLog.SetTokenFailure']
        },
        {
            title: '部门管理',
            path: '/dept/list',
            permission: 'Dept.List',
            childPermission: [...makeMangePermission('Dept')]
        },
        {
            title: '数据字典',
            path: '/enumItem/list',
            permission: 'EnumItem.List',
            childPermission: [...makeMangePermission('EnumItem')]
        },
        {
            title: '单据编号规则',
            path: '/NumberOption/list',
            permission: 'NumberOption.List',
            childPermission: [...makeMangePermission('NumberOption')]
        },
        {
            title: '角色管理',
            path: '/role/list',
            permission: 'Role.List',
            childPermission: [...makeMangePermission('Role')]
        },
        {
            title: '通知管理',
            path: '/notify/list',
            permission: 'Notify.List',
            childPermission: [...makeMangePermission('Notify')]
        },
        {
            title: '企业管理',
            path: '/tenant/list',
            permission: 'Tenant.List'
        },
        {
            title: '资源库',
            path: '/meidia/list',
            permission: 'Meidia.List',
            childPermission: [...makeMangePermission('Meidia')]
        },
        {
            title: '图标库',
            path: '/icons',
        },
    ]
}