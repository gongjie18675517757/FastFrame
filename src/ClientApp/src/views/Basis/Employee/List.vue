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
          name: 'Employee',
          direction: '员工表'
        }
      }
    }
  }
}
</script>
