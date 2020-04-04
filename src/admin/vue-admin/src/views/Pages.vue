<template  >
  <v-flex>
    <v-flex class="tabs">
      <v-chip
        :close="page.closeable"
        color="primary"
        label
        :outlined="page.fullPath!=$store.state.currPageFullPath"
        :text-color="page.fullPath==$store.state.currPageFullPath?'#fff':''"
        v-for="page in $store.state.pages"
        :key="page.fullPath"
        @click="$router.push(page.fullPath)"
        :value="true"
        @input="closePage(page)"
      >{{page.title}}</v-chip>
    </v-flex>
    <v-divider></v-divider>
    <component
      xs12
      v-for="page in $store.state.pages"
      :key="`${page.fullPath}`"
      v-show="page.fullPath==$store.state.currPageFullPath"
      :is="page.component"
      v-bind="page.pars"
      isTab
      style="height:calc(100vh - 125px);overflow: auto;padding: 12px;"
      @success="closePage(page)"
      @close="closePage(page)"
    />
  </v-flex>
</template>

<script>
export default {
  methods: {
    closePage(page) {
      this.$store.dispatch("closePage", page.fullPath);
    }
  }
};
</script>

<style>
</style>
