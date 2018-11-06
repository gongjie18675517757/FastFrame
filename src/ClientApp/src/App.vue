<template>
  <div>
    <router-view/>
    <Alert/>
  </div>
</template>

<script>
import Alert from '@/components/Alert.vue'
export default {
  components: { Alert },
  data() {
    return {}
  },
  created() {}
}
</script>

<style>
</style>
