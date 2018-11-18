<template>
  <v-container grid-list-xl fluid>
    <v-layout row wrap="">
      <v-flex lg12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{moduleInfo.direction}}列表</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="refresh" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <a-btn @click="toEdit({})" icon title="新增">
              <v-icon>add</v-icon>
            </a-btn>
            <a-btn
              icon
              title="修改"
              :disabled="selection.length!=1 && !currentRow"
              @click="toEdit(currentRow || selection[0])"
            >
              <v-icon>edit</v-icon>
            </a-btn>
            <a-btn @click="remove()" :disabled="!havSelection" icon title="删除">
              <v-icon>delete</v-icon>
            </a-btn>
            <a-btn
              v-for="item in toolitems"
              :key="item.name"
              icon
              v-if="evalShow(item)"
              :title="item.title"
              :disabled="evalDisabled(item)"
              @click="evalAction(item)"
            >
              <v-icon>{{item.icon}}</v-icon>
            </a-btn>
            <v-menu offset-y>
              <v-btn icon slot="activator" title="设置">
                <v-icon>more_vert</v-icon>
              </v-btn>
              <v-list>
                <v-list-tile v-if="!pageInfo.success">
                  <v-list-tile-action>
                    <v-checkbox v-model="dialogMode"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>弹出模式</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-list-tile>
                  <v-list-tile-action>
                    <v-checkbox v-model="showMamageField"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>显示管理字段</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-menu>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-title>
            <!-- <a-btn to="/user/add">添加</a-btn> -->
            <v-spacer></v-spacer>
            <v-text-field
              append-icon="search"
              label="搜索"
              single-line
              hide-details
              v-model="search"
              @change="loadList"
            ></v-text-field>
          </v-card-title>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <v-data-table
              :headers="headers"
              :loading="loading"
              :items="items"
              :total-items="total"
              :pagination.sync="pager"
              v-model="selection"
              @update:pagination="loadList"
              item-key="Id"
            >
              <template slot="items" slot-scope="props">
                <tr :active="!singleSelection && props.selected" @click="handleRowClick(props)">
                  <td>
                    <v-icon
                      small
                      size="16"
                      color="primary"
                      v-if="singleSelection && currentRow==props.item"
                    >check</v-icon>
                    <v-checkbox
                      v-if="!singleSelection"
                      primary
                      hide-details
                      v-model="props.selected"
                    ></v-checkbox>
                  </td>
                  <!-- <td>
                  <v-avatar size="32">
                    <img
                      :src="props.item.HandIconId?'/api/resource/get/'+props.item.HandIconId:timg"
                    >
                  </v-avatar>
                  </td>-->
                  <td v-for="col in columns" :key="col.Name">
                    <Cell :info="col" :model="props.item" @toEdit="toEdit(props.item)"/>
                  </td>
                </tr>
              </template>
              <template slot="no-data">没有加载数据</template>
            </v-data-table>
          </v-card-text>
          <v-card-actions v-if="this.pageInfo.success">
            <v-btn flat @click="this.pageInfo.close">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="success">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { getColumns } from '@/generate'
import { showDialog } from '@/utils'
import { getComponent } from '@/router'
import Cell from './Cell.vue'

