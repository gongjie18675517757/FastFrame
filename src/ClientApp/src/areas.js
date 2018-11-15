let pages = [{
    name: 'List',
    title: '列表'
  },
  {
    name: 'Add',
    title: '添加'
  }
]

let generateItem = (name, title) => {
  return {
    name,
    title,
    items: [...pages]
  }
}

export default [{
  name: 'Basis',
  title: '基础设置',
  items: [{
      name: 'User',
      title: '用户',
      items: [...pages]
    },
    {
      name: 'Dept',
      title: '部门',
      items: [...pages]
    },
    generateItem('Employee', '员工'),
    generateItem('Menu', '菜单'),
    generateItem('Organize', '组织'),
    generateItem('QueryProgram', '查询方案'),
  ]
}]