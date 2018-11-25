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
            <v-btn icon @click="refrsh" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
            <v-text-field
              append-icon="search"
              label="搜索"
              single-line
              hide-details
              v-model="search"            
            ></v-text-field>
          </v-card-title>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar class="media-content--warp">
              <v-container fluid v-if="view ==='grid'">
                <v-layout row wrap="" class="x-grid-lg">
                  <GridItem v-for="item in filterItms" :key="item.Id" :item="item"/>
                </v-layout>
              </v-container>
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="success">
            <v-btn flat @click="close">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="handleSuccess">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from 'vue-perfect-scrollbar'
import GridItem from './GridItem.vue'
import { showDialog, alert } from '@/utils'
import Prompt from '@/components/Message/Prompt.vue'
import rules from '@/rules'
export default {
  inject: ['reload'],
  components: {
    VuePerfectScrollbar,
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
      search: ''
    }
  },
  computed: {
    currId() {
      return this.stack[this.stack.length - 1]
    },
    filterItms() {
      let key = this.search.toLowerCase()
      return this.items.filter(r => r.Name.toLowerCase().includes(key))
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
    async loadList(val) {
      let url = `/api/Meidia/Meidias/${val}`
      this.items = await this.$http.get(url)
    },
    upload() {},
    async addFolder() {
      let { name } = await showDialog(Prompt, {
        title: '文件夹名称',
        maxWidth: '500px',
        options: [
          {
            Name: 'name',
            Type: 'String',
            Description: '文件夹名称',
            rules: [rules.required('文件夹名称')]
          }
        ]
      })
      let postData = {
        Name: name,
        IsFolder: true
      }
      let entity = await this.$http.post('/api/Meidia/post', postData)
      this.items.splice(0, 0, entity)
      alert.success('添加成功!')
    },
    handleSuccess() {}
  }
}
</script>

<style lang="stylus" scoped>
.media {
  &-cotent--wrap, &-menu {
    min-width: 260px;
    border-right: 1px solid #eee;
    min-height: calc(100vh - 50px - 64px);
  }

  &-detail {
    min-width: 300px;
    border-left: 1px solid #eee;
  }
}
</style>
