import {makeMangePermission} from './comm'
export default {
    title: '代理服务',
    icon:  'mdi-format-text-wrapping-overflow',
    items: [
        {
            title: '内网穿透隧道',
            path: '/ProxyClient/list',
            permission: 'ProxyClient.List',
            childPermission: [...makeMangePermission('ProxyClient')]
        }
    ]
}