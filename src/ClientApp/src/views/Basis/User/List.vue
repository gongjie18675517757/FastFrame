<template>
  <Page v-bind="page"/>
</template>

<script>
import Page from '@/components/Page/BasisListPage.vue'
import { alert } from '@/utils'
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
          area: 'Basis',
          name: 'User',
          direction: '用户',
          toolItems: [
            {
              name: 'ToogleAdminIdentity',
              title: '切换身份[管理员<>普通用户]',
              icon: 'perm_identity',
              disabled({ selection }) {
                return selection.length == 0
              },
              async action({ selection, rows }) {
                let { Id } = selection[0]
                let result = await this.$http.put(`/api/user/ToogleAdminIdentity/${Id}`)
                alert.success('切换成功!')
                let index = rows.findIndex(r => r.Id == Id)
                if (index > -1) {
                  rows.splice(index, 1, result)
                }
              }
            },
            {
              name: 'ToogleDisabled',
              title: '切换状态[禁用<>启用]',
              icon: 'sync_disabled',
              disabled({ selection }) {
                return selection.length == 0
              },
              async action({ selection, rows }) {
                let { Id } = selection[0]
                let result = await this.$http.put(`/api/user/ToogleDisabled/${Id}`)
                alert.success('切换成功!')
                let index = rows.findIndex(r => r.Id == Id)
                if (index > -1) {
                  rows.splice(index, 1, result)
                }
              }
            }
          ]
        },
        pageInfo: {
          success: this.success,
          close: this.close,
          pars: {
            ...this.pars, 
          }
        }
      }
    }
  }
}
</script>

<style>
</style>
