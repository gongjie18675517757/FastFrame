export function makeStandardRouteItems(title) {
    return [
        {
            key: 'List',
            title: `${title}列表`,
        },
        {
            key: 'Add',
            title: `${title}`,
        },
        {
            key: ':id',
            title: `${title}`,
            path: 'Add',
            permission: 'Get'
        },
    ]
}