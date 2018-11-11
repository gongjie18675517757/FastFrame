<template>
  <v-toolbar color="primary" fixed app dark dense>
    <v-toolbar-side-icon @click.stop="leftDrawer = !leftDrawer"></v-toolbar-side-icon>
    <v-toolbar-title v-text="title"></v-toolbar-title>
    <v-spacer></v-spacer>
    <v-menu
      offset-y
      origin="center center"
      class="elelvation-1"
      :nudge-bottom="14"
      transition="scale-transition"
    >
      <v-btn icon flat slot="activator">
        <v-badge color="red" overlap>
          <span slot="badge">{{notifyCount}}</span>
          <v-icon medium>notifications</v-icon>
        </v-badge>
      </v-btn>
      <Notification/>
    </v-menu>
    <v-btn icon @click.stop="rightDrawer = !rightDrawer">
      <v-icon>settings</v-icon>
    </v-btn>
    <v-btn icon @click="handleFullScreen()">
      <v-icon>fullscreen</v-icon>
    </v-btn>
    <v-menu offset-y origin="center center" :nudge-bottom="10" transition="scale-transition">
      <v-btn icon large flat slot="activator">
        <v-avatar size="30px">
          <img :src="handIcon" alt="">
        </v-avatar>
      </v-btn>
      <v-list class="pa-0">
        <v-list-tile
          v-for="(item,index) in items"
          :to="item.href"
          @click="item.click"
          ripple="ripple"
          :disabled="item.disabled"
          :target="item.target"
          rel="noopener"
          :key="index"
        >
          <v-list-tile-action v-if="item.icon">
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-menu>
  </v-toolbar>
</template>

<script>
import Notification from '@/components/Notification.vue'
import timg from '@/assets/timg.jpg'
export default {
  components: {
    Notification
  },
  props: {
    title: String
  },
  data() {
    return {
      clipped: false,
      items: [
        {
          icon: 'account_circle',
          title: '个人中心',
          click: e => {
            this.$router.push('/userCenter')
          }
        }, 
        {
          icon: 'settings',
          title: '更换头像',
          click: e => {
            this.rightDrawer = true
          }
        },
        {
          icon: 'fullscreen_exit',
          title: '注销',
          click: async e => {
            this.$store.commit('logout')
            this.$router.push('/login')
            this.$http.post()
          }
        }
      ]
    }
  },
  computed: {
    handIcon(){
      let iconId=this.$store.state.currUser.HandIconId;
      return iconId?`/api/resource/get/${iconId}`:timg
    },
    notifyCount() {
      return this.$store.state.notifys.length
    },
    leftDrawer: {
      get() {
        return this.$store.state.leftDrawer
      },
      set(val) {
        this.$store.commit({
          type: 'toggleLeftDrawer',
          value: val
        })
      }
    },
    rightDrawer: {
      get() {
        return this.$store.state.rightDrawer
      },
      set(val) {
        this.$store.commit({
          type: 'toggleRightDrawer',
          value: val
        })
      }
    }
  },
  methods: {
    handleFullScreen() {
      let doc = window.document
      let docEl = doc.documentElement

      let requestFullScreen =
        docEl.requestFullscreen ||
        docEl.mozRequestFullScreen ||
        docEl.webkitRequestFullScreen ||
        docEl.msRequestFullscreen
      let cancelFullScreen =
        doc.exitFullscreen ||
        doc.mozCancelFullScreen ||
        doc.webkitExitFullscreen ||
        doc.msExitFullscreen

      if (
        !doc.fullscreenElement &&
        !doc.mozFullScreenElement &&
        !doc.webkitFullscreenElement &&
        !doc.msFullscreenElement
      ) {
        requestFullScreen.call(docEl)
      } else {
        cancelFullScreen.call(doc)
      }
    }
  }
}
</script>

<style>
</style>
