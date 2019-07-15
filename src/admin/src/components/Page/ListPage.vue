<template>
  <v-container grid-list-xl fluid app>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{direction}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <span class="hidden-sm-and-down btn-group">
              <a-btn
                v-for="btn in items1"
                :key="btn.key || btn.name"
                :title="btn.title"
                :moduleName="moduleName"
                :disabled="evalDisabled(btn)"
                @click="evalAction(btn)"
                color="primary"
                flat
                small
              >
                <span>{{btn.title}}</span>
              </a-btn>
            </span>
            <!-- <span> -->
            <v-menu offset-y :close-on-content-click="false">
              <v-btn
                slot="activator"
                title="更多"
                small
                flat
                :color="$vuetify.breakpoint.smAndDown?'':'success'"
                :icon="$vuetify.breakpoint.smAndDown"
              >
                <v-icon>more_vert</v-icon>
                <span v-if="!$vuetify.breakpoint.smAndDown">更多</span>
              </v-btn>
              <v-list two-line dense expand>
                <v-list-tile
                  v-for="item in [...($vuetify.breakpoint.smAndDown?items:items2)]"
                  :key="item.key || item.name"
                  :title="item.title"
                  :disabled="evalDisabled(item)"
                  @click="evalAction(item)"
                >
                  <v-list-tile-action>
                    <v-icon>{{item.icon}}</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>{{item.title}}</v-list-tile-content>
                </v-list-tile>
                <v-list-tile
                  v-if="!$vuetify.breakpoint.smAndDown && !isDialog"
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
                  @click="$emit('changeShowMamageField')"
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
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <vue-perfect-scrollbar :class="[isDialog?'dialog-page':'full-page']">
              <Table
                :items="rows"
                :totalItems="total"
                :loading="loading"
                :columns="columns"
                rowKey="Id"
                :value="selection"
                :hidePager="hidePager"
                :multiple="!singleSelection"
                :classArr="tableClassArr"
                :styleObj="tableStyleObj"
                v-on="tableListenter"
              />
            </vue-perfect-scrollbar>
          </v-card-text>
          <v-card-actions v-if="isDialog">
            <v-btn flat @click="$emit('close')">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="$emit('success')">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Cell from "@/components/Table/Cell.vue";
import Table from "@/components/Table/DataTable.vue";
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import { skip, take } from "@/utils";

export default {
  components: {
    Cell,
    Table,
    VuePerfectScrollbar
  },
  props: {
    toolItems: {
      type: Array,
      default: () => []
    },
    toolSpliceCount: Number,
    moduleName: String,
    direction: String,
    isDialog: Boolean,
    columns: {
      type: Array,
      default: () => []
    },
    rows: {
      type: Array,
      default: () => []
    },
    selection: {
      type: Array,
      default: () => []
    },
    loading: Boolean,
    tableClassArr: Array,
    tableStyleObj: Object,
    total: Number,
    singleSelection: Boolean,
    hidePager: Boolean,
    showMamageField: Boolean,
    ModuleStrut: {
      type: Object,
      default: () => ({})
    }
  },
  computed: {
    context() {
      return {
        selection: this.selection,
        rows: this.rows
      };
    },
    items() {
      return this.toolItems.filter(this.evalVisible);
    },
    items1() {
      return take(this.items, this.toolSpliceCount);
    },
    items2() {
      return skip(this.items, this.toolSpliceCount);
    },
    tableListenter() {
      return {
        ...this.$listeners,
        input: val => this.$emit("selection_update", val)
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
  methods: {
    evalVisible({ visible }) {
      let val = true;
      if (typeof visible == "function") val = visible.call(this, this.context);
      if (typeof visible == "boolean") val = visible;
      if (typeof visible == "string") val = !!visible;

      return val;
    },
    evalDisabled({ disabled, name }) {
      let val = false;
      if (typeof disabled == "function")
        val = disabled.call(this, this.context);
      if (typeof disabled == "boolean") val = disabled;
      if (typeof disabled == "string") val = !!disabled;

      return (
        val || !this.$store.getters.existsPermission(this.moduleName, name)
      );
    },
    evalAction(item) {
      this.$emit(`toolItemClick`, item);
    }
  },
  watch: {}
};
</script>

<style scoped lang="stylus">
.btn-group .v-btn {
  // padding: 0px;
  // margin: 1px;
}

.btn-group {
  .v-btn {
    margin: 1px;
    // padding: 1px;
    min-width: 50px;
  }

  button, html [type='button'], [type='reset'], [type='submit'] {
    margin: 1px;
  }
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