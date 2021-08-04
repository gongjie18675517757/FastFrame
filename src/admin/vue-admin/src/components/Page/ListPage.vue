<template>
  <v-container grid-list-xl fluid app>
    <v-layout row wrap>
      <v-flex xs12 :style="{ padding: isTab ? '0px' : null }">
        <v-card tile>
          <v-toolbar flat dense height="30px" color="transparent">
            <v-toolbar-title>{{ direction }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items
              class="hidden-sm-and-down btn-group"
              v-if="!hideToolitem"
            >
              <permission-facatory
                v-for="btn in items1"
                :key="btn.key || btn.name"
                :permission="btn.permission"
              >
                <v-btn
                  @click="evalAction(btn)"
                  text
                  small
                  color="primary"
                  :disabled="evalDisabled(btn)"
                  v-bind="btn"
                >
                  <v-icon v-if="btn.iconName">{{ btn.iconName }}</v-icon>
                  {{ btn.title }}
                </v-btn>
              </permission-facatory>
            </v-toolbar-items>
            <!-- <span> -->
            <v-menu
              offset-y
              :close-on-content-click="false"
              v-if="!hideToolitem"
            >
              <template v-slot:activator="{ on }">
                <v-btn
                  slot="activator"
                  title="更多"
                  small
                  text
                  v-on="on"
                  :color="$vuetify.breakpoint.smAndDown ? '' : 'success'"
                  :icon="$vuetify.breakpoint.smAndDown"
                >
                  <v-icon>more_vert</v-icon>
                  <span v-if="!$vuetify.breakpoint.smAndDown">更多</span>
                </v-btn>
              </template>
              <v-list dense>
                <permission-facatory
                  v-for="item in [
                    ...($vuetify.breakpoint.smAndDown ? items : items2),
                  ]"
                  :key="item.key || item.name"
                  :permission="item.permission"
                >
                  <v-list-item
                    :title="item.title"
                    :disabled="evalDisabled(item)"
                    @click="evalAction(item)"
                  >
                    <v-list-item-action>
                      <v-icon>{{ item.iconName }}</v-icon>
                    </v-list-item-action>

                    <v-list-item-content>
                      <v-list-item-title>{{ item.title }}</v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>
                </permission-facatory>

    
                <v-list-item
                  v-if="this.ModuleStrut.HasManage"
                  @click="$emit('changeShowMamageField')"
                >
                  <v-list-item-action>
                    <v-checkbox :value="showMamageField"></v-checkbox>
                  </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>显示管理字段</v-list-item-title>
                    <v-list-item-subtitle
                      >录入人,修改人,录入时间,修改时间</v-list-item-subtitle
                    >
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-menu>
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
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
              :height="tableHeight"
              :expandComponent="expandComponent"
              v-on="tableListenter"
            />
          </v-card-text>
          <v-card-actions v-if="isDialog">
            <v-btn text @click="$emit('close')">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="$emit('success')">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Table from "@/components/Table/DataTable.vue";
import { skip, take } from "../../utils";

export default {
  components: {
    Table,
  },
  props: {
    toolItems: {
      type: Array,
      default: () => [],
    },
    toolSpliceCount: Number,
    direction: String,
    isDialog: Boolean,
    isTab: Boolean,
    tableHeight: String,
    hideToolitem: Boolean,
    columns: {
      type: Array,
      default: () => [],
    },
    rows: {
      type: Array,
      default: () => [],
    },
    selection: {
      type: Array,
      default: () => [],
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
      default: () => ({}),
    },
    expandComponent: [Object, Function],
  },
  computed: {
    context() {
      return {
        selection: this.selection,
        rows: this.rows,
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
        input: (val) => this.$emit("selection_update", val),
      };
    }, 
  },
  methods: {
    evalVisible({ visible }) {
      if (!visible) {
        return true;
      }
      let val = true;
      if (typeof visible == "function") val = visible.call(this, this.context);
      if (typeof visible == "boolean") val = visible;
      if (typeof visible == "string") val = !!visible;

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
    evalAction(item) {
      this.$emit(`toolItemClick`, item);
    },
  },
  watch: {},
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

.theme--dark.v-table thead th {
  background-color: #424242;
}

.theme--light.v-table thead th {
  background-color: #ffffff;
}
</style>