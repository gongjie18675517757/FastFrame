export default  {
    title: '基础资料',
    icon: 'settings',
    items: [
        {
            title: '用户管理',
            path: '/user/list',
            permission: 'User.List'
        },
        {
            title: '登录记录',
            path: '/LoginLog/list',
            permission: 'LoginLog.List'
        },
        {
            title: '部门管理',
            path: '/dept/list',
            permission: 'Dept.List'
        },
        {
            title: '权限管理',
            path: '/permission/list',
            permission: 'Permission.List'
        },
        {
            title: '数据字典',
            path: '/enumItem/list',
            permission: 'EnumItem.List'
        }, 
        {
            title: '单据编号规则',
            path: '/NumberOption/list',
            permission: 'NumberOption.List'
        },
        {
            title: '角色管理',
            path: '/role/list',
            permission: 'Role.List'
        },
        {
            title: '通知管理',
            path: '/notify/list',
            permission: 'Notify.List'
        },
        {
            title: '企业管理',
            path: '/tenant/list',
            permission: 'Tenant.List'
        },
        {
            title: '资源库',
            path: '/meidia/list',
            permission: 'Meidia.List'
        },
        {
            title: '图标库',
            path: '/icons',
        },
    ]
}