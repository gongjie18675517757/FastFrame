<template>
  <v-container grid-list-xl fluid app>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{moduleInfo.direction}}列表</v-toolbar-title>
            <!-- <v-spacer></v-spacer> -->
            <a-btn @click="toEdit({})" icon title="新增" :moduleName="moduleInfo.name" name="Add">
              <v-icon>add</v-icon>
            </a-btn>
            <v-btn icon title="搜索" @click="search">
              <v-icon>search</v-icon>
            </v-btn>
            <a-btn
              :moduleName="moduleInfo.name"
              name="Update"
              icon
              title="修改"
              :disabled="selection.length!=1 && !currentRow"
              @click="toEdit(currentRow || selection[0])"
            >
              <v-icon>edit</v-icon>
            </a-btn>
            <a-btn
              @click="remove()"
              :disabled="!havSelection"
              icon
              title="删除"
              :moduleName="moduleInfo.name"
              name="Delete"
            >
              <v-icon>delete</v-icon>
            </a-btn>
            <a-btn
              v-for="item in toolitems.filter(r=>evalShow(r))"
              :key="item.name"
              icon
              :title="item.title"
              :moduleName="moduleInfo.name"
              :name="item.name"
              :disabled="evalDisabled(item)"
              @click="evalAction(item)"
            >
              <v-icon>{{item.icon}}</v-icon>
            </a-btn>
            <v-btn icon @click="loadList" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
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
          <v-card-text class="pa-0">
            <v-layout row wrap>
              <v-flex v-if="TreeKey" :xs3="TreeKey">
                <vue-perfect-scrollbar :class="[this.pageInfo.success?'dialog-page':'full-page']">
                  <v-treeview
                    :active.sync="tree.active"
                    :items="tree.items"
                    :load-children="loadTreeItems"
                    :open.sync="tree.open"
                    activatable
                    active-class="primary--text"
                    class="grey lighten-5"
                    item-text="Name"
                    item-key="Id"
                    transition
                    @update:active="setCurrentTreeNode"
                  >
                    <v-icon
                      v-if="!item.children.length"
                      slot="prepend"
                      slot-scope="{ item, active }"
                      :color="active ? 'primary' : ''"
                    >mdi-account</v-icon>
                  </v-treeview>
                </vue-perfect-scrollbar>
              </v-flex>
              <v-flex :xs9="TreeKey">
                <vue-perfect-scrollbar :class="[this.pageInfo.success?'dialog-page':'full-page']">
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
                      <tr
                        :active="!singleSelection && props.selected"
                        @click="handleRowClick(props)"
                      >
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
                            :value="props.selected"
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
                          <Cell
                            :info="col"
                            :model="props.item"
                            @toEdit="toEdit(props.item)"
                            :moduleName="moduleInfo.name"
                          />
                        </td>
                      </tr>
                    </template>
                    <template slot="no-data">没有加载数据</template>
                  </v-data-table>
                </vue-perfect-scrollbar>
              </v-flex>
            </v-layout>
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
import { getColumns, getModuleStrut } from "@/generate";
import { showDialog } from "@/utils";
import { getComponent } from "@/router";
import Cell from "@/components/Table/Cell.vue";
import VuePerfectScrollbar from "vue-perfect-scrollbar";
export default {
  components: {
    Cell,
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
      currentRow: null,
      pager: {},
      loading: false,
      total: 0,

      showMamageField: false,
      cols: [],
      items: [],

      RelateFields: [],
      TreeKey: null,
      tree: {
        active: [],
        items: [],
        open: []
      }
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
    headers() {
      return [
        {
          text: "#",
          sortable: false,
          width: "50px"
        },
        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name
          };
        })
      ];
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
  async created() {
    let { TreeKey, RelateFields } = await getModuleStrut(this.moduleInfo.name);
    this.TreeKey = TreeKey;
    this.RelateFields = RelateFields;
    this.cols = await getColumns(this.moduleInfo.name);

    if (TreeKey) {
      // this.tree.items = await this.loadTreeItems({})
      this.tree.items = await this.loadTreeItems({});
    }
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
    search() {},
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
    handleRowClick(props) {
      if (this.singleSelection) {
        this.currentRow = props.item;
      } else {
        props.selected = !props.selected;
      }
    },
    toEdit({ Id = "" } = {}) {
      let url = `${this.path.add}?q=${Id}`;
      let { name } = this.moduleInfo;
      if (this.dialogMode) {
        let component = getComponent(`${name}_add`);
        showDialog(component, {
          id: Id
        });
        // let index = this.items.findIndex(x => x.Id == data.Id);
        // if (index != -1) {
        //   this.items.splice(index, 1, data);
        // } else {
        //   this.items.splice(0, 0, data);
        // }
      } else {
        this.$router.push(url);
      }
    },
    async remove() {
      await this.$message.confirm("提示", "确认要删除吗?");
      let ids = [];
      if (this.singleSelection) {
        ids = [this.currentRow.Id];
      } else {
        ids = this.selection.map(r => r.Id);
      }
      for (const id of ids) {
        try {
          await this.$http.delete(`/api/${this.moduleInfo.name}/delete/${id}`);
          let index = this.items.findIndex(r => r.Id == id);
          // this.items.splice(index, 1);
          index = this.selection.findIndex(r => r.Id == id);
          this.selection.splice(index, 1);
          if (this.currentRow && this.currentRow.Id == id)
            this.currentRow = null;
        } catch (err) {
          window.console.error(err);
        }
      }
    },
    async loadList() {
      this.loading = true;
      let { page, rowsPerPage, sortBy, descending } = this.pager;

      /*条件1 */
      let { queryFilter = [] } = this.pageInfo.pars || {};
      if (typeof queryFilter == "function") {
        queryFilter = await queryFilter.call(this, this.context);
      }

      /*条件2 */
      let treeFilter = [];
      if (this.TreeKey && this.tree.active.length > 0) {
        let parentId = this.tree.active[this.tree.active.length - 1];
        if (parentId) {
          treeFilter = [
            {
              Name: this.TreeKey,
              Compare: "==",
              Value: parentId
            }
          ];
        }
      }
      let pageInfo = {
        PageIndex: page,
        PageSize: rowsPerPage,
        Condition: {
          KeyWord: this.search,
          Filters: [...queryFilter, ...treeFilter]
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
    async loadTreeItems(item) {
      let { Data } = await this.$http.post(
        `/api/${this.moduleInfo.name}/List`,
        {
          PageIndex: 1,
          PageSize: 999,
          Condition: {
            Filters: [
              {
                Name: this.TreeKey,
                Compare: "==",
                Value: item.Id ? item.Id : "null"
              }
            ]
          }
        }
      );
      Data.forEach(r => (r.children = []));
      if (item.children) item.children.push(...Data);
      return Data;
    },
    setCurrentTreeNode() {
      this.loadList();
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
  height: calc(100vh - 205px);
  overflow: auto;
}

.dialog-page {
  height: calc(100vh - 265px);
  overflow: auto;
}
</style>