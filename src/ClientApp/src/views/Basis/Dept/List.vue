<template>
  <Page v-bind="page"/>
</template>

<script>
import Page from '@/components/Page/BasisListPage.vue'
export default {
  components: {
    Page
  },
  data() {
    return {
      page: {
        moduleInfo: {
          area:'Basis',
          name: 'Dept',
          direction: '部门'
        }
      }
    }
  }
}
</script>

<style>
</style>
