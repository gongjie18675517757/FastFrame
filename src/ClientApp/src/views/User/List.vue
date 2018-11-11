<template>
  <v-container grid-list-xl fluid>
    <v-layout row wrap="">
      <v-flex lg12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>用户列表</v-toolbar-title>
            <v-spacer></v-spacer>
            <a-btn to="/user/add">添加</a-btn>
            <a-btn @click="remove" :disabled="selection.length==0">删除</a-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-title>
            <!-- <a-btn to="/user/add">添加</a-btn> -->
            <v-spacer></v-spacer>
            <v-text-field append-icon="search" label="搜索" single-line hide-details v-model="search" @change="loadList"></v-text-field>
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
                <td>
                  <v-avatar size="32">
                    <img
                      :src="props.item.HandIconId?'/api/resource/get/'+props.item.HandIconId:timg"
                    >
                  </v-avatar>
                </td>
                <td>{{ props.item.Account }}</td>
                <td>{{ props.item.Name }}</td>
                <td>{{ props.item.Email }}</td>
                <td>{{ props.item.PhoneNumber }}</td>
                <td>{{ props.item.IsDisabled }}</td>
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
import timg from '@/assets/timg.jpg'
export default {
  data() {
    return {
      timg,
      search: '',
      selection: [],
      pager: {},
      loading: false,
      total: 0,
      headers: [
        {
          text: '#',
          value: '',
          sortable: false
        },
        {
          text: '头像',
          value: 'HandIconId',
          sortable: false
        },
        {
          text: '帐号',
          value: 'Account'
        },
        {
          text: '名称',
          value: 'Name'
        },
        {
          text: '邮箱',
          value: 'Email'
        },
        {
          text: '手机号',
          value: 'PhoneNumber'
        },
        {
          text: '禁用?',
          value: 'IsDisabled'
        },
        {
          text: 'Action',
          sortable: false,
          value: ''
        }
      ],
      items: []
    }
  },
  created() {
    
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
          // Filters: [
          //   {
          //     Name: 'string',
          //     Compare: 'string',
          //     Value: 'string'
          //   }
          // ]
        },
        SortInfo: {
          Name: sortBy,
          Mode: descending ? 'asc' : 'desc'
        }
      }
      try {
        let { Total, Data } = await this.$http.post('/api/user/list', pageInfo)
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

