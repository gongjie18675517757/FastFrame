import {makeMangePermission} from './comm'
export default {
    title: '工作流',
    icon:  'mdi-clipboard-flow-outline',
    items: [
        {
            title: '流程设计',
            path: '/WorkFlow/list',
            permission: 'WorkFlow.List',
            childPermission: [...makeMangePermission('WorkFlow')]
        }
    ]
}