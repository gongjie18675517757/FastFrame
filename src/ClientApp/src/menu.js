export default [{
  title: '基础信息',
  items: [{
      icon: 'account_box',
      title: '用户管理',
      path: '/user/list',
      permission: 'User'
    },
    {
      icon: 'group',
      title: '部门管理',
      path: '/dept/list',
      permission: 'Dept'
    },
    {
      icon: 'mdi-coin',
      title: '权限管理',
      path: '/permission/list',
      permission: 'Permission'
    },
    {
      icon: 'mdi-coin',
      title: '角色管理',
      path: '/role/list',
      permission: 'Role'
    }
  ]
}]