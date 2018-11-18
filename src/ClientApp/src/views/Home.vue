<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <Menus/>
    <Toolbar :title="title"/>
    <v-content>
      <router-view/>
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
export default {
  components: {
    Setting,
    Menus,
    Toolbar
  },
  data() {
    return {
      fixed: false,
      title: 'XXX管理后台'
    }
  },
  async created() {
    try {
      let request = await this.$http.get('/api/account/GetCurrent')
      this.$store.commit('login', request)
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
</style>

 