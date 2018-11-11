import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import {
  changeChar,
  mapMany
} from './utils'
Vue.use(Router)


let load = (path, url) => {
  path = path || ''
  let paths = path.split('/')
  let name = paths[paths.length - 1].toLowerCase()
  paths = paths.map(a => changeChar(a, x => x.toUpperCase()))
  path = paths.join('/');

  return {
    path: url || `/${name}`,
    name: name,
    component: () =>
      import(`./views${path}.vue`)
  }
}

let areas = {
  Basis: {
    User: {
      List: '列表',
      Add: '添加'
    }
  }
}

function loadAreas() {
  return mapMany(Object.keys(areas), function (areaKey) {
    let area = areas[areaKey]
    return mapMany(Object.keys(area), function (moduleName) {
      let module = area[moduleName]
      return Object.keys(module).map(function (name) {
        return {
          path: `/${moduleName.toLowerCase()}/${name.toLowerCase()}`,
          name: `${moduleName}_${name}`.toLowerCase(),
          component: () =>
            import(`./views/${moduleName}/${name}.vue`)
        }
      })
    })
  })
}  

export default new Router({
  routes: [{
      path: '/',
      name: 'home',
      component: Home,
      children: [
        load('/index/index', '/'),
        load('/about'),
        load('/userCenter'),
        load('/page1/page1-1'),
        load('/page1/page1-2'),
        ...loadAreas()
      ]
    },
    load('/login'),
  ]
})