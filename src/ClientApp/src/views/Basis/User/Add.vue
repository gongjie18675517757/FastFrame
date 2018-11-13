<template>
  <Page v-bind="page"/>
</template>

<script>
import Page from '@/components/Page/BasisFormPage.vue'
export default {
  components: {
    Page
  },
  data() {
    return {
      page: {
        moduleInfo: {
          name: 'user',
          direction: '用户'
        }
      }
    }
  }
}
</script>

<style>
</style>
