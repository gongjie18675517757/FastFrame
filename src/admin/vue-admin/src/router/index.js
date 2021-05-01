import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import areas from './areas'
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
    areaName,
    items
  }) => {
    return mapMany(Object.entries(items), ([key, areaPageList = []]) => {
      return areaPageList.map(page => {
        return {
          path: `/${key}/${page.key}`.toLowerCase(),
          name: `${key}_${page.key}`.toLowerCase(),
          meta: {
            title: page.title,
            keepAlive: false,
            moduleName: key,
            permission: `${key}.${page.permission || page.key}`,
            pagePath: `@/views/${areaName}/${key}/${page.path || page.key}.vue`,
          },

          component: () =>
            import(`@/views/${areaName}/${key}/${page.path || page.key}.vue`)
        }
      })
    })
  })
}

let childs = loadAreas()

export const pages = [{
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
{
  path: '/flowDesign',
  name: 'flowDesign',
  meta: {
    title: '流程设计',
    keepAlive: false
  },
  component: () =>
    import(`@/views/Flow/Design/Add.vue`)
},
...childs
].map(v => ({
  ...v,
  props
}))

let routes = [{
  path: '/',
  component: Home,
  children: pages
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
    let route = pages.find(v => v.name == to.name)
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

  try {
    /**
     * 验证登录身份
     */
    await store.dispatch('existsIdentity')
  } catch (error) {
    nextFunc({
      path: '/login',
      query: {
        redirect: to.fullPath
      }
    })
    return
  }

  if (!to.meta.moduleName) {
    next();
    return
  }

  let exists = await store.dispatch('existsPermissionAsync', to.meta.permission);

  if (exists) {
    next()
  } else {
    nextFunc({
      path: '/405',
      query: {
        redirect: to.fullPath
      }
    })
  }
})

router.afterEach((to) => {
  if (!to.meta.allowAnonymous) {
    store.state.lastUrl = to.fullPath
  }
})

export default router


