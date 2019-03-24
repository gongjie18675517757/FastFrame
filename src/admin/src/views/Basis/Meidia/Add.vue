<template>
  <Page v-bind="page" @success="$emit('success',$event)"/>
</template>

<script>
import Page from '@/components/Page/BasisFormPage.vue'
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
          name: 'Meidia',
          direction: '图片库',
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

 
