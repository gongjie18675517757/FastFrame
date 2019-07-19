<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <Menus />
    <Toolbar :title="title" />
    <v-content>
      <v-fade-transition mode="out-in" v-if="$store.state.singlePageMode">
        <div>
          <keep-alive>
            <router-view v-if="$route.meta.keepAlive && resufreshed"></router-view>
          </keep-alive>
          <router-view v-if="!$route.meta.keepAlive && resufreshed"></router-view>
        </div>
      </v-fade-transition>
      <template v-else>
        <v-flex class="tabs">
          <v-chip
            :close="page.closeable"
            color="primary"
            label
            :outline="page.fullPath!=$store.state.currPageFullPath"
            :text-color="page.fullPath==$store.state.currPageFullPath?'#fff':''"
            v-for="page in $store.state.pages"
            :key="page.fullPath"
            @click="clickPage(page)"
            :value="true"
            @input="closePage(page)"
          >{{page.title}}</v-chip>
        </v-flex>
        <v-divider></v-divider>
        <v-flex
          xs12
          v-for="page in $store.state.pages"
          :key="`${page.fullPath}`"
          v-show="page.fullPath==$store.state.currPageFullPath"
        >
          <component :is="page.component" v-bind="page.pars" isTab />
        </v-flex>
      </template>
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
  created() {},
  methods: {
    resufresh() {
      this.resufreshed = false;
      this.$nextTick(function() {
        this.resufreshed = true;
      });
    },
    clickPage({ fullPath }) {
      this.$router.push(fullPath);
    },
    closePage(page) {
      this.$store.dispatch("closePage", page.fullPath);
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


 