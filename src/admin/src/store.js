import Vue from 'vue'
import Vuex from 'vuex'
import $http from '@/http'
import {
  sleep,
  eventBus
} from '@/utils'
Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    /**
     * 是否使用单页签模式
     */
    singlePageMode: false,

    /**
     * 多页内容
     */
    pages: [{
      fullPath: '/',
      title: '首页',
      pars: {},
      component: () =>
        import(`@/views/Index/Index.vue`)
    }],

    /**
     * 当前页
     */
    currPageFullPath: '/',

    /**
     * 当前登陆用户
     */
    currUser: {},

    /**
     * 当前企业
     */
    tenant: {},

    /**
     * 是否已加载权限
     */
    isLoadPermission: false,

    /**
     * 权限列表
     */
    permissionList: [],

    /**
     * 左边菜单栏是否显示
     */
    leftDrawer: true,

    /**
     * 右边菜单栏是否显示
     */
    rightDrawer: false,
    /**
     * 使用弹窗模式
     */
    dialogMode: false,

    /**
     * 地图模式[百度/谷歌]
     */
    mapMode: '',

    /**
     * 通知列表
     */
    notifys: [],

    /**
     * 弹窗列表
     */
    dialogs: [],

    /**
     * 好友消息
     */
    friendMsgs: [],

    /**
     * 所有数据字典
     */
    enumItemValues: {}
  },
  mutations: {
    login(state, payload) {
      state.currUser = payload
    },
    logout(state) {
      state.isLoadPermission = false
      state.currUser = null
    },
    setPermission(state, payload) {
      state.permissionList = payload
    },
    toggleLeftDrawer(state, payload) {
      state.leftDrawer = payload.value
    },
    toggleRightDrawer(state, payload) {
      state.rightDrawer = payload.value
    },
    showDialog(state, payload) {
      state.dialogs.push({
        key: payload.key || new Date().getTime(),
        render: payload.render
      })
    },
    hideDialog(state, payload) {
      let index = state.dialogs.findIndex(x => x.key == payload.key || x.render == payload.render)
      if (index != -1)
        state.dialogs.splice(index, 1)
    },
    setTenant(state, payload) {
      state.tenant = payload.info
    },
    setMapMode(state, payload) {
      state.mapMode = payload.mode
      window.localStorage.setItem('mapMode', payload.mode)
    },
    addFriendMsg(state, payload) {
      state.friendMsgs.push(payload.FriendMsg)
    },
    addEnumItem(state, payload) {
      state.enumItemValues[payload.Key].push(payload)
    }
  },
  actions: {
    /**
     * 添加页
     * @param {*} param0 
     * @param {*} page 
     */
    addPage({
      state
    }, page) {
      if (!state.pages.find(v => v.fullPath == page.fullPath)) {
        state.pages.push({
          ...page,
          closeable: true
        });
      }
      state.currPageFullPath = page.fullPath;
    },
    /**
     * 关闭页
     * @param {*} param0 
     * @param {*} fullPath 
     */
    closePage({
      state
    }, fullPath) {
      let index = state.pages.findIndex(v => v.fullPath == fullPath);
      if (index > -1) {
        let page = state.pages[index]
        if (page.closeable) {
          page = state.pages[state.pages.length - 1];
          state.currPageFullPath = page.fullPath;
          state.pages.splice(index, 1)
        }
      }
    },
    login({
      commit,
      state
    }, {
      user
    }) {
      commit('login', user)
      $http.get('/api/Permission/Permissions').then(data => {
        commit('setPermission', data)
        state.isLoadPermission = true
      })
      eventBus.$emit("init");
    },
    loadEnumValues({
      state
    }, name) {
      if (!state.enumItemValues[name]) {
        let obj = {}
        obj[name] = []
        state.enumItemValues = {
          ...state.enumItemValues,
          ...obj
        }
        $http.get(`/api/EnumItem/GetValues/${name}`).then((data) => {
          state.enumItemValues[name] = data
        })
      }
    }
  },
  getters: {
    mapMode: state => {
      return state.mapMode || window.localStorage.getItem('mapMode') || 'bd'
    },
    getSiteNameAsync: state => async () => {
      if (!state.tenant || !state.tenant.Id) {
        await sleep(1000)
      }
      return state.tenant || {};
    },
    existsLoginAsync: state => async () => {
      if (!state.currUser.Id) {
        await sleep(1000)
      }
      if (!state.currUser.Id) {
        throw new Error('未登陆');
      }
    },
    existsPermission: state => (moduleName, actionName = 'List') => {
      let patent = state.permissionList.find(v => v.EnCode == moduleName)
      return !!patent && !!state.permissionList.find(v => v.EnCode == actionName && v.Super_Id == patent.Id)
    },
    existsPermissionAsync: state => async (moduleName, actionName = 'List') => {
      if (!state.isLoadPermission) {
        await sleep(2000)
      }
      let patent = state.permissionList.find(v => v.EnCode == moduleName);
      return !!patent && !!state.permissionList.find(v => v.EnCode == actionName && v.Super_Id == patent.Id);
    },
    getItemValues: state => (enumKey, superId) => {
      if (state.enumItemValues[enumKey]) {
        let items = state.enumItemValues[enumKey]
        if (superId) {
          items = items.filter(v => v.Super_Id == superId)
        }
        return items;
      } else {
        return []
      }
    }
  }
})