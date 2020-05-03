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
          <menu-item title="地图演示" icon="map" path="/map" />
          <menu-item title="通知" icon="notifications" path="/notifyCenter" />
          <v-divider></v-divider>
          <div v-for="(group,index) in menus" :key="index">
            <v-subheader>{{ group.title }}</v-subheader>
            <div v-for="(menu,index2) in group.items" :key="index2">
              <MenuGroup v-if="menu.items && menu.items.length>0" v-bind="menu" :level="1 " />
              <menu-item v-else v-bind="menu" />
            </div>
          </div>
        </v-list>
      </vue-perfect-scrollbar>
    </div>
  </v-navigation-drawer>
</template>

<script>
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
      return this.$store.state.menuList;
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

.v-application--is-ltr .v-list-item__action:first-child, .v-application--is-ltr .v-list-item__icon:first-child {
  margin-right: 24px;
}

.v-application--is-ltr .v-list-item__action:last-of-type:not(:only-child), .v-application--is-ltr .v-list-item__avatar:last-of-type:not(:only-child), .v-application--is-ltr .v-list-item__icon:last-of-type:not(:only-child) {
  padding-left: 6px;
}
</style>

