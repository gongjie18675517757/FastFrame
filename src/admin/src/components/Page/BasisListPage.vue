<template>
  <v-container grid-list-xl fluid app>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{moduleInfo.direction}}列表</v-toolbar-title>
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
            <!-- <span> -->
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
                  v-for="item in [...($vuetify.breakpoint.smAndDown?basisBtns:[]),...toolitems].filter(r=>evalShow(r))"
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
                <v-list-tile
                  v-if="!$vuetify.breakpoint.smAndDown && !pageInfo.success"
                  @click="dialogMode=!dialogMode"
                >
                  <v-list-tile-action>
                    <v-checkbox :value="dialogMode"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>弹出模式</v-list-tile-title>
                    <v-list-tile-sub-title>是否使用表单窗口</v-list-tile-sub-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-list-tile
                  v-if="this.ModuleStrut.HasManage"
                  @click="showMamageField=!showMamageField"
                >
                  <v-list-tile-action>
                    <v-checkbox :value="showMamageField"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>显示管理字段</v-list-tile-title>
                    <v-list-tile-sub-title>录入人,修改人,录入时间,修改时间</v-list-tile-sub-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-menu>
            <v-btn icon @click="pageInfo.close" title="关闭" v-if="pageInfo.success">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar :class="[this.pageInfo.success?'dialog-page':'full-page']">
              <Table
                :items="items"
                :totalItems="total"
                :loading="loading"
                :columns="columns"
                @loadList="loadList"
                @toEdit="toEdit"
                rowKey="Id"
                v-model="selection"
                :hidePager="false"
                :multiple="!singleSelection"
              />
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="pageInfo.success">
            <v-btn flat @click="pageInfo.close">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="success">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import { getColumns, getModuleStrut, getQueryOptions } from "@/generate";
import { showDialog } from "@/utils";
import { getComponent } from "@/router";
import Cell from "@/components/Table/Cell.vue";
import Table from "@/components/Table/DataTable.vue";
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import SearchDialogVue from "@/components/Dialog/SearchDialog.vue";

