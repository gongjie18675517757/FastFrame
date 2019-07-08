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
    currUser: {},
    tenant: {},

    isLoadPermission: false,
    permissionList: [],
    leftDrawer: true,
    rightDrawer: false,
    dialogMode: false,
    singlePageMode: true,
    mapMode: '',
    notifys: [],
    dialogs: [],
    friendMsgs: [],
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