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
    items: [
      generateItem('User', '员工'),
      generateItem('Dept', '部门'),
      generateItem('Permission', '权限'),
      generateItem('Role', '角色'),
      generateItem('Employee', '员工'),
      generateItem('Menu', '菜单'),
      generateItem('Tenant', '组织'),
      generateItem('QueryProgram', '查询方案'),
    ]
  },
  {
    name: 'Cms',
    title: '内容管理',
    items: [
      generateItem('ArticleCategory', '文章类别'),
      generateItem('Article', '文章'),
      generateItem('Meidia', '媒体'),
    ]
  }
]