<template>
  <Page v-bind="page"/>
</template>
<script>
import Page from '@/components/Page/BasisListPage.vue'
import {alert} from '@/utils'
export default {
  inject: ['reload'],
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
          area: 'Basis',
          name: 'Permission',
          direction: '权限',
          toolItems: [
            {
              name: 'InitPermission',
              title: '初始化权限',
              icon: 'settings_power',
              async action() {
                await this.$http.post(`/api/Permission/InitPermission`)
                alert.success('初始化成功!')
                this.reload()
              }
            }
          ]
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
