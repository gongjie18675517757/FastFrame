import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import {
  changeChar
} from './utils'
Vue.use(Router)


const load = (path = "") => {
  let paths = path.split('/')
  let name = paths[paths.length - 1].toLowerCase()
  paths = paths.map(a => changeChar(a, x => x.toUpperCase()))
  path = paths.join('/');

  return {
    path: `/${name}`,
    name: name,
    component: () =>
      import(`./views${path}.vue`)
  }
}
export default new Router({
  routes: [{
      path: '/',
      name: 'home',
      component: Home,
      children: [
        load('/about'),
        load('/page1/page1-1'),
        load('/page1/page1-2'),
      ]
    },
    load('/login'),
  ]
})