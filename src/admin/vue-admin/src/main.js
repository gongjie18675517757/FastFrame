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



/**
 * 1,前端文件访问抽像化
 * 2,数据字典增加树，增加编码
 * 3,增加动态表单
 * 4,流程可视化
 * 5,角色权限调整
 * --6,菜单后台化
 * 7,增加系统管理，滑动验证码背景可配置
 * 8,菜单配置化,注册路由时，增加权限声明
 * 9,页面按钮权限定义修改
 * 10,增加第三方应用管理，增加单点登陆
 * 11,修改审计，增加审计日志
 * 12,页面加载增加加载条
 */