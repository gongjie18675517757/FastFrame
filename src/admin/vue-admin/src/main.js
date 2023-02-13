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
import colors from "vuetify/es5/util/colors";

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
    window.$message=this.$message;
    this.$http('/api/Tenant/GetCurrent').then(v => {
      this.$store.commit({
        type: 'setTenant',
        info: v
      })
    })
  }, 
  mounted() {
    let val = this.$store.state.themeColor
    this.$vuetify.theme.themes.light.primary = colors[val].base;
    this.$vuetify.theme.themes.dark.primary = colors[val].base; 
  }
}).$mount('#app')

 

/**
 * 1,检查所有方案的返回，不允许返回全对象
 * 1.1 改选查询语句,和DTO对象,用SQL生成DTO,用简单类型代替复杂类型
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
 * 16,增加业务流程模板,配合动态表单,审批流程也支持关联动态表单/固定表单
 * 17,枚举增加名称配置
 * 18,导出时可选列
 * 19,列表可以设置列宽(在右侧面板中)，手动调整列宽度，列顺序，显示、隐藏,更新名称
 * 19.1,表单也可以定义宽度，顺序，名称等
 * 20,表单页增加多TAB，加折叠
 * 21,资料库增加文件权限设置（右侧弹窗）
 * 23,资源表增加权限、区分个人资源、公共资源
 * 24,增加响应pdf功能
 * 25,优化表单结构，取消viewmodel模式
 * 26,增加暂存功能
 * 27,集成邮箱发送，增加将当前表单给管理员功能
 * 28,列表页优化，增加搜索条，按钮下移一行
 * 29,每个列表增加动态统计、列表、图表，动态选择统计列，汇总列，统计方式 
 * 32,通讯录+给全体用户发送实时消息/确认消息的功能，统计在线用户，给指定用户发送实时消息
 * 33,缓存管理界面化
 * 34,所有字符串常量化
 * 35,前端身份优化
 * 36,限制同时只能有一个POST/PUT请求
 * 37,集成邮件模板
 * 38，批量删除（删除后联动页面更新）
 * 39，待办事项
 * 40,弹出的表单框复用formCore.js
 * 41,公共的导入方法
 * 42,删除时增加依赖检测 
 * 44,权限常量化
 * 45,增加字段权限，数据权限
 * 46,单据归属部门逻辑填充
 * 47,流程绑定表单
 * 48,增加设置表单功能
 * 49，首页增加服务器占用，接口请求次数等
 * 50，表单调整，取消复杂对象
 * 
 * 代理功能加上配置,加上tcp,udp
 * 增加记录代理日志
 * 升级VUE3.0,UI库也升级了
 * 启用Nullable
 * 升级typescript 
 * 生成的表名调整 
 * 微信扫码登录
 * 文件上传改为流式大文件上传 
 * 自动事务
 * 自动生成codeGenerate
 * 富文本组件要升级,富文本的内容单独存放[blobProvider]
 * 树状层级管理还要优化，保存时要验证循环引用
 * 数据字段改为使用int做为key,并使用int存储
 * 所有数据库字段的枚举改为int存储
 * 角色的权限可以继承
 * 启动时依赖收集
 * 系统设置功能调整
 * 文件下载改为原生下载
 * 树装选择器
 */