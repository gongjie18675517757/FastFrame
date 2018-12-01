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
import {
  eventBus
} from './utils';

import VueQuillEditor from 'vue-quill-editor'
// require styles 引入样式
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
Vue.use(VueQuillEditor)

import Btn from '@/components/Btn.vue'


Vue.config.productionTip = false
Vue.prototype.$eventBus = eventBus
Vue.prototype.$http = $http
Vue.component('a-btn', Btn)

new Vue({
  router,
  store,
  render: h => h(App),
  async created() {
    let tenant = await $http('/api/Tenant/GetCurrent')
    this.$store.commit({
      type: 'setTenant',
      info: tenant
    })
  }
}).$mount('#app')