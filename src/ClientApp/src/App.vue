<template>
  <div>
    <router-view v-if="resufreshed"/>
    <Alert/>
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
  created() {},
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
