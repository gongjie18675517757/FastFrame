import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import areas from '@/areas.js'
import store from '@/store.js'
import {
  existBtn
} from '@/permission.js'
import {
  changeChar,
  mapMany
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
            title: `${page.title}列表`,
            keepAlive: true,
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
  return childs.find(x => x.name == name).component
}

let router = new Router({
  routes: [{
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
            keepAlive: false
          },
          component: () =>
            import(`./views/About.vue`)
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
})


const siteName = 'XX管理系统'
const existLogin = () => {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (!store.state.currUser.Id)
        return reject()
      else
        return resolve();
    }, 100);
  })
}

router.beforeEach(async (to, from, next) => {
  window.document.title = to.meta.title ? `${to.meta.title}-${siteName}` : siteName;
  if (to.meta.allowAnonymous) {
    next();
    return
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
})



export default router