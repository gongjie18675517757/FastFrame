<template>
  <div>
    <router-view v-if="resufreshed"/>
    <Alert/>
    <component v-for="dialog in dialogs" :key="dialog.key" :is="dialog.render"/>
  </div>
</template>

<script>
import Alert from '@/components/Alert.vue'
export default {
  components: { Alert },
  provide() {
    return {
      reload: this.resufresh
    }
  },
  data() {
    return {
      resufreshed: true
    }
  },
  computed: {
    dialogs() {
      return this.$store.state.dialogs
    }
  },
  async created() {
    let request = await this.$http.get('/api/account/GetCurrent')
    this.$store.commit('login', request)
  },
  methods: {
    resufresh() {
      this.resufreshed = false
      this.$nextTick(function() {
        this.resufreshed = true
      })
    }
  }
}
</script>

<style>
.form {
  padding: 5px;
}
.v-card__title {
  padding: 2px;
}
</style>
