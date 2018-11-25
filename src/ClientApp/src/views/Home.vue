<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <Menus/>
    <Toolbar :title="title"/>
    <v-content>
      <keep-alive>
        <router-view v-if="$route.meta.keepAlive && resufreshed"></router-view>
      </keep-alive>
      <router-view v-if="!$route.meta.keepAlive && resufreshed"></router-view>
      <!-- <router-view></router-view> -->
    </v-content>
    <Setting/>
    <v-footer :fixed="fixed" app inset>
      <span>&copy; 2017</span>
    </v-footer>
  </v-app>
</template>

<script>
import Setting from '@/components/Setting.vue'
import Menus from '@/components/Menus.vue'
import Toolbar from '@/components/Toolbar.vue'
import { init } from '@/permission.js'

export default {
  components: {
    Setting,
    Menus,
    Toolbar
  },
  
  data() {
    return {
      fixed: false,
      title: 'XXX管理后台',
      resufreshed: true
    }
  },
  async created() {
    try {
      let request = await this.$http.get('/api/account/GetCurrent')
      this.$store.commit('login', request)
      await init()
      this.$eventBus.$emit('init')
    } catch (error) {
      if (this.$route.fullpath != '/login') {
        this.$router.push({
          path: '/login',
          query: {
            redirect: this.$route.fullpath
          }
        })
      }
    }
  },
  methods: {
     
  }
}
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

 