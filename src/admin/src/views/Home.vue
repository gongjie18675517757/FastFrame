<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <Menus/>
    <Toolbar :title="title"/>
    <v-content>
      <v-fade-transition mode="out-in">
        <div>
          <keep-alive>
            <router-view v-if="$route.meta.keepAlive && resufreshed"></router-view>
          </keep-alive>
          <router-view v-if="!$route.meta.keepAlive && resufreshed"></router-view>
        </div>
      </v-fade-transition>
      <!-- <router-view></router-view> -->
    </v-content>
    <Setting/>
    <v-footer :fixed="fixed" app inset>
      <span>&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script>
import Setting from "@/components/Setting.vue";
import Menus from "@/components/Menus.vue";
import Toolbar from "@/components/Toolbar.vue";
 

export default {
  components: {
    Setting,
    Menus,
    Toolbar
  },
  provide() {
    return {
      reload: this.resufresh
    };
  },
  data() {
    return {
      fixed: false,
      resufreshed: true,
      activePage: null,
      pages: [
        {
          key: new Date().getTime().toString(),
          src: "@/views/Index/Index.vue",
          title: "首页",
          canClose: false
        }
      ]
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
    
  },
  methods: {
    resufresh() {
      this.resufreshed = false;
      this.$nextTick(function() {
        this.resufreshed = true;
      });
    },
    handlePageClose() {},
    handlePageActive() {}
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
</style>

 