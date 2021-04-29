import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import store from './store'
import router from './router'
import '@babel/polyfill'
import './hubs'
import './httpClient'
import './components/Message'
import './components/PermissionBtn' 
import '@mdi/font/css/materialdesignicons.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
// import 'roboto-fontface/css/roboto/roboto-fontface.css'

import {
  eventBus,
} from './utils';
 

Vue.prototype.$eventBus = eventBus
Vue.config.productionTip = false

new Vue({
  vuetify,
  store,
  router,
  render: h => h(App),
  created() {
    this.$http('/api/Tenant/GetCurrent').then(v => {
      this.$store.commit({
        type: 'setTenant',
        info: v
      })
    })
  }
}).$mount('#app')
