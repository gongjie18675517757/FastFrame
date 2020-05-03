function makeStandardRouteItems(title) {
    return [
        {
            key: 'List',
            title: `${title}列表`,
        },
        {
            key: 'Add',
            title: `添加${title}`,
        },
        {
            key: ':id',
            title: `查看${title}`,
            path: 'Add',
            permission: 'Get'
        },
    ]
}

export default {
    areaName: 'Basis',
    title: '基础设置',
    items: {
        User: [
            ...makeStandardRouteItems('用户')
        ],
        Dept: [
            ...makeStandardRouteItems('部门')
        ],
        Permission: [
            {
                key: 'List',
                title: `权限列表`,
            }
        ],
        Role: [
            ...makeStandardRouteItems('角色')
        ],
        Tenant: [
            ...makeStandardRouteItems('组织')
        ],
        Meidia: [
            {
                key: 'List',
                title: `资源列表`,
            }
        ],
        Notify: [
            ...makeStandardRouteItems('通知')
        ],
        EnumItem: [
            ...makeStandardRouteItems('数据字典')
        ],
        LoginLog: [
            {
                key: 'List',
                title: `登录身份列表`,
            }
        ],
        NumberOption: [
            ...makeStandardRouteItems('单据编号规则')
        ]
    }
}
