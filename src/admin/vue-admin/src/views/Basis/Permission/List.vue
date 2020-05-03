<script>
let pageInfo = {
  area: "Basis",
  name: "Permission",
  direction: "权限"
};

import Page from "../../../components/Page/ListPageCore.js";

export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
      treeChildComponent: () => import("./List.vue"),
      toolItems: [
        ...data.toolItems,
        {
          name: "InitPermission",
          title: "初始化权限",
          icon: "error_outline",
          async action() {
            await this.$http.post(`/api/Permission/InitPermission`);
            this.$message.toast.success("初始化成功!");
            this.loadList();
          }
        }
      ]
    };
  }
};
</script>
