import Vue from 'vue'
import Vuex from 'vuex'
import $http from '../httpClient'
import router from '../router'
import menuList from './menu'
import { sleep } from '@/utils'
import { start, stop } from '../hubs'
Vue.use(Vuex)

/**
 * 缓存单面模式
 */
const key_singlePageMode = 'singlePageMode'

/**
 * 缓存表单页显示方式
 */
const key_dialogMode = "dialogMode"

/**
 * 缓存主题颜色
 */
const key_themeColor = 'themeColor'


export default new Vuex.Store({
  state: {
    /**
     * 是否使用单页签模式
     */
    singlePageMode: localStorage.getItem(key_singlePageMode) == "1",

    /**
    * 使用弹窗模式
    */
    dialogMode: localStorage.getItem(key_dialogMode) == "1",

    /**
     * 主题颜色
     */
    themeColor: localStorage.getItem(key_themeColor) || 'indigo',

    /**
     * 是否在移动端模式下
     */
    isXs: false,

    /**
     * 多页内容
     */
    pages: [{
      key: '/',
      fullPath: '/',
      title: '首页',
      pars: {},
      component: () =>
        import(`@/views/Index/Index.vue`),
      lastTime: new Date().getTime()
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
     * 菜单列表
     */
    menuList: [],

    /**
     * 左边菜单栏是否显示
     */
    leftDrawer: true,

    /**
     * 右边菜单栏是否显示
     */
    rightDrawer: false,



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
    setPermission(state, payload) {
      state.permissionList = payload
    },
    setThemeColor(state, payload) { 
      state.themeColor = payload.value
      localStorage.setItem(key_themeColor, payload.value)
    },
    toggleDialogMode(state) {
      state.dialogMode = !state.dialogMode;
      localStorage.setItem(key_dialogMode, state.dialogMode ? 1 : 0)
    },
    togglePageMode(state) {
      state.singlePageMode = !state.singlePageMode;
      localStorage.setItem(key_singlePageMode, state.singlePageMode ? 1 : 0)
    },
    toggleLeftDrawer(state, payload) {
      state.leftDrawer = payload.value
    },
    toggleRightDrawer(state, payload) {
      state.rightDrawer = payload.value
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
          key: new Date().getTime().toString(),
          closeable: true,
          lastTime: new Date().getTime()
        });
      }
      state.lastTime = new Date().getTime()
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
          state.pages.splice(index, 1)
          if (state.currPageFullPath == fullPath) {
            let pages = [...state.pages]
            pages.sort((a, b) => a.lastTime - b.lastTime)
            page = pages[pages.length - 1];
            router.push(page.fullPath);
          }
        }
      }
    },

    /**
     * 登出
     * @param {*} param0 
     */
    logout({ state }) {
      state.isLoadPermission = false
      state.currUser = {}
      stop();
      $http.post('/api/account/logout')
    },

    /**
     * 验证身份
     * @param {*} param0 
     */
    existsIdentity({
      dispatch,
      state
    }) {
      if (state.currUser && state.currUser.Id) {
        return;
      }
      return $http.get("/api/account/GetCurrent").then(data => {
        start();
        return dispatch('login', data)
      }).catch((err) => {
        stop();
        dispatch('logout')
        throw err;
      })
    },

    /**
     * 登陆
     * @param {*} param0 
     * @param {*} param1 
     */
    login({
      commit,
      state
    }, user) {
      commit('login', user);
      return $http.get('/api/Permission/Permissions').then(data => {
        commit('setPermission', data)
        state.isLoadPermission = true;

        function existsMenuPermission(menu) {
          if (Array.isArray(menu.items)) {
            menu.items = menu.items.filter(existsMenuPermission);
          }

          let exists = Array.isArray(menu.items) ?
            menu.items.length > 0 :
            existsPermission(menu.permission);

          return !!exists;
        }

        function existsPermission(permission) {
          if (!permission) return true;
          if (!Array.isArray(permission))
            permission = [permission];

          return !!data.some(v => v.Child.some(r => permission.includes(r.Name)));
        }
        state.menuList = JSON.parse(JSON.stringify(menuList)).filter(existsMenuPermission)

      })
    },

    /**
     * 加载数据字典
     * @param {*} param0 
     * @param {*} name 
     */
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
    },

    /**
     * 验证是否有权限
     * @param {*} param0 
     */
    async existsPermissionAsync({ state, getters }, permission) {
      let count = 0;
      while (count < 20) {
        if (!state.isLoadPermission) {
          await sleep(100)
        } else {
          break;
        }
        count++;
      }

      return getters.existsPermission(permission);
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
    existsPermission: state => (permission) => {
      let arr = Array.isArray(permission) ? permission : [permission];
      return !!state.permissionList.some(v => v.Child.some(r => arr.includes(r.Name)));
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