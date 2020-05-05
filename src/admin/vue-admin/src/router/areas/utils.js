export function makeStandardRouteItems(title) {
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