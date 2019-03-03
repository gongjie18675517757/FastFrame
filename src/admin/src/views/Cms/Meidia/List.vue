<template>
  <v-container grid-list-xl fluid app class="media">
    <v-layout row wrap="">
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>资源列表</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-title>
            <v-btn-toggle v-model="view">
              <v-btn flat value="list">
                <v-icon>view_headline</v-icon>
              </v-btn>
              <v-btn flat value="grid">
                <v-icon>view_list</v-icon>
              </v-btn>
            </v-btn-toggle>
            <a-btn @click="upload" icon title="上传" :moduleName="moduleInfo.name" name="Add">
              <v-icon>cloud_upload</v-icon>
            </a-btn>
            <a-btn @click="addFolder" icon title="新增文件夹" :moduleName="moduleInfo.name" name="Add">
              <v-icon>folder</v-icon>
            </a-btn>
            <a-btn @click="handleDelete" :disabled="!currItem" icon title="删除" :moduleName="moduleInfo.name" name="Delete">
              <v-icon>delete</v-icon>
            </a-btn>
            <v-btn icon @click="refrsh" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <v-btn icon @click="stack.splice(stack.length-1,1)" title="返回上一级" :disabled="!currId">
              <v-icon>arrow_upward</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
            <v-text-field append-icon="search" label="搜索" single-line hide-details v-model="search"></v-text-field>
          </v-card-title>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar class="media-content--warp" :class="[success?'dialog-page':'full-page']">
              <v-container fluid v-if="view ==='grid'">
                <v-layout row wrap="" class="x-grid-lg">
                  <GridItem v-for="item in orderItems" :key="item.Id" :item="item" :selected="currItem==item" :mode="view"
                    @click="handleClick(item)" @dblclick="handleDbClick(item)" />
                </v-layout>
              </v-container>
              <v-layout column v-else>
                <v-list dense class="transparent">
                  <ListItem v-for="item in orderItems" :key="item.Id" :item="item" :mode="view" :selected="currItem==item"
                    @click="handleClick(item)" @dblclick="handleDbClick(item)" />
                </v-list>
              </v-layout>
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="success">
            <v-btn flat @click="close">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="handleSuccess" :disabled="!currItem || currItem.IsFolder">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
  import VuePerfectScrollbar from 'vue-perfect-scrollbar'
  import GridItem from './GridItem.vue'
  import ListItem from './ListItem.vue'
  import {
    showDialog,
    alert,
    upload
  } from '@/utils'
  import Prompt from '@/components/Message/Prompt.vue'
  import rules from '@/rules'
  export default {
    inject: ['reload'],
    components: {
      VuePerfectScrollbar,
      ListItem,
      GridItem
    },
    props: {
      success: Function,
      close: Function
    },
    data() {
      return {
        moduleInfo: {
          name: 'Meidia'
        },
        stack: [null],
        items: [],
        view: 'grid',
        search: '',
        currItem: null
      }
    },
    computed: {
      currId() {
        return this.stack[this.stack.length - 1]
      },
      orderItems() {
        return this.items.sort(a => a.IsFolder)
      }
    },
    watch: {
      currId(val) {
        this.loadList(val)
      }
    },
    created() {
      this.loadList(null)
    },
    methods: {
      refrsh() {
        this.reload()
      },
      handleClick(item) {
        this.currItem = item
      },
      handleDbClick({
        Id,
        IsFolder
      }) {
        if (IsFolder) {
          this.currItem = null
          this.stack.push(Id)
        }
      },
      async loadList(val) {
        let url = `/api/Meidia/Meidias/${val}`
        this.items = await this.$http.get(url)
      },
      async upload() {
        let accept = 'image/gif, image/jpeg'
        let [resource] = await upload({
          accept
        })

        let postData = {
          Name: resource.Name,
          parent_Id: this.currId,
          Resource_Id: resource.Id,
          IsFolder: false
        }
        let entity = await this.$http.post('/api/Meidia/post', postData)
        this.items.push(entity)
        alert.success('添加成功!')
      },
      async addFolder() {
        let {
          name
        } = await showDialog(Prompt, {
          title: '文件夹名称',
          maxWidth: '500px',
          options: [{
            Name: 'name',
            Type: 'String',
            Description: '文件夹名称',
            rules: [rules.required('文件夹名称')]
          }]
        })
        let postData = {
          Name: name,
          IsFolder: true,
          parent_Id: this.currId
        }
        let entity = await this.$http.post('/api/Meidia/post', postData)
        this.items.push(entity)
        alert.success('添加成功!')
      },
      async handleDelete() {
        await this.$message.confirm("提示", "确认要删除吗?");
        let {
          Id
        } = this.currItem
        let index = this.items.findIndex(r => r == this.currItem)
        await this.$http.delete(`/api/meidia/delete/${Id}`)
        alert.success('删除成功')
        this.currItem = null
        this.items.splice(index, 1)
      },
      handleSuccess() {
        this.$emit('success', [this.currItem])
      }
    }
  }
</script>

<style lang="stylus" scoped>
  .media {

    &-cotent--wrap,
    &-menu {
      min-width: 260px;
      border-right: 1px solid #eee;
      /* min-height: calc(100vh - 50px - 64px); */
    }

    &-detail {
      min-width: 300px;
      border-left: 1px solid #eee;
    }
  }

  .full-page {
    height: calc(100vh - 200px);
    overflow: auto;
  }

  .dialog-page {
    height: calc(100vh - 274px);
    overflow: auto;
  }
</style>