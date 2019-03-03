import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    currUser: {},
    tenant: {},
    leftDrawer: true,
    rightDrawer: false,
    dialogMode: true,
    mapMode: '',
    notifys: [{
        title: '您的帐户入帐100元',
        color: 'light-green',
        icon: 'account_circle',
        timeLabel: '刚刚'
      },
      {
        title: '您的帐户入帐100元',
        color: 'light-blue',
        icon: 'shopping_cart',
        timeLabel: '2分钟前'
      },
      {
        title: '您的帐户入帐100元',
        color: 'cyan',
        icon: 'payment',
        timeLabel: '24分钟前'
      },
      {
        title: '您的帐户入帐100元',
        color: 'red',
        icon: 'email',
        timeLabel: '1小时前'
      }
    ],
    dialogs: [],
    friendMsgs: []
  },
  mutations: {
    login(state, payload) {
      state.currUser = payload
    },
    logout(state) {
      state.currUser = null
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
    }
  },
  actions: {},
  getters: {
    mapMode: state => {
      return state.mapMode || window.localStorage.getItem('mapMode') || 'bd'
    }
  }
})