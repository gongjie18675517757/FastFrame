import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import store from './store'
import router from './router'
import '@babel/polyfill'
import './hubs'
import './httpClient'
import './components/Message'
import './components/common' 
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
 * 1,检查所有方案的返回，不允许返回全对象
 * 2,数据字典增加树，增加编码，增加状态，并不可删除
 * 3,增加动态表单
 * 4,流程可视化
 * 5,角色权限调整
 * 6,验证权限，增加管理员修改密码，增加侧面板弹窗（比如搜索条件弹窗）
 * 7,增加系统管理，滑动验证码背景可配置
 * 8,菜单配置化（可由用户编辑）,注册路由时，增加权限声明
 * 9,页面按钮权限定义修改
 * 10,增加第三方应用管理，增加单点登陆
 * 11,修改审计，增加审计日志
 * 12,页面加载增加加载条
 * 13,多租户功能优化：顶级租户无ID，租户ID取消阴影属性
 * 14，升级到.net5/6
 * 15，缓存CacheUserMapKey优化，实时消息推送优化（如增加CHROME的消息弹窗），增加响应用户操作：如确认，选择
 * 16,增加业务流程模板,配合动态表单,审批流程也支持关联动态表单/固定表单
 * 17,枚举增加名称配置
 * 18,导出时可选列
 * 19,列表可以设置列宽
 * 20,表单页增加多TAB
 * 21,资料库增加文件权限设置（右侧弹窗）
 * 22,增加后台异步任务功能
 */