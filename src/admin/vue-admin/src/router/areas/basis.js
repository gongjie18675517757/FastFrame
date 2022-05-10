import { makeStandardRouteItems } from './utils'

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
            {
                key: 'TeeView',
                title: `数据字典`,
                permission: 'List'
            },
            ...makeStandardRouteItems('数据字典'),
        ],
        LoginLog: [
            {
                key: 'List',
                title: `登录身份列表`,
            }
        ],
        ApiRequestLog: [
            {
                key: 'List',
                title: `接口访问记录`,
            }
        ],
        NumberOption: [
            ...makeStandardRouteItems('单据编号规则')
        ]
    }
}