export default {
  components: {
    Cell
  },
  props: {
    moduleInfo: {
      type: Object,
      default: function() {
        return {
          area: '',
          name: '',
          direction: ''
        }
      }
    },
    pageInfo: {
      type: Object,
      default: function() {
        return {}
      }
    }
  },
  inject: ['reload'],
  data() {
    return {
      search: '',
      selection: [],
      currentRow: null,
      pager: {},
      loading: false,
      total: 0,

      dialogMode: true,
      showMamageField: false,
      cols: [],
      items: []
    }
  },
  computed: {
    path() {
      return {
        add: `/${this.moduleInfo.name}/add`
      }
    },
    singleSelection() {
      return this.pageInfo.pars && this.pageInfo.pars.single
    },
    headers() {
      return [
        {
          text: '#',
          sortable: false,
          width: '50px'
        },
        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name
          }
        })
      ]
    },
    columns() {
      let adminColumns = [
        { Name: 'CreateName', Description: '创建人' },
        { Name: 'CreateTime', Description: '创建时间' },
        { Name: 'ModifyName', Description: '修改人' },
        { Name: 'ModifyTime', Description: '修改时间' }
      ]
      return [...this.cols, ...(this.showMamageField ? adminColumns : [])]
    },
    havSelection() {
      return this.selection.length > 0 || !!this.currentRow
    },
    toolitems() {
      return this.moduleInfo.toolItems || []
    },
    context() {
      let selection = []
      if (this.singleSelection && this.currentRow) selection = [this.currentRow]
      else selection = this.selection
      return {
        selection,
        rows: this.items
      }
    }
  },
  async created() {
    this.cols = await getColumns(this.moduleInfo.name)
  },
  methods: {
    evalShow({ show }) {
      let val = true
      if (typeof show == 'function') val = show.call(this, this.context)
      if (typeof show == 'boolean') val = show
      if (typeof show == 'string') val = !!show

      return val
    },
    evalDisabled({ disabled }) {
      let val = false
      if (typeof disabled == 'function') val = disabled.call(this, this.context)
      if (typeof disabled == 'boolean') val = disabled
      if (typeof disabled == 'string') val = !!disabled

      return val
    },
    evalAction({ action }) {
      if (typeof action == 'function') action.call(this, this.context)
    },
    refresh() {
      this.reload()
    },
    handleRowClick(props) {
      if (this.singleSelection) {
        this.currentRow = props.item
      } else {
        props.selected = !props.selected
      }
    },
    async toEdit({ Id = '' } = {}) {
      let url = `${this.path.add}?q=${Id}`
      let { name } = this.moduleInfo
      if (this.dialogMode) {
        let component = getComponent(`${name}_add`)
        let data = await showDialog(component, { id: Id })
        let index = this.items.findIndex(x => x.Id == data.Id)
        if (index != -1) {
          this.items.splice(index, 1, data)
        } else {
          this.items.splice(0, 0, data)
        }
      } else {
        this.$router.push(url)
      }
    },
    async remove() {
      let ids = []
      if (this.singleSelection) {
        ids = [currentRow.Id]
      } else {
        ids = this.selection.map(r => r.Id)
      }
      for (const id of ids) {
        try {
          await this.$http.delete(`/api/${this.moduleInfo.name}/delete/${id}`)
          let index = this.items.findIndex(r => r.Id == id)
          this.items.splice(index, 1)
        } finally {
        }
      }
    },
    async loadList() {
      this.loading = true
      let { page, rowsPerPage, sortBy, descending } = this.pager,
        keyword = this.search
      let { queryFilter = [] } = this.pageInfo
      if (typeof queryFilter == 'function') {
        queryFilter = await queryFilter.call(this, context)
      }

      let pageInfo = {
        PageIndex: page,
        PageSize: rowsPerPage,
        Condition: {
          KeyWord: this.search
        },
        SortInfo: {
          Name: sortBy || 'Id',
          Mode: descending ? 'asc' : 'desc'
        },
        Filters: [...queryFilter]
      }
      try {
        let { Total, Data } = await this.$http.post('/api/' + this.moduleInfo.name + '/list', pageInfo)
        this.items = Data
        this.total = Total
      } finally {
        this.loading = false
      }
    },
    success() {
      let selection = []
      if (this.singleSelection) {
        selection = [this.currentRow]
      } else {
        selection = this.selection
      }
      this.$emit('success', selection)
      this.pageInfo.success(selection)
    }
  },
  watch: {}
}
</script>
<style>
.btn-group .v-btn {
  padding: 0px;
  margin: 1px;
}
.selection {
  background: #eee;
}
</style>

