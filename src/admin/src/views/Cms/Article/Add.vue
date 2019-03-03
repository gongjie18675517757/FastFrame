<template>
  <Page v-bind="page" @success="$emit('success',$event)"/>
</template>

<script>
import Page from '@/components/Page/BasisFormPage.vue'
export default {
  props: {
    success: Function,
    close: Function,
    pars: Object
  },
  components: {
    Page
  },
  data() {
    return {
      page: {
        moduleInfo: {
          area: 'CMS',
          name: 'Article',
          direction: '文章',
          formatterForm(form) {
            return {
              Content: '',
              ...form
            }
          },
          formatterOptions(options) {
            return [
              ...options,
              {
                IsRichText: true,
                Description: '文章内容',
                Name: 'Content'
              }
            ]
          }
        },
        pageInfo: {
          success: this.success,
          close: this.close,
          pars: this.pars
        }
      }
    }
  }
}
</script>

<style>
</style>
