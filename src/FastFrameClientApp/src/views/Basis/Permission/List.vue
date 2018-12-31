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
          name: 'Permission',
          direction: '权限'
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
