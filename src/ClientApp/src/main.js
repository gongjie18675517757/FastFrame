import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import store from './store'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import './hubs/chatHub'
import $http from '@/http.js'
import utils from './utils';



Vue.config.productionTip = false
Vue.prototype.$eventBus = utils.eventBus
Vue.prototype.$http = $http

new Vue({
  router,
  store,
  render: h => h(App),
  async created() {
    try {
      let request = await $http.get('/api/account/GetCurrent')
      this.$store.commit('login', request)
    } catch (error) {
      if (this.$route.fullpath != '/login') {
        this.$router.push({
          path: '/login',
          query: {
            redirect: this.$route.fullpath
          }
        })
      }
    }
  }
}).$mount('#app')