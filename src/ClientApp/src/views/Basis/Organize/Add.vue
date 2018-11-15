<template>
  <Page v-bind="page" @success="$emit('success',$event)"/>
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
          area:'Basis',
          name: 'Organize',
          direction: '组织信息',
          ...this.$attrs
        }
      }
    }
  }
}
</script>

<style>
</style>
