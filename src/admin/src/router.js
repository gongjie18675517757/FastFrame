import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import areas from '@/areas.js'
import store from '@/store.js'
import {
  existBtn
} from '@/permission.js'
import {

  mapMany,
  sleep
} from './utils'

Vue.use(Router)



function loadAreas() {
  return mapMany(areas, area => {
    return mapMany(area.items, page => {
      return page.items.map(item => {
        return {
          path: `/${page.name.toLowerCase()}/${item.name.toLowerCase()}`,
          name: `${page.name}_${item.name}`.toLowerCase(),
          meta: {
            title: `${item.name == 'List'?'':'新增'}${page.title}${item.name == 'List'?'列表':''}`,
            keepAlive: item.name == 'List',
            moduleName: page.name,
            pageName: item.name
          },
          component: () =>
            import(`@/views/${area.name}/${page.name}/${item.name}.vue`)
        }
      })
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
    name: 'home',
    component: Home,
    children: [{
        path: '/',
        name: 'index',
        meta: {
          title: '首页',
          keepAlive: true
        },
        component: () =>
          import(`./views/Index/Index.vue`)
      },
      {
        path: '/about',
        name: 'about',
        meta: {
          title: '关于页',
          keepAlive: true
        },
        component: () =>
          import(`./views/About.vue`)
      },
      {
        path: '/chat',
        name: 'chat',
        meta: {
          title: '在线聊天',
          keepAlive: true
        },
        component: () =>
          import(`./views/Chat/ChatLayout.vue`)
      },
      {
        path: '/map',
        name: 'map',
        meta: {
          title: '地图演示',
          keepAlive: true
        },
        component: () =>
          import(`./views/Map.vue`)
      },
      {
        path: '/tenantCenter',
        name: 'tenantCenter',
        meta: {
          title: '企业中心',
          keepAlive: false
        },
        component: () =>
          import(`./views/TenantCenter.vue`)
      },
      {
        path: '/userCenter',
        name: 'userCenter',
        meta: {
          title: '个人中心',
          keepAlive: false
        },
        component: () =>
          import(`./views/UserCenter.vue`)
      },
      ...childs
    ]
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
      import(`./views/Login.vue`)
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
      import(`./views/Regist.vue`)
  },
  {
    path: '/405',
    name: '405',
    meta: {
      title: '权限不足',
      keepAlive: false,
    },
    component: () =>
      import(`./views/405.vue`)
  },
]

let router = new Router({
  routes
})



const getSiteName = async () => {
  let name = ''
  while (true) {
    name = store.state.tenant.FullName
    if (!name) {
      await sleep(100)
    } else {
      break;
    }
  }
  return name
}
const existLogin = () => {
  return new Promise((resolve, reject) => {
    if (store.state.currUser.Id)
      return resolve();

    setTimeout(() => {
      if (!store.state.currUser.Id)
        return reject()
      else
        return resolve();
    }, 500);
  })
}

router.beforeEach(async (to, from, next) => {
  let name = await getSiteName()
  window.document.title = to.meta.title ? `${to.meta.title}-${name}` : name;
  if (to.meta.allowAnonymous) {
    next();
    return
  }

  if (!to.meta.moduleName) {
    next();
    return
  }

  let exists = await existBtn(to.meta.moduleName, to.meta.pageName)
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
    await existLogin()
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