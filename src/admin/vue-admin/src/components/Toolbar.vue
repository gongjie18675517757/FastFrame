<template>
  <v-app-bar color="primary" fixed dark dense app>
    <v-app-bar-nav-icon @click.stop="leftDrawer = !leftDrawer">
      <v-icon>menu</v-icon>
    </v-app-bar-nav-icon>
    <v-toolbar-title>{{ title }}</v-toolbar-title>
    <v-spacer></v-spacer>
    <v-menu
      offset-y
      origin="center center"
      class="elelvation-1"
      :nudge-bottom="14"
      transition="scale-transition"
      min-width="300px"
    >
      <template v-slot:activator="{ on }">
        <v-btn icon text v-on="on">
          <v-badge color="red" overlap>
            <span slot="badge">{{ notifyCount }}</span>
            <v-icon medium>notifications</v-icon>
          </v-badge>
        </v-btn>
      </template>

      <Notification />
    </v-menu>
    <v-btn icon @click.stop="rightDrawer = !rightDrawer">
      <v-icon>settings</v-icon>
    </v-btn>
    <v-btn icon @click="handleFullScreen">
      <v-icon>fullscreen</v-icon>
    </v-btn>
    <v-menu
      offset-y
      origin="center center"
      :nudge-bottom="10"
      transition="scale-transition"
    >
      <template v-slot:activator="{ on }">
        <v-btn icon large text v-on="on">
          <v-avatar size="30px">
            <img :src="handIcon" alt />
          </v-avatar>
        </v-btn>
      </template>

      <v-list class="pa-0">
        <v-list-item
          v-for="(item, index) in items"
          :to="item.href"
          @click="item.click"
          ripple="ripple"
          :disabled="item.disabled"
          :target="item.target"
          rel="noopener"
          :key="index"
        >
          <v-list-item-action v-if="item.icon">
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-app-bar>
</template>

<script>
import Notification from "@/components/Notification.vue";
import timg from "@/assets/timg.jpg";
import { getDownLoadPath } from "../config";
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
          icon: "account_circle",
          title: "个人中心",
          click: () => {
            this.$router.push("/userCenter");
          }
        },
        {
          icon: "settings",
          title: "更换头像",
          click: () => {
            this.rightDrawer = true;
          }
        },
        {
          icon: "fullscreen_exit",
          title: "注销",
          click: async () => {
            this.$store.dispatch("logout");
            this.$router.push("/login");
          }
        }
      ]
    };
  },
  computed: {
    handIcon() {
      let iconId = (this.$store.state.currUser || {}).HandIcon_Id;
      return iconId ? getDownLoadPath(iconId) : timg;
    },
    notifyCount() {
      return this.$store.state.notifys.length;
    },
    leftDrawer: {
      get() {
        return this.$store.state.leftDrawer;
      },
      set(val) {
        this.$store.commit({
          type: "toggleLeftDrawer",
          value: val
        });
      }
    },
    rightDrawer: {
      get() {
        return this.$store.state.rightDrawer;
      },
      set(val) {
        this.$store.commit({
          type: "toggleRightDrawer",
          value: val
        });
      }
    }
  },
  methods: {
    handleFullScreen() {
      let doc = window.document;
      let docEl = doc.documentElement;

      let requestFullScreen =
        docEl.requestFullscreen ||
        docEl.mozRequestFullScreen ||
        docEl.webkitRequestFullScreen ||
        docEl.msRequestFullscreen;
      let cancelFullScreen =
        doc.exitFullscreen ||
        doc.mozCancelFullScreen ||
        doc.webkitExitFullscreen ||
        doc.msExitFullscreen;

      if (
        !doc.fullscreenElement &&
        !doc.mozFullScreenElement &&
        !doc.webkitFullscreenElement &&
        !doc.msFullscreenElement
      ) {
        requestFullScreen.call(docEl);
      } else {
        cancelFullScreen.call(doc);
      }
    }
  }
};
</script>

 
