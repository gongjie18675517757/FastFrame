<template>
  <div style="background: #fff;">
    <div style="padding: 0px 12px;">
      <v-text-field
        append-icon="search"
        placeholder="搜索"
        hide-details
        dense
        append-outer-icon="refresh"
        @click:append-outer="refresh"
        class="tree-search"
        single-line
        v-model="kw"
      ></v-text-field>
    </div>

    <VuePerfectScrollbar :style="styleObj">
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
      </v-treeview>
    </VuePerfectScrollbar>
  </div>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import { debounce } from "../../utils";

export default {
  components: {
    VuePerfectScrollbar,
  },
  props: {
    height: String,
    requestUrl: String,
    init_super_key:String
  },
  data() {
    return {
      active: [],
      items: [],
      open: [],
      kw: null,
    };
  },
  computed: {
    styleObj() {
      return {
        height: `${this.height.replace("100vh", "100vh - 10px")}`,
        overflow: "auto",
      };
    },
    treeSelected() {
      return this.active ? this.active.find((v) => v) || null : null;
    },
  },
  watch: {
    treeSelected(val) {
      this.$emit("input", val);
    },
    kw() {
      this.refresh();
    },
  },
  created() {
    this.init = debounce(this.init, 500).bind(this);
  },
  mounted() {
    this.refresh();
  },
  methods: {
    refresh() {
      this.items = [];
      this.open = [];
      this.active = [];
      this.init();
    },
    async init() {
      if (this.requestUrl) {
        this.items = await this.requestData(this.init_super_key || '');
      }
    },
    async requestData(id) {
      let arr = await this.$http.get(
        `${this.requestUrl}?super_id=${id || ""}&kw=${this.kw || ""}`
      );

      return arr.map((v) => ({
        ...v,
        id: v.Id,
        name: v.Value,
        ...(v.ChildCount > 0 ? { children: [] } : {}),
        type: "node",
      }));
    },
    async requestChild(parm) {
      let { id, children } = parm || {};
      let arr = await this.requestData(id);
      if (Array.isArray(children)) children.push(...arr);
    },
  },
};
</script>

<style lang="stylus">
.tree-search {
  .v-input__append-outer, .v-input__prepend-outer {
    margin-top: 0px;
  }
}
</style>