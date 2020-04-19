<template>
  <v-container grid-list-xl fluid app class="media">
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense color="transparent" height="30px">
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
                text
              >
                <v-icon>{{btn.icon}}</v-icon>
                <span>{{btn.title}}</span>
              </a-btn>
            </span>
            <v-menu offset-y>
              <template v-slot:activator="{ on }">
                <v-btn v-on="on" title="设置" small icon>
                  <v-icon>more_vert</v-icon>
                </v-btn>
              </template>
              <v-list dense>
                <v-list-item
                  v-for="item in [...($vuetify.breakpoint.smAndDown?basisBtns:[])].filter(r=>evalShow(r))"
                  :key="`${item.title}_${item.name}`"
                  :title="item.title"
                  :moduleName="moduleInfo.name"
                  :name="item.name"
                  :disabled="evalDisabled(item)"
                  @click="evalAction(item)"
                >
                  <v-list-item-action>
                    <v-icon>{{item.icon}}</v-icon>
                  </v-list-item-action>
                  <v-list-item-content>{{item.title}}</v-list-item-content>
                </v-list-item>

                <v-list-item @click="view='grid'">
                  <v-list-item-action>
                    <v-icon>view_headline</v-icon>
                  </v-list-item-action>
                  <v-list-item-content>详细模式</v-list-item-content>
                </v-list-item>
                <v-list-item @click="view='list'">
                  <v-list-item-action>
                    <v-icon>view_list</v-icon>
                  </v-list-item-action>
                  <v-list-item-content>列表模式</v-list-item-content>
                </v-list-item>
              </v-list>
            </v-menu>
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-progress-linear color="secondary" v-if="uploading" height="2" :value="uploadValue"></v-progress-linear>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar
              class="media-content--warp"
              :class="[isTab?'tab-page':isDialog?'dialog-page':'full-page']"
            >
              <v-container fluid v-if="items.length">
                <v-row v-if="view ==='grid'">
                  <v-col v-for="item in orderItems" :key="item.name" cols="12" sm="4" md="3" lg="2">
                    <GridItem
                      :item="item"
                      :selected="currItem==item"
                      :mode="view"
                      @click="handleClick(item)"
                      @dblclick="handleDbClick(item)"
                      @remove="handleRemove"
                      @reName="handleReName"
                      @download="handleDownload"
                      @preview="handlePreview"
                    >
                      <!-- <template v-slot:action="item"></template> -->
                    </GridItem>
                  </v-col>
                </v-row>

                <v-layout column v-else>
                  <v-list dense class="transparent" width="800px">
                    <v-list-item-group v-model="currItem">
                      <ListItem
                        v-for="item in orderItems"
                        :key="item.Id"
                        :item="item"
                        :mode="view"
                        :selected="currItem==item"
                        @click="handleClick(item)"
                        @dblclick="handleDbClick(item)"
                        @remove="handleRemove"
                        @reName="handleReName"
                        @download="handleDownload"
                        @preview="handlePreview"
                      />
                    </v-list-item-group>
                  </v-list>
                </v-layout>
              </v-container>
              <v-container fluid v-else style="height: 100%;">
                <v-row align="center" justify="center" style="height: 100%;">
                  <a @click="addFolder">创建文件夹</a>
                  <a @click="upload" style="padding-left:15px;">上传文件</a>
                </v-row>
              </v-container>
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="isDialog">
            <v-btn text @click="$emit('close')">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="handleSuccess" :disabled="!currItem">确认</v-btn>
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
import { upload } from "../../../utils";
import fileIcons from "./fileIcons";
import rules from "@/rules";
export default {
  components: {
    VuePerfectScrollbar,
    ListItem,
    GridItem
  },
  props: {
    isDialog: Boolean,
    isTab: Boolean,
    p: String, //上级
    s: String //搜索
  },
  data() {
    return {
      moduleInfo: {
        name: "Meidia"
      },
      items: [],
      view: "grid",
      currItem: null,
      Super_Id: null, //上级文件夹
      current_Id: this.p, //当前文件夹
      kw: this.s,
      uploading: false,
      uploadValue: 0,
      basisBtns: [
        {
          title: "返回上一级",
          color: "info",
          name: "List",
          icon: "arrow_upward",
          action() {
            this.current_Id = this.Super_Id;
            this.loadList();
          },
          disabled() {
            return !this.current_Id;
          }
        },
        {
          title: "上传文件",
          color: "success",
          name: "Add",
          text: true,
          icon: "cloud_upload",
          action() {
            this.upload();
          },
          disabled() {
            return this.uploading || this.kw || this.isDialog;
          }
        },
        {
          title: "搜索",
          color: "info",
          name: "List",
          icon: "search",
          action() {
            this.search();
          },
          disabled() {
            return this.uploading;
          }
        },
        {
          title: "创建文件夹",
          color: "success",
          name: "Add",
          icon: "folder",
          action() {
            this.addFolder();
          },
          disabled() {
            return this.uploading || this.kw || this.isDialog;
          }
        },
        {
          title: "重置",
          color: "info",
          name: "List",
          icon: "refresh",
          action() {
            this.refrsh();
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
  created() {
    this.loadList();
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

      return !!val;
    },
    evalAction({ action }) {
      if (typeof action == "function") action.call(this, this.context);
    },
    refrsh() {
      this.kw = null;
      this.items = [];
      this.$nextTick(this.loadList);
    },
    handleClick(item) {
      this.currItem = item;
      let { Id, IsFolder } = item;
      if (IsFolder) {
        this.currItem = null;
        this.current_Id = Id;
        this.loadList();
      }
    },
    handleDbClick({ Id, IsFolder }) {
      if (IsFolder) {
        this.currItem = null;
        this.current_Id = Id;
        this.loadList();
      }
    },
    async loadList() {
      let url = `/api/Meidia/List/${this.current_Id || ""}?v=${this.kw || ""}`;
      let { Super_Id, Children } = await this.$http.get(url);
      this.Super_Id = Super_Id;
      this.items = Children.map(this.fmtItem);
    },
    fmtItem(v) {
      let isImage = v.ContentType && v.ContentType.startsWith("image/");
      v.isImage = isImage;
      v.icon = this.getIcon(v);
      return v;
    },
    getIcon({ ContentType, Resource_Id, isImage, IsFolder }) {
      let { folder, file, xlsx, pptx, js, txt, pdf } = fileIcons;
      if (isImage) {
        return `/api/resource/get/${Resource_Id}`;
      } else if (IsFolder) {
        return folder;
      } else if (!ContentType) {
        return file;
      } else {
        switch (ContentType) {
          case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
            return xlsx;
          case "application/vnd.ms-excel":
            return xlsx;
          case "application/vnd.ms-powerpoint":
            return pptx;

          case "text/javascript":
            return js;

          case "text/plain":
            return txt;

          case "application/pdf":
            return pdf;
          default:
            return file;
        }
      }
    },
    async upload() {
      // let accept = "image/gif, image/jpeg";
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
          Super_Id: this.current_Id,
          Resource_Id: resource.Id,
          IsFolder: false
        };
        let res = await this.$http.post("/api/Meidia/post", postData);
        this.items.push(
          this.fmtItem({
            ...res
          })
        );
      } finally {
        this.uploading = false;
      }
    },
    async addFolder() {
      let { name } = await this.$message.prompt({
        title: "文件夹名称",
        width: "500px",
        model: {
          name: null
        },
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
        Super_Id: this.current_Id
      };
      let Id = await this.$http.post("/api/Meidia/post", postData);
      this.items.push(
        this.fmtItem({
          ...postData,
          Id
        })
      );
    },
    async handleReName(item) {
      let dialog = this.$message.prompt({
        title: "名称",
        width: "500px",
        model: {
          name: item.Name
        },
        options: [
          {
            Name: "name",
            Type: "String",
            Description: "名称",
            rules: [rules.required("文件夹名称")]
          }
        ]
      });
      dialog.then(({ name }) => {
        if (name == item.Name) return;
        let beforeName = item.Name;
        item.Name = name;

        this.$http.put(`/api/Meidia/Put/${item.Id}?name=${name}`).catch(() => {
          item.Name = beforeName;
        });
      });
    },
    handleDownload({ IsFolder, Resource_Id, Name }) {
      if (IsFolder) return;
      window.open(`/api/resource/get/${Resource_Id}/${Name}`);
    },
    handlePreview({ isImage, icon }) {
      if (!isImage) return;
      let src = icon;
      this.$message.dialog({
        render(h) {
          let img = h("v-img", {
            props: {
              "lazy-src": src,
              src: src,
              "max-height": "80vh"
            },
            on: {
              click: () => {
                this.$emit("success");
              }
            }
          });

          return h(
            "v-layout",
            {
              props: {
                "grid-list-xl": true,
                fluid: true,
                app: true,
                "align-center": true,
                "justify-center": true
              }
            },
            [
              h(
                "v-flex",
                {
                  xs12: true
                },
                [img]
              )
            ]
          );
        }
      });
    },
    search() {
      this.$message
        .prompt({
          title: "文件/文件夹名称",
          width: "500px",
          model: {
            name: null
          },
          options: [
            {
              Name: "name",
              Type: "String",
              IsRequired: true,
              Description: "搜索关键字",
              rules: [rules.required("搜索关键字")]
            }
          ]
        })
        .then(({ name }) => {
          this.kw = name;
          this.loadList();
        });
    },
    handleRemove(item) {
      this.$message
        .confirm({
          title: "提示",
          content: "确认要删除吗?"
        })
        .then(() => {
          let confirmPromise = Promise.resolve();
          if (item.IsFolder) {
            confirmPromise = this.$message.confirm({
              title: "提示",
              content: "是否删除文件夹下的所有文件?"
            });
          }
          return confirmPromise;
        })
        .then(() => {
          let { Id } = item;
          let index = this.items.findIndex(r => r == item);
          this.$http.delete(`/api/meidia/delete/${Id}`).then(() => {
            if (item.IsFolder) {
              this.refrsh();
            } else {
              this.items.splice(index, 1);
            }
          });
        });
    },
    handleSuccess() {
      this.$emit("success", [this.currItem]);
    }
  }
};
</script>

<style lang="stylus" scoped>.full-page {
  height: calc(100vh - 140px);
  overflow: auto;
}

.dialog-page {
  height: calc(100vh - 214px);
  overflow: auto;
}

.tab-page {
  height: calc(100vh - 180px);
  overflow: auto;
}
</style>