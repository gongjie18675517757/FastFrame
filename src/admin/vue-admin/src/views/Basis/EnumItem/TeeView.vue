<template>
  <v-container grid-list-xl fluid app>
    <v-layout row wrap>
      <v-flex xs12 :style="{ padding: isTab ? '0px' : null }">
        <v-card>
          <v-toolbar flat dense height="30px" color="transparent">
            <v-toolbar-title>数据字典</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-text class="pa-0">
            <v-row class="pa-4" justify="space-between">
              <v-col cols="3" style="padding: 5px">
                <v-card tile>
                  <v-toolbar flat dense height="30px" color="transparent">
                    <v-toolbar-title>字典分类</v-toolbar-title>
                    <v-spacer></v-spacer>
                    <v-toolbar-items class="hidden-sm-and-down btn-group">
                    </v-toolbar-items>
                    <v-btn title="刷新" small color="primary" @click="refresh">
                      <v-icon>mdi-refresh</v-icon>
                      刷新
                    </v-btn>
                  </v-toolbar>
                  <v-divider></v-divider>
                  <v-card-text
                    class="pa-0"
                    :class="{
                      'tab-page': isTab,
                      'dialog-page': isDialog,
                      'full-page': !isTab && !isDialog,
                    }"
                  >
                    <v-treeview
                      :active.sync="active"
                      :items="items"
                      :open.sync="open"
                      :load-children="requestChild"
                      :multiple-active="false"
                      return-object
                      activatable
                      transition
                      dense
                    >
                    </v-treeview
                  ></v-card-text>
                </v-card>
              </v-col>

              <v-divider vertical></v-divider>

              <v-col class="d-flex text-center" style="padding: 5px">
                <v-scroll-y-transition mode="out-in">
                  <ListPage v-bind="childProps" v-on="childListeners" />
                </v-scroll-y-transition>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>
 

<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import { getEnumValues } from "../../../generate";
import Page from "../../../components/Page/ListPageCore.js";
import ListPage from "../../../components/Page/ListPage.vue";

export default {
  ...Page,
  components: {
    ListPage,
  },
  data() {
    return {
      active: [],
      items: [],
      open: [],
      ...Page.data.call(this),
      ...pageInfo,
    };
  },
  computed: {
    ...Page.computed,
    treeSelected() {
      return this.active.find((v) => v) || null;
    },
    dialogMode() {
      return true;
    },
    tableHeight() {
      if (this.superId) {
        return null;
      }
      if (this.isTab) {
        return `calc(100vh - 195px - 42px)`;
      } else if (this.isDialog) {
        return `calc(100vh - ${300}px - 42px)`;
      } else {
        return `calc(100vh - 160px - 42px)`;
      }
    },
  },
  watch: {
    ...Page.watch,
    treeSelected() {
      this.pager.page = 1;
      this.loadList();
    },
  },
  mounted() {
    this.refresh();
  },
  methods: {
    ...Page.methods,
    refresh() {
      this.items = [];
      Promise.all([
        getEnumValues("EnumItem", "Key"),
        this.$http.get(`/api/enumitem/GetHasChildrenNames`),
      ]).then(([arr, brr]) => {
        this.items = arr.map((v) => ({
          ...v,
          id: v.Key,
          name: v.Value,
          type: "root",
          ...(brr.includes(v.Key) ? { children: [] } : []),
        }));

        this.$nextTick(() => {
          this.open = this.items.filter((v) =>
            this.open.some((x) => x.id == v.id)
          );
          setTimeout(() => {
            this.active = this.items.filter((v) =>
              this.active.some((x) => x.id == v.id)
            );
          }, 500);
        });
      });
    },
    async requestChild({ id, type }) {
      let arr = [];
      switch (type) {
        case "root":
          arr = await this.$http.get(`/api/EnumItem/GetChildrenByName/${id}`);
          break;
        default:
          arr = await this.$http.get(
            `/api/EnumItem/GetChildrenBySuperId/${id}`
          );
          break;
      }

      arr = arr.map((v) => ({
        ...v,
        id: v.Id,
        name: [v.Value, v.Code]
          .filter((v) => v)
          .map((v, i) => (i > 0 ? `(${v})` : v))
          .join(""),
        ...(v.HasTreeChildren ? { children: [] } : {}),
        type: "node",
      }));

      // children.push(...arr);
      return arr;
    },
    getPageTitle() {
      let v = this.treeSelected;
      if (v == null) {
        return "全部数据字典";
      }

      switch (v.type) {
        case "root":
          return `${v.name}:字典内容`;
        default:
          return `${v.name}:下级字典内容`;
      }
    },
    getRequestPars() {
      return Page.methods.getRequestPars
        .call(this, ...arguments)
        .then((page) => {
          let v = this.treeSelected;
          if (v != null) {
            switch (v.type) {
              case "root":
                page.Filters.push({
                  key: "and",
                  value: [
                    {
                      Name: "Key",
                      compare: "==",
                      value: v.id,
                    },
                  ],
                });
                break;
              default:
                page.Filters.push({
                  key: "and",
                  value: [
                    {
                      Name: "Super_Id",
                      compare: "==",
                      value: v.id,
                    },
                  ],
                });
                break;
            }
          }
          // console.log(page);
          return page;
        });
    },
    getFormPagePars() {
      return Page.methods.getFormPagePars
        .call(this, ...arguments)
        .then((obj) => {
          return {
            ...obj,
            ...(this.treeSelected
              ? {
                  keyname: this.treeSelected.Key,
                  superid: this.treeSelected.Id || "",
                }
              : {}),
          };
        });
    },
  },
};
</script>

<style>
</style>