import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import areas from '@/areas.js'
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



function loadAreas() {
  return mapMany(areas, area => {
    return mapMany(area.items, page => {
      return page.items.map(item => {
        return {
          path: `/${page.name.toLowerCase()}/${item.name.toLowerCase()}`,
          name: `${page.name}_${item.name}`.toLowerCase(),
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
  name=name.toLowerCase()
  return childs.find(x => x.name == name).component
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
        ...childs
      ]
    },
    load('/login'),
  ]
})