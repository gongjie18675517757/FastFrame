export default [{
    title: '基础信息',
    items: [{
        icon: 'account_box',
        title: '用户管理',
        path: '/user/list',
      },
      {
        icon: 'group',
        title: '部门管理',
        path: '/dept/list',
      }
    ]
  },
  {
    title: 'group3',
    items: [{
      icon: 'widgets',
      title: '子页面',
      items: [{
          path: '/page1-1',
          title: '关于页'
        },
        {
          path: '/page1-2',
          title: '关于页'
        }
      ]
    }]
  }
]