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
    <!-- <v-btn
      fab
      small
      icon
      style="position: absolute; top: 0px; right: 50px"
      color="primary"
      @click="refresh"
    >
      <v-icon> mdi-unfold-less-horizontal </v-icon>
    </v-btn>
    <v-btn
      fab
      small
      icon
      style="position: absolute; top: 0px; right: 80px"
      color="primary"
      @click="refresh"
    >
      <v-icon> mdi-unfold-more-horizontal </v-icon>
    </v-btn> -->
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
  },
};
</script>

<style>
</style>