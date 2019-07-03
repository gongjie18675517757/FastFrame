export default [{
    title: '存货管理',
    items: [{
      icon: 'account_box',
      title: '用户管理', 
      permission: 'User',
      items: [{
        icon: 'account_box',
        title: '用户管理',
        path: '/user/list',
        permission: 'User'
      }]
    }]
  },
  {
    title: '基础信息',
    items: [{
        icon: 'account_box',
        title: '用户管理',
        path: '/user/list',
        permission: 'User'
      },
      {
        icon: 'fa fa-male',
        title: '部门管理',
        path: '/dept/list',
        permission: 'Dept'
      },
      {
        icon: 'people',
        title: '权限管理',
        path: '/permission/list',
        permission: 'Permission'
      },
      {
        icon: 'people_outline',
        title: '角色管理',
        path: '/role/list',
        permission: 'Role'
      },
      {
        icon: 'notifications',
        title: '通知管理',
        path: '/notify/list',
        permission: 'Notify'
      },
      {
        icon: 'view_agenda',
        title: '下级企业',
        path: '/tenant/list',
        permission: 'Tenant'
      },
      {
        icon: 'perm_media',
        title: '资源库',
        path: '/meidia/list',
        permission: 'Meidia'
      }
    ]
  },

]