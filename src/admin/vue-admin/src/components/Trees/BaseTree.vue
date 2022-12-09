<template>
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
    <v-btn
      fab
      small
      icon
      style="position: absolute; top: 0; right: 20px"
      color="primary"
      @click="refresh"
    >
      <v-icon> refresh </v-icon>
    </v-btn>
  </VuePerfectScrollbar>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";

export default {
  components: {
    VuePerfectScrollbar,
  },
  props: {
    height: String,
    requestUrl: String,
  },
  data() {
    return {
      active: [],
      items: [],
      open: [],
    };
  },
  computed: {
    styleObj() {
      return {
        height: `${this.height.replace("100vh", "100vh + 40px")}`,
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
        this.items = await this.requestData(null);
      }
    },
    async requestData(id) {
      let arr = await this.$http.get(`${this.requestUrl}?super_id=${id || ""}`);

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

<style>
</style>