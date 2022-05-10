import { makeStandardRouteItems } from './utils'

export default {
    areaName: 'Proxy',
    title: '代理服务',
    items: {
        ProxyClient: [
            ...makeStandardRouteItems('内网穿透隧道')
        ],
         
    }
}
