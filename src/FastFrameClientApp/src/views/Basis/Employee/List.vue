<template>
  <Page v-bind="page"/>
</template>
<script>
import Page from '@/components/Page/BasisListPage.vue'
export default {
  props:{
      success:Function,
      close:Function,
      pars:Object
  },
  components: {
    Page
  },
  data() {
    return {
      page: {
        moduleInfo: {
          area:'Basis',
          name: 'Employee',
          direction: '员工'
        },
        pageInfo:{
          success:this.success,
          close:this.close,
          pars:this.pars
        }
      }
    }
  }
}
</script>
