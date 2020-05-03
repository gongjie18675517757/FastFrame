<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <Menus />
    <Toolbar :title="title" />
    <v-content>
      <v-fade-transition mode="out-in">
        <div v-if="$store.state.singlePageMode">
          <keep-alive>
            <router-view v-if="$route.meta.keepAlive && resufreshed"></router-view>
          </keep-alive>
          <router-view v-if="!$route.meta.keepAlive && resufreshed"></router-view>
        </div>
        <Pages v-else />
      </v-fade-transition>
    </v-content>
    <Setting />
    <v-footer :fixed="fixed" app inset>
      <span>&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script>
import Setting from "@/components/Setting.vue";
import Menus from "@/components/Menus.vue";
import Toolbar from "@/components/Toolbar.vue";
import Pages from "./Pages.vue";

export default {
  components: {
    Setting,
    Menus,
    Toolbar,
    Pages
  },
  provide() {
    return {
      reload: this.resufresh
    };
  },
  data() {
    return {
      fixed: false,
      resufreshed: true
    };
  },
  computed: {
    title() {
      return this.$store.state.tenant.FullName;
    },
    singlePageMode() {
      return this.$store.state.singlePageMode;
    }
  },
  created() {
    window.getMatchedComponents=(v)=>this.$router.getMatchedComponents(v)
  },
  methods: {
    resufresh() {
      this.resufreshed = false;
      this.$nextTick(function() {
        this.resufreshed = true;
      });
    }
  }
};
</script>
<style scoped>
.v-toolbar {
  box-shadow: none;
  -webkit-box-shadow: none;
}
body {
  overflow: hidden;
}
.container {
  padding: 5px;
}
.tabs {
  background: #ffffff;
}
</style>
<style>
</style>


 