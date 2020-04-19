import Vue from 'vue'
import Vuex from 'vuex'
import $http from '../httpClient'
import router from '../router'
import menuList from './menu'
import { sleep } from '@/utils'
import { start, stop } from '../hubs'
Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    /**
     * 是否使用单页签模式
     */
    singlePageMode: false,

    /**
     * 是否在移动端模式下
     */
    isXs:false,

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
    setPermission(state, payload) {
      state.permissionList = payload
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
          if (typeof permission == "string") permission = [permission, "List"];
          if (Array.isArray(permission)) {
            let [moduleName, pageName] = permission;
            let parentIds = data.filter(r => r.EnCode == moduleName).map(v => v.Id);
            let val = parentIds.length > 0 &&
              !!data.find(
                v => v.EnCode == pageName && parentIds.includes(v.Super_Id))


            return val;
          }
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
    existsPermission: state => (moduleName, actionName = 'List') => {
      let arr = Array.isArray(actionName) ? actionName : [actionName];
      return !!arr.find(r => {
        let patentIds = state.permissionList.filter(v => v.EnCode == moduleName).map(v => v.Id) || []
        return patentIds.length > 0 && !!state.permissionList.find(v => v.EnCode == r && patentIds.includes(v.Super_Id))
      })
    },
    existsPermissionAsync: state => async (moduleName, actionName = 'List') => {
      let count = 0;
      while (count < 20) {
        if (!state.isLoadPermission) {
          await sleep(100)
        } else {
          break;
        }
        count++;
      }

      let patentIds = state.permissionList.filter(v => v.EnCode == moduleName).map(v => v.Id);
      let val = patentIds.length > 0 && !!state.permissionList.find(v => v.EnCode == actionName && patentIds.includes(v.Super_Id));
      return val;
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