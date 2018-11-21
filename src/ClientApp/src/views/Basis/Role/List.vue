<template>
  <Page v-bind="page"/>
</template>
<script>
import Page from '@/components/Page/BasisListPage.vue'
import { showDialog, alert } from '@/utils'
import TreeSelect from '@/components/Page/TreeSelect.vue'
import CheckGroup from '@/components/Page/CheckGroup.vue'

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
          name: 'Role',
          direction: '角色',
          toolItems: [
            {
              name: 'SetRolePermission',
              title: '分配权限',
              icon: 'error_outline',
              disabled({ selection }) {
                return selection.length == 0
              },
              async action({ selection }) {
                let { Id } = selection[0]
                let data = await this.$http.get(`/api/role/GetRolePermission/${Id}`)
                let ids = await showDialog(TreeSelect, {
                  title: '设置权限',
                  requestUrl: '/api/Permission/list',
                  model: data.map(r => r.Id)
                })
                await this.$http.put(`/api/role/SetRolePermission/${Id}`, ids)

                alert.success('设置成功!')
              }
            },
            {
              name: 'SetRoleMember',
              title: '分配成员',
              icon: 'error_outline',
              disabled({ selection }) {
                return selection.length == 0
              },
              async action({ selection }) {
                let { Id } = selection[0]
                let data = await this.$http.get(`/api/role/GetRoleMember/${Id}`)
                let ids = await showDialog(CheckGroup, {
                  title: '角色成员',
                  requestUrl: '/api/User/list',
                  model: data.map(r => r.Id),
                  labelFormatter(item) {
                    return `${item.Name}[${item.Account}]`
                  }
                })
                await this.$http.put(`/api/role/SetRoleMember/${Id}`, ids)

                alert.success('设置成功!')
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
