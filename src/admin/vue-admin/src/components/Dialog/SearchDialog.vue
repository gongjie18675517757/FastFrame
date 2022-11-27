<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center>
      <v-flex xs12 md8>
        <v-card>
          <v-toolbar flat dense color="transparent">
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-spacer></v-spacer>

            <v-btn icon @click="cancel" title="关闭">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>

          <v-card-text  style="min-width: 600px; padding: 0px">
            <VuePerfectScrollbar class="query-dialog-page">
              <SearchQueryFilterCollection
                :makeOptionsFunc="makeOptionsFunc"
                :value="query_arr"
              />
            </VuePerfectScrollbar>
          </v-card-text>

          <v-card-actions>
            <v-btn @click="reset" text> 重置 </v-btn>

            <v-spacer></v-spacer>
            <v-btn color="primary" @click="success">查询</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import SearchQueryFilterCollection from "./SearchQueryFilterCollection.vue";
export default {
  components: { SearchQueryFilterCollection, VuePerfectScrollbar },
  props: {
    title: String,
    value: Array,
    makeOptionsFunc: Function,
  },
  data() {
    return {
      conds: {
        and: "且",
        or: "或",
      },
      query_arr: this.value,
    };
  },
  mounted() {},
  methods: {
    success() {
      this.$emit("success", this.query_arr);
    },
    cancel() {
      this.$emit("close");
    },
    reset() {
      this.query_arr = this.makeOptionsFunc();
    },
  },
};
</script>

 

<style lang="stylus">
.query-dialog-page {
  height: calc(100vh - 265px);
  overflow: auto;
  padding: 12px;
  min-width: 500px;

  .v-text-field__details,.v-messages {
    display: none;
  }
}
</style>
