import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import areas from './areas.js'
import store from '../store'

import {
  mapMany,
} from '../utils'

Vue.use(Router)

let props = (route) => ({
  ...route.query,
  ...route.params
})


function loadAreas() {
  return mapMany(areas, ({
    name,
    items
  }) => {
    return mapMany(Object.entries(items), ([key, val]) => {
      return [{
        path: `/${key.toLowerCase()}/list`,
        name: `${key}_List`.toLowerCase(),
        meta: {
          title: `${val}列表`,
          keepAlive: false,
          moduleName: key,
          pageName: 'List'
        },
        component: () =>
          import(`@/views/${name}/${key}/List.vue`)
      },
      {
        path: `/${key.toLowerCase()}/add`,
        name: `${key}_Add`.toLowerCase(),
        meta: {
          title: `添加${val}`,
          keepAlive: false,
          moduleName: key,
          pageName: 'Add'
        },
        component: () =>
          import(`@/views/${name}/${key}/Add.vue`)
      },
      {
        path: `/${key.toLowerCase()}/:id`,
        name: `${key}`.toLowerCase(),
        meta: {
          title: `修改${val}`,
          keepAlive: false,
          moduleName: key,
          pageName: 'Get'
        },
        component: () =>
          import(`@/views/${name}/${key}/Add.vue`)
      }
      ]
    })
  })
}

let childs = loadAreas()

/**
 * 获取组件
 * @param {*} name 
 */
export function getComponent(name) {
  name = name.toLowerCase()
  let item = childs.find(x => x.name == name)
  return item == null ? null : item.component
}

let routes = [{
  path: '/',

  component: Home,
  children: [{
    path: '/',
    name: 'home',
    meta: {
      title: '首页',
      keepAlive: true
    },
    component: () =>
      import(`@/views/Index/Index.vue`)
  },
  {
    path: '/about',
    name: 'about',
    meta: {
      title: '关于页',
      keepAlive: true
    },
    props: true,
    component: () =>
      import(`@/views/About.vue`)
  },
  {
    path: '/icons',
    name: 'icons',
    meta: {
      title: '所有图标',
      keepAlive: true
    },
    props: true,
    component: () =>
      import(`@/views/Icons.vue`)
  },
  {
    path: '/map',
    name: 'map',
    meta: {
      title: '地图演示',
      keepAlive: true
    },
    component: () =>
      import(`@/views/Map.vue`)
  },
  {
    path: '/tenantCenter',
    name: 'tenantCenter',
    meta: {
      title: '企业中心',
      keepAlive: false
    },
    component: () =>
      import(`@/views/TenantCenter.vue`)
  },
  {
    path: '/userCenter',
    name: 'userCenter',
    meta: {
      title: '个人中心',
      keepAlive: false
    },
    component: () =>
      import(`@/views/UserCenter.vue`)
  },
  {
    path: '/notifyCenter',
    name: 'notifyCenter',
    meta: {
      title: '通知中心',
      keepAlive: false
    },
    component: () =>
      import(`@/views/Index/NotifyCenter`)
  },
  ...childs
  ].map(v => ({
    ...v,
    props
  }))
},
{
  path: '/login',
  name: 'login',
  meta: {
    title: '登陆页',
    keepAlive: false,
    allowAnonymous: true,
  },
  component: () =>
    import(`@/views/Login.vue`)
},
{
  path: '/regist',
  name: 'regist',
  meta: {
    title: '注册页',
    keepAlive: false,
    allowAnonymous: true,
  },
  component: () =>
    import(`@/views/Regist.vue`)
},
{
  path: '/405',
  name: '405',
  meta: {
    title: '权限不足',
    keepAlive: false,
  },
  component: () =>
    import(`@/views/405.vue`)
},
]

let router = new Router({
  routes
})

router.beforeEach(async (to, from, nextFunc) => {
  let next = store.state.singlePageMode ? nextFunc : () => {
    let route = routes[0].children.find(v => v.name == to.name)
    if (route) {
      let page = {
        fullPath: to.fullPath,
        title: to.meta.title,
        pars: {
          ...to.query,
          ...to.params
        },
        component: route.component
      }
      store.dispatch('addPage', page)
    }
    nextFunc()
  }
  store.getters.getSiteNameAsync().then(({
    FullName = ""
  }) => {
    window.document.title = to.meta.title ? `${to.meta.title}-${FullName}` : FullName;
  })

  if (to.meta.allowAnonymous) {
    next();
    return
  }

  if (!to.meta.moduleName) {
    next();
    return
  }

  let exists = await store.getters.existsPermissionAsync(to.meta.moduleName, to.meta.pageName)
  if (exists) {
    next()
  } else {
    next({
      path: '/405',
      query: {
        redirect: to.fullPath
      }
    })
  }

  try {
    await store.getters.existsLoginAsync()
  } catch (error) {
    next({
      path: '/login',
      query: {
        redirect: to.fullPath
      }
    })
    return
  }
})

router.afterEach((to) => {
  if (!to.meta.allowAnonymous) {
    store.state.lastUrl = to.fullPath
  }
})

export default router


