import '@babel/polyfill'
import Vue from 'vue'
import vuetify  from  './plugins/vuetify'
import App from './App.vue'
import router from './router'
import store from './store'
import 'roboto-fontface/css/roboto/roboto-fontface.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import '@/hubs/messageHub'
import $http from '@/http.js'
import Alert from '@/components/Message/Alert.vue'
import Confirm from '@/components/Message/Confirm.vue'
import Prompt from '@/components/Message/Prompt.vue'
import {
  eventBus,
  showDialog
} from './utils';

import VueQuillEditor from 'vue-quill-editor'
// require styles 引入样式
import 'quill/dist/quill.core.css'
import 'quill/dist/quill.snow.css'
import 'quill/dist/quill.bubble.css'
Vue.use(VueQuillEditor)

import Btn from '@/components/Btn.vue'

Vue.prototype.$message = {
  alert(title = "", content = "") {
    return showDialog(Alert, {
      title,
      content
    })
  },
  confirm(title = "", content = "") {
    return showDialog(Confirm, {
      title,
      content
    })
  },
  prompt() {
    return showDialog(Prompt, ...arguments)
  },
  dialog(component, pars) {
    return showDialog(component, pars)
  }
}
Vue.config.productionTip = false
Vue.prototype.$eventBus = eventBus
Vue.prototype.$http = $http
Vue.component('a-btn', Btn)

 

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App),
  async created() {
    let tenant = await $http('/api/Tenant/GetCurrent')
    this.$store.commit({
      type: 'setTenant',
      info: tenant
    })
  }
}).$mount('#app')