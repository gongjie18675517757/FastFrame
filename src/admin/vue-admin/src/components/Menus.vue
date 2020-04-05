<template>
  <v-navigation-drawer
    :mini-variant="miniVariant"
    fixed
    :dark="$vuetify.dark"
    app
    v-model="leftDrawer"
    width="200"
  >
    <div id="appDrawer">
      <v-toolbar color="primary darken-1" dark dense flat @click="toTenantCenter">
        <img :src="handicon" height="36" alt="Vue Material Admin Template" />
        <v-toolbar-title class="ml-0 pl-3">
          <span class="hidden-sm-and-down">{{tenant.ShortName}}</span>
        </v-toolbar-title>
      </v-toolbar>
      <vue-perfect-scrollbar class="drawer-menu--scroll" :settings="scrollSettings">
        <v-list dense expand>
          <v-subheader>常用</v-subheader>
          <menu-item title="首页" icon="dashboard" path="/" />
          <menu-item title="地图" icon="map" path="/map" />
          <menu-item title="邮件" icon="email" />
          <menu-item title="通知" icon="notifications" path="/notifyCenter" />
          <!-- <menu-item title="消息" icon="chat" path="/chat" /> -->
          <v-divider></v-divider>
          <div v-for="(group,index) in menus" :key="index">
            <v-subheader>{{ group.title }}</v-subheader>
            <div v-for="(menu,index2) in group.items" :key="index2">
              <MenuGroup v-if="menu.items && menu.items.length>0" v-bind="menu" />
              <menu-item v-else v-bind="menu" />
            </div>
          </div>
        </v-list>
      </vue-perfect-scrollbar>
    </div>
  </v-navigation-drawer>
</template>

<script>
import menu from "@/menu.js";

import VuePerfectScrollbar from "vue-perfect-scrollbar";
import MenuGroup from "./MenuGroup.vue";
import MenuItem from "./MenuItem.vue";
import logo from "@/assets/logo.png";
export default {
  components: {
    VuePerfectScrollbar,
    MenuGroup,
    MenuItem
  },
  data() {
    return {
      miniVariant: false,
      items: menu,
      scrollSettings: {
        maxScrollbarLength: 160
      }
    };
  },
  destroyed() {
    this.$eventBus.$off("init", this.initMenu);
  },
  computed: {
    handicon() {
      return this.tenant.HandIcon_Id
        ? `/api/resource/get/${this.tenant.HandIcon_Id}`
        : logo;
    },
    tenant() {
      return this.$store.state.tenant;
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
    menus() {
      let existsPermission = this.$store.getters.existsPermission;

      function exists(menus) {
        menus = [...menus];
        for (const item of menus) {
          let { items } = item;
          if (Array.isArray(items)) {
            item.items = exists(items);
          }
        }
        return menus
          .filter(
            v =>
              !v.permission ||
              existsPermission(
                ...(Array.isArray(v.permission)
                  ? v.permission
                  : [v.permission, "List"])
              )
          )
          .filter(v => !v.items || v.items.length > 0);
      }

      return exists(this.items);
    }
  },
  methods: {
    toTenantCenter() {
      this.$router.push({
        path: "/tenantCenter"
      });
    }
  }
};
</script>

<style lang="stylus">
#appDrawer {
  overflow: hidden;

  .drawer-menu--scroll {
    height: calc(100vh - 48px);
    overflow: auto;
  }

  .darken-1 {
    cursor: pointer;
  }
}
</style>

