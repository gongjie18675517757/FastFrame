import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    currUser: {},
    leftDrawer: true,
    rightDrawer: false,
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
    dialogs: []
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
    }

  },
  actions: {
    
  }
})