import Vue from 'vue'
import {
  Vuetify,
  VApp,
  VNavigationDrawer,
  VFooter,
  VList,
  VBtn,
  VIcon,
  VGrid,
  VToolbar,
  transitions,
  VSubheader,
  VDivider,
  VBtnToggle,
  VTextField,
  VCard,
  VForm,
  VAlert,
  VSnackbar,
  VBadge,
  VMenu,
  VAvatar
} from 'vuetify'
import 'vuetify/src/stylus/app.styl'
import zhHans from 'vuetify/es5/locale/zh-Hans'

Vue.use(Vuetify, {
  components: {
    Vuetify,
    VApp,
    VNavigationDrawer,
    VFooter,
    VList,
    VBtn,
    VIcon,
    VGrid,
    VToolbar,
    transitions,
    VSubheader,
    VDivider,
    VBtnToggle,
    VTextField,
    VCard,
    VForm,
    VAlert,
    VSnackbar,
    VBadge,
    VMenu,
    VAvatar
  },
  // theme: {
  //   primary: '#ee44aa',
  //   secondary: '#424242',
  //   accent: '#82B1FF',
  //   error: '#FF5252',
  //   info: '#2196F3',
  //   success: '#4CAF50',
  //   warning: '#FFC107'
  // },
  customProperties: true,
  iconfont: 'md',
  lang: {
    locales: {
      zhHans
    },
    current: 'zh-Hans'
  },
})