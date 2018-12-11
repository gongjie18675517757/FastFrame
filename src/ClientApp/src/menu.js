export default [{
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
    }
  ]
}, {
  title: '内容管理',
  items: [{
      icon: 'list',
      title: '文章管理',
      path: '/article/list',
      permission: 'Article'
    },
    {
      icon: 'merge_type',
      title: '类别管理',
      path: '/articlecategory/list',
      permission: 'ArticleCategory'
    },
    {
      icon: 'perm_media',
      title: '媒体库',
      path: '/meidia/list',
      permission: 'Meidia'
    }
  ]
}]