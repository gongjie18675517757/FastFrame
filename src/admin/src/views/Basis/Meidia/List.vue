<template>
  <v-container grid-list-xl fluid app class="media">
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>资源列表</v-toolbar-title>
            <v-spacer></v-spacer>
            <span class="hidden-sm-and-down">
              <a-btn
                v-for="btn in basisBtns"
                :key="`${btn.title}_${btn.name}`"
                :title="btn.title"
                :color="btn.color"
                :moduleName="moduleInfo.name"
                :disabled="evalDisabled(btn)"
                @click="evalAction(btn)"
                small
              >
                <v-icon>{{btn.icon}}</v-icon>
                <span>{{btn.title}}</span>
              </a-btn>
            </span>
            <v-menu offset-y :close-on-content-click="false">
              <v-btn
                slot="activator"
                title="设置"
                small
                :color="$vuetify.breakpoint.smAndDown?'':'success'"
                :icon="$vuetify.breakpoint.smAndDown"
              >
                <v-icon>more_vert</v-icon>
                <span v-if="!$vuetify.breakpoint.smAndDown">更多</span>
              </v-btn>
              <v-list two-line dense expand>
                <v-list-tile
                  v-for="item in [...($vuetify.breakpoint.smAndDown?basisBtns:[])].filter(r=>evalShow(r))"
                  :key="`${item.title}_${item.name}`"
                  :title="item.title"
                  :moduleName="moduleInfo.name"
                  :name="item.name"
                  :disabled="evalDisabled(item)"
                  @click="evalAction(item)"
                >
                  <v-list-tile-action>
                    <v-icon>{{item.icon}}</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>{{item.title}}</v-list-tile-content>
                </v-list-tile>

                <v-list-tile @click="view='grid'">
                  <v-list-tile-action>
                    <v-icon>view_headline</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>详细模式</v-list-tile-content>
                </v-list-tile>
                <v-list-tile @click="view='list'">
                  <v-list-tile-action>
                    <v-icon>view_list</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>列表模式</v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-menu>
            <v-btn icon @click="close" title="关闭" v-if="success">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-progress-linear color="secondary" v-if="uploading" height="2" :value="uploadValue"></v-progress-linear>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar
              class="media-content--warp"
              :class="[success?'dialog-page':'full-page']"
            >
              <v-container fluid>
                <v-layout row wrap v-if="view ==='grid'">
                  <GridItem
                    v-for="item in orderItems"
                    :key="item.Id"
                    :item="item"
                    :selected="currItem==item"
                    :mode="view"
                    @click="handleClick(item)"
                    @dblclick="handleDbClick(item)"
                  />
                </v-layout>
                <v-layout column v-else>
                  <v-list dense class="transparent">
                    <ListItem
                      v-for="item in orderItems"
                      :key="item.Id"
                      :item="item"
                      :mode="view"
                      :selected="currItem==item"
                      @click="handleClick(item)"
                      @dblclick="handleDbClick(item)"
                    />
                  </v-list>
                </v-layout>
              </v-container>
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="success">
            <v-btn flat @click="close">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn
              color="primary"
              flat
              @click="handleSuccess"
              :disabled="!currItem || currItem.IsFolder"
            >确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import GridItem from "./GridItem.vue";
