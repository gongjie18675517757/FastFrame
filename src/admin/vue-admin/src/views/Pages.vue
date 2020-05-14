<template  >
  <v-flex>
    <v-tabs height="35px" :value="curr">
      <v-tab
        v-for="page in $store.state.pages"
        :key="page.fullPath"
        @click="handleClick(page)"
        @contextmenu="show(page)"
      >
        {{page.title }}
        <v-icon
          style="padding-left:5px;"
          size="small"
          v-if="page.closeable"
          @click="closePage(page)"
        >close</v-icon>
      </v-tab>
    </v-tabs>
    <v-divider></v-divider>
    <component
      xs12
      v-for="page in $store.state.pages"
      :key="`${page.key}`"
      v-show="page.fullPath==$store.state.currPageFullPath"
      :is="page.component"
      v-bind="page.pars"
      isTab
      style="height:calc(100vh - 125px);overflow: hide;padding: 12px;"
      @success="closePage(page)"
      @close="closePage(page)"
    />
    <v-menu v-model="showMenu" :position-x="x" :position-y="y" absolute offset-y>
      <v-list dense>
        <v-list-item
          v-for="(item, index) in items.filter(v=>v.visible)"
          :key="index"
          @click="item.action"
        >
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      contextMenuPage: null,
      showMenu: false,
      x: 0,
      y: 0
    };
  },
  computed: {
    items() {
      if (this.contextMenuPage) {
        let currPath = this.$store.state.currPageFullPath;
        let page = this.contextMenuPage;
        let currIndex = this.$store.state.pages.findIndex(
          v => v == this.contextMenuPage
        );
        let total = this.$store.state.pages.length;

        return [
          {
            title: "关闭当前",
            visible: this.contextMenuPage.closeable,
            action: () => {
              this.closePage(this.contextMenuPage);
            }
          },
          {
            title: "关闭其它",
            visible: total > 1,
            action: () => {
              this.$router.push(page.fullPath);
              this.$store.state.pages = this.$store.state.pages.filter(
                v => v == page || !v.closeable
              );
            }
          },
          {
            title: "关闭左侧",
            visible: total > 1 && currIndex > 0,
            action: () => {
              var arr = this.$store.state.pages.filter(
                (v, i) => i >= currIndex || !v.closeable
              );
              if (!arr.find(v => v.fullPath == currPath)) {
                this.$router.push(page.fullPath);
              }
              this.$store.state.pages = arr;
            }
          },
          {
            title: "关闭右侧",
            visible: total > 1 && currIndex < total - 1,
            action: () => {
              var arr = this.$store.state.pages.filter(
                (v, i) => i <= currIndex || !v.closeable
              );
              if (!arr.find(v => v.fullPath == currPath)) {
                this.$router.push(page.fullPath);
              }
              this.$store.state.pages = arr;
            }
          }
        ];
      } else {
        return [];
      }
    },
    curr() {
      return this.$store.state.pages.findIndex(
        v => v.fullPath == this.$store.state.currPageFullPath
      );
    }
  },
  methods: {
    handleClick(page) {
      if (this.$store.state.currPageFullPath == page.fullPath) return;
      this.$router.push(page.fullPath);
    },
    closePage(page) {
      this.$store.dispatch("closePage", page.fullPath);
    },
    show(page) {
      this.contextMenuPage = page;
      let e = event;
      e.preventDefault();
      this.showMenu = false;
      this.x = e.clientX;
      this.y = e.clientY;
      this.$nextTick(() => {
        this.showMenu = true;
      });
    }
  }
};
</script>

<style>
</style>
