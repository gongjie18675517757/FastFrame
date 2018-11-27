<template>
  <Page v-bind="page"/>
</template>
<script>
import Page from '@/components/Page/BasisListPage.vue'
import {alert} from '@/utils'
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
          toolItems: [
            {
              name: 'ToggleRelease',
              title: '发布/取消发布',
              icon: 'fa-toggle-on',
              disabled({ selection }) {
                return selection.length != 1
              },
              async action({ selection, rows }) {
                let { Id, IsRelease } = selection[0]
                let result = await this.$http.put(`/api/Article/ToggleRelease/${Id}`)
                let msg = `${IsRelease ? '取消' : ''}发布成功`
                selection[0].IsRelease = !IsRelease
                alert.success(msg)
              }
            }
          ],
          
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