export default {
  components: {
    Cell,
    Table,
    VuePerfectScrollbar
  },
  props: {
    moduleInfo: {
      type: Object,
      default: function() {
        return {
          area: "",
          name: "",
          direction: ""
        };
      }
    },
    pageInfo: {
      type: Object,
      default: function() {
        return {};
      }
    }
  },
  data() {
    return {
      selection: [],
      total: 0,
      loading: false,
      showMamageField: false,
      cols: [],
      items: [],
      query: [],

      ModuleStrut: {},
      basisBtns: [
        {
          title: "新增",
          color: "success",
          name: "Add",
          icon: "add",
          action() {
            this.toEdit({});
          },
          show() {
            return this.ModuleStrut.HasManage;
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
          title: "修改",
          color: "warning",
          name: "Update",
          icon: "search",
          action({ selection }) {
            this.toEdit(selection[0]);
          },
          disabled({ selection }) {
            return selection.length != 1;
          },
          show() {
            return this.ModuleStrut.HasManage;
          }
        },
        {
          title: "删除",
          color: "warning",
          name: "Delete",
          icon: "delete",
          action({ selection }) {
            this.remove(selection);
          },
          disabled({ selection }) {
            return selection.length == 0;
          }
          // show() {
          //   return this.ModuleStrut.HasManage;
          // }
        },
        {
          title: "刷新",
          color: "success",
          name: "List",
          icon: "refresh",
          action() {
            this.loadList();
          }
        }
      ]
    };
  },
  computed: {
    path() {
      return {
        add: `/${this.moduleInfo.name}/add`
      };
    },
    singleSelection() {
      return this.pageInfo.pars && this.pageInfo.pars.single;
    },
    columns() {
      let adminColumns = [
        // { Name: 'Create_User.Account', Description: '创建人' },
        // { Name: 'CreateTime', Description: '创建时间' },
        // { Name: 'Modify_User.Account', Description: '修改人' },
        // { Name: 'ModifyTime', Description: '修改时间' }
      ];
      return [...this.cols, ...(this.showMamageField ? adminColumns : [])];
    },
    havSelection() {
      return this.selection.length > 0 || !!this.currentRow;
    },
    toolitems() {
      return this.moduleInfo.toolItems || [];
    },
    context() {
      let selection = [];
      if (this.singleSelection && this.currentRow)
        selection = [this.currentRow];
      else selection = this.selection;
      return {
        selection,
        rows: this.items
      };
    },
    dialogMode: {
      get() {
        return this.$store.state.dialogMode;
      },
      set(val) {
        this.$store.state.dialogMode = val;
      }
    }
  },
  created() {
    getModuleStrut(this.moduleInfo.name).then(ModuleStrut => {
      this.ModuleStrut = ModuleStrut;
    });

    getColumns(this.moduleInfo.name).then(cols => {
      this.cols = cols;
    });
  },
  mounted() {
    this.$eventBus.$on(`${this.moduleInfo.name}_DataAdded`, this.DataAdded);
    this.$eventBus.$on(`${this.moduleInfo.name}_DataUpdated`, this.DataUpdated);
    this.$eventBus.$on(`${this.moduleInfo.name}_DataDeleted`, this.DataDeleted);
  },
  destroyed() {
    this.$eventBus.$off(`${this.moduleInfo.name}_DataAdded`, this.DataAdded);
    this.$eventBus.$off(
      `${this.moduleInfo.name}_DataUpdated`,
      this.DataUpdated
    );
    this.$eventBus.$off(
      `${this.moduleInfo.name}_DataDeleted`,
      this.DataDeleted
    );
  },
  methods: {
    DataDeleted(id) {
      this.$nextTick(() => {
        let index = this.items.findIndex(r => r.Id == id);
        if (index >= 0) this.items.splice(index, 1);
      });
    },
    DataUpdated(item) {
      let index = this.items.findIndex(r => r.Id == item.Id);
      if (index >= 0) this.items.splice(index, 1, item);
    },
    DataAdded(item) {
      this.items.splice(0, 0, item);
    },
    search() {
      getQueryOptions(this.moduleInfo.name).then(options => {
        for (const item of this.query) {
          let opt = options.find(
            r => r.Name == item.Name && r.compare == item.compare
          );
          if (opt) {
            opt.value = item.value;
          }
        }

        this.$message
          .dialog(SearchDialogVue, {
            title: `查询${this.moduleInfo.direction}列表`,
            options
          })
          .then(query => {
            this.query = query;
            this.loadList();
          });
      });
    },
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
    toEdit({ Id = "" } = {}) {
      let url = `${this.path.add}?q=${Id}`;
      let { name } = this.moduleInfo;
      if (
        (this.dialogMode && !this.$vuetify.breakpoint.smAndDown) ||
        this.pageInfo.success
      ) {
        let component = getComponent(`${name}_add`);
        showDialog(component, {
          id: Id
        });
      } else {
        this.$router.push(url);
      }
    },
    remove() {
      this.$message.confirm("提示", "确认要删除吗?").then(() => {
        let ids = [];
        if (this.singleSelection) {
          ids = [this.currentRow.Id];
        } else {
          ids = this.selection.map(r => r.Id);
        }
        for (const id of ids) {
          let index = this.selection.findIndex(r => r.Id == id);
          this.$http
            .delete(`/api/${this.moduleInfo.name}/delete/${id}`)
            .then(() => {
              this.selection.splice(index, 1);
              if (this.currentRow && this.currentRow.Id == id)
                this.currentRow = null;
            });
        }
      });
    },
    async loadList(val) {      
      this.loading = true;
      let { page, rowsPerPage, sortBy, descending } = val;

      /*条件1 */
      let { queryFilter = [] } = this.pageInfo.pars || {};
      if (typeof queryFilter == "function") {
        queryFilter = await queryFilter.call(this, this.context);
      }

      let pageInfo = {
        PageIndex: page,
        PageSize: rowsPerPage,
        Condition: {
          KeyWord: this.search,
          Filters: [...queryFilter, ...this.query]
        },
        SortInfo: {
          Name: sortBy || "Id",
          Mode: descending ? "asc" : "desc"
        }
      };
      try {
        let { Total, Data } = await this.$http.post(
          "/api/" + this.moduleInfo.name + "/list",
          pageInfo
        );
        this.items = Data;
        this.total = Total;
      } finally {
        this.loading = false;
      }
    },
    success() {
      let selection = [];
      if (this.singleSelection) {
        selection = [this.currentRow];
      } else {
        selection = this.selection;
      }
      this.$emit("success", selection);
      this.pageInfo.success(selection);
    }
  },
  watch: {}
};
</script>

<style scoped lang="stylus">
.btn-group .v-btn {
  padding: 0px;
  margin: 1px;
}

.selection {
  background: #eee;
}

.full-page {
  height: calc(100vh - 140px);
  overflow: auto;
}

.dialog-page {
  height: calc(100vh - 255px);
  overflow: auto;
}

.theme--dark.v-table thead th {
  background-color: #424242;
}

.theme--light.v-table thead th {
  background-color: #ffffff;
}
</style>