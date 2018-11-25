import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import store from './store'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import '@/hubs/messageHub' 
import $http from '@/http.js'
import {eventBus} from './utils';

import Btn from '@/components/Btn.vue'


Vue.config.productionTip = false
Vue.prototype.$eventBus = eventBus
Vue.prototype.$http = $http
Vue.component('a-btn', Btn)

new Vue({
  router,
  store,
  render: h => h(App), 
}).$mount('#app')