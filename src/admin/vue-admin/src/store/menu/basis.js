export default {
    title: '功能列表',
    items: [
        {
            title: '基础资料',
            icon: 'settings',
            items: [
                {
                    title: '用户管理',
                    path: '/user/list',
                    permission: 'User'
                },
                {
                    title: '登录记录',
                    path: '/LoginLog/list',
                    permission: 'LoginLog'
                },
                {
                    title: '部门管理',
                    path: '/dept/list',
                    permission: 'Dept'
                },
                {
                    title: '权限管理',
                    path: '/permission/list',
                    permission: 'Permission'
                },
                {
                    title: '数据字典',
                    path: '/enumItem/list',
                    permission: 'EnumItem'
                }, 
                {
                    title: '单据编号规则',
                    path: '/NumberOption/list',
                    permission: 'NumberOption'
                },
                {
                    title: '角色管理',
                    path: '/role/list',
                    permission: 'Role'
                },
                {
                    title: '通知管理',
                    path: '/notify/list',
                    permission: 'Notify'
                },
                {
                    title: '企业管理',
                    path: '/tenant/list',
                    permission: 'Tenant'
                },
                {
                    title: '资源库',
                    path: '/meidia/list',
                    permission: 'Meidia'
                },
                {
                    title: '图标库',
                    path: '/icons',
                },
            ]
        }
    ]
}