<template>
  <v-container grid-list-xl fluid>
    <v-layout row wrap="">
      <v-flex lg12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{moduleInfo.direction}}列表</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="refresh">
              <v-icon>refresh</v-icon>
            </v-btn>
            <a-btn @click="toEdit({})" icon>
              <v-icon>playlist_add</v-icon>
            </a-btn>
            <a-btn @click="remove()" :disabled="selection.length==0" icon>
              <v-icon>delete_forever</v-icon>
            </a-btn>
            <v-menu offset-y>
              <v-btn icon slot="activator">
                <v-icon>more_vert</v-icon>
              </v-btn>
              <v-list>
                <v-list-tile>
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
                <td>
                  <v-checkbox primary hide-details v-model="props.selected"></v-checkbox>
                </td>
                <!-- <td>
                  <v-avatar size="32">
                    <img
                      :src="props.item.HandIconId?'/api/resource/get/'+props.item.HandIconId:timg"
                    >
                  </v-avatar>
                </td>-->
                <td v-for="col in columns" :key="col.Name">
                  <span v-if="col.Type=='Boolean'">{{ props.item[col.Name]?'是':'否' }}</span>
                  <span v-else-if="col.Relate">
                    <span>{{getRelateText({column:col,row:props.item})}}</span>
                  </span>
                  <span v-else>{{ props.item[col.Name] }}</span>
                </td>
                <td>
                  <v-btn outline icon color="primary" @click="toEdit(props.item)">
                    <v-icon>edit</v-icon>
                  </v-btn>
                  <v-btn outline icon color="pink" @click="remove(props.item.Id)">
                    <v-icon>delete</v-icon>
                  </v-btn>
                </td>
              </template>
              <template slot="no-data">没有加载数据</template>
            </v-data-table>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <v-dialog v-if="dialog" v-model="dialog" persistent scrollable >
      <component :is="template" @success="success" :pars="pars"/>
    </v-dialog>
  </v-container>
</template>

<script>
import { getColumns } from '@/generate'
export default {
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
    }
  },
  inject: ['reload'],
  data() {
    return {
      search: '',
      selection: [],
      pager: {},
      loading: false,
      total: 0,

      dialog: false,
      dialogMode: true,
      showMamageField: false,
      pars: {},
      template: () =>
        import(`@/views/${this.moduleInfo.area}/${this.moduleInfo.name}/Add`),
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
    headers() {
      return [
        {
          text: '#',
          value: '',
          sortable: false
        },
        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name
          }
        }),
        {
          text: '操作',
          sortable: false,
          value: ''
        }
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
    }
  },
  async created() {
    this.cols = await getColumns(this.moduleInfo.name)
  },
  methods: {
    refresh() {
      this.reload()
    },
    toEdit({ Id = '' } = {}) {
      let url = `${this.path.add}?q=${Id}`
      if (this.dialogMode) {
        this.pars = {
          id: Id
        }
        this.dialog = true
      } else {
        this.$router.push(url)
      }
    },
    async remove(key) {
      let ids = []
      if (key) {
        ids = [key]
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
    getRelateText({ column, row }) {
      let tempName = column.Name.replace('_Id', '')
      let name = column.Relate[0]
      return row[`${tempName}_${name}`]
    },
    async loadList() {
      this.loading = true
      let { page, rowsPerPage, sortBy, descending } = this.pager,
        keyword = this.search
      let pageInfo = {
        PageIndex: page,
        PageSize: rowsPerPage,
        Condition: {
          KeyWord: this.search
        },
        SortInfo: {
          Name: sortBy || 'Id',
          Mode: descending ? 'asc' : 'desc'
        }
      }
      try {
        let { Total, Data } = await this.$http.post(
          '/api/' + this.moduleInfo.name + '/list',
          pageInfo
        )
        this.items = Data
        this.total = Total
      } finally {
        this.loading = false
      }
    },
    success(data) {
      this.dialog = false
      if (data && data.Id) {
        let index = this.items.findIndex(x => x.Id == data.Id)
        if (index != -1) {
          this.items.splice(index, 1, data)
        } else {
          this.items.splice(0, 0, data)
        }
      }
    }
  }
}
</script>
<style>
.btn-group .v-btn {
  padding: 0px;
  margin: 1px;
}
</style>