import ListItem from "./ListItem.vue";
import { showDialog, alert, upload } from "@/utils";
import Prompt from "@/components/Message/Prompt.vue";
import rules from "@/rules";
export default {
  inject: ["reload"],
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
        name: "Meidia"
      },
      items: [],
      view: "grid",
      currItem: null,
      curr: null,
      uploading: false,
      uploadValue: 0,
      basisBtns: [
        {
          title: "上传文件",
          color: "success",
          name: "Add",
          icon: "cloud_upload",
          action() {
            this.upload();
          },
          disabled() {
            return this.uploading;
          }
        },
        {
          title: "搜索",
          color: "info",
          name: "List",
          icon: "search",
          action() {
            this.search();
          }
        },
        {
          title: "创建文件夹",
          color: "success",
          name: "Add",
          icon: "folder",
          action() {
            this.addFolder();
          }
        },
        {
          title: "删除",
          color: "error",
          name: "Delete",
          icon: "delete",
          action() {
            this.handleDelete();
          },
          disabled({ selection }) {
            return selection.length == 0;
          }
        },
        {
          title: "刷新",
          color: "info",
          name: "List",
          icon: "refresh",
          action() {
            this.refrsh();
          }
        },
        {
          title: "返回上一级",
          color: "info",
          name: "List",
          icon: "arrow_upward",
          action() {
            // this.stack.splice(this.stack.length - 1, 1);
            this.$router.push(`/meidia/list?id=${this.curr.Parent_Id}`);
          },
          disabled() {
            return !this.curr;
          }
        }
      ]
    };
  },
  computed: {
    orderItems() {
      return this.items;
    },
    context() {
      return {
        selection: this.currItem ? [this.currItem] : [],
        items: this.items
      };
    }
  },
  watch: {
    $route({ query }) {
      this.loadList(query.id || null, query.v);
    }
  },
  created() {
    this.loadList(this.$route.query.id || null);
  },
  methods: {
    evalShow({ show }) {
      let val = true;
      if (typeof show == "function") val = show.call(this, this.context);
      if (typeof show == "boolean") val = show;
      if (typeof show == "string") val = !!show;

      return val;
    },
    evalDisabled({ disabled }) {
      let val = false;
      if (typeof disabled == "function")
        val = disabled.call(this, this.context);
      if (typeof disabled == "boolean") val = disabled;
      if (typeof disabled == "string") val = !!disabled;

      return val;
    },
    evalAction({ action }) {
      if (typeof action == "function") action.call(this, this.context);
    },
    refrsh() {
      this.$router.push("/meidia/list");
    },
    handleClick(item) {
      this.currItem = item;
    },
    handleDbClick({ Id, IsFolder }) {
      if (IsFolder) {
        this.currItem = null;
        // this.stack.push(Id);
        this.$router.push(`/meidia/list?id=${Id}`);
      }
    },
    async loadList(val = "null", keyword = "") {
      let url = `/api/Meidia/Meidias/${val}?v=${keyword}`;
      let { Curr, Children } = await this.$http.get(url);
      this.curr = Curr;
      this.items = Children;
    },
    async upload() {
      let accept = "image/gif, image/jpeg";
      try {
        let [resource] = await upload({
          accept: "",
          onStart: () => {
            this.uploading = true;
            this.uploadValue = 0;
          },
          onProgress: e => {
            this.uploading = true;
            this.uploadValue = e;
          }
        });

        let postData = {
          Name: resource.Name,
          parent_Id: (this.curr || {}).Id,
          Resource_Id: resource.Id,
          IsFolder: false
        };
        let entity = await this.$http.post("/api/Meidia/post", postData);
        this.items.push(entity);
        alert.success("添加成功!");
      } finally {
        this.uploading = false;
      }
    },
    async addFolder() {
      let { name } = await showDialog(Prompt, {
        title: "文件夹名称",
        maxWidth: "500px",
        options: [
          {
            Name: "name",
            Type: "String",
            Description: "文件夹名称",
            rules: [rules.required("文件夹名称")]
          }
        ]
      });
      let postData = {
        Name: name,
        IsFolder: true,
        parent_Id: (this.curr || {}).Id
      };
      let entity = await this.$http.post("/api/Meidia/post", postData);
      this.items.push(entity);
      alert.success("添加成功!");
    },
    search() {
      showDialog(Prompt, {
        title: "文件夹名称",
        maxWidth: "500px",
        options: [
          {
            Name: "name",
            Type: "String",
            IsRequired: true,
            Description: "搜索关键字",
            rules: [rules.required("搜索关键字")]
          }
        ]
      }).then(({ name }) => {
        this.$router.push(`/meidia/list?v=${name}`);
      });
    },
    async handleDelete() {
      await this.$message.confirm("提示", "确认要删除吗?");
      let { Id } = this.currItem;
      let index = this.items.findIndex(r => r == this.currItem);
      await this.$http.delete(`/api/meidia/delete/${Id}`);
      alert.success("删除成功");
      this.currItem = null;
      this.items.splice(index, 1);
    },
    handleSuccess() {
      this.$emit("success", [this.currItem]);
    }
  }
};
</script>

<style lang="stylus" scoped>
.media {
  &-cotent--wrap, &-menu {
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
  height: calc(100vh - 140px);
  overflow: auto;
}

.dialog-page {
  height: calc(100vh - 214px);
  overflow: auto;
}
</style>