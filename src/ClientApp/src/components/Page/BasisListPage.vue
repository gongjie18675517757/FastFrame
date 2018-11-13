<template>
  <v-container grid-list-xl fluid>
    <v-layout row wrap="">
      <v-flex lg12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{moduleInfo.direction}}列表</v-toolbar-title>
            <v-spacer></v-spacer>
            <a-btn :to="path.add">添加</a-btn>
            <a-btn @click="remove" :disabled="selection.length==0">删除</a-btn>
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
                  <span v-else-if="col.Relate">{{ props.item[col.Name] }}</span>
                  <span v-else-if="col.Type=='String'">{{ props.item[col.Name] }}</span>
                </td>
                <td>
                  <v-btn depressed outline icon fab dark color="primary" small>
                    <v-icon>edit</v-icon>
                  </v-btn>
                  <v-btn depressed outline icon fab dark color="pink" small>
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
          name: '',
          direction: ''
        }
      }
    }
  },
  data() {
    return {
      search: '',
      selection: [],
      pager: {},
      loading: false,
      total: 0,

      headers: [],
      columns: [],
      items: []
    }
  },
  computed: {
    path() {
      return {
        add: `/${this.moduleInfo.name}/add`
      }
    }
  },
  async created() {
    let cols = await getColumns(this.moduleInfo.name)
    this.headers = [
      {
        text: '#',
        value: '',
        sortable: false
      },
      ...cols.map(c => {
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
    this.columns = cols
  },
  methods: {
    remove() {},
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

