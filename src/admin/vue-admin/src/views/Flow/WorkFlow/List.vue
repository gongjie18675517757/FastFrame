<script>
let pageInfo = { area: "Flow", name: "WorkFlow", direction: "工作流" };
import {
  ListPageDefines,
  makeListPageInheritedFromBaseListPage,
} from "../../../components/Page";
import FlowTreeVue from "./FlowTree.vue";
export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      [ListPageDefines.DataDefines.treeComponent]: FlowTreeVue,
    };
  },
  methods: {
    async [ListPageDefines.MethodsDefines.fmtColumns](super_func) {
      let kvs = await this.$http.get(`/api/common/HaveCheckModuleList`);
      return [
        ...(await super_func()),
        {
          Name: "BeModule",
          EnumValues: kvs,
        },
        {
          Name: "Enabled",
          renderFunc: (h, { model }) =>
            h(
              "v-chip",
              {
                props: {
                  "hide-details": true,
                  color: model.Enabled == "enabled" ? "p" : null,
                  "text-color": model.Enabled == "enabled" ? "white" : null,
                  label: true,
                  small: true,
                },
                attrs: {
                  title: "点击切换",
                },
                on: {
                  click: () => {
                    this.toggleEnable(model);
                  },
                },
              },
              model.Enabled == "enabled" ? "启用" : "停用"
            ),
        },
      ];
    },
    [ListPageDefines.MethodsDefines.getPageTitle](_, v) {
      if (v == null) {
        return "全部流程";
      }

      return `${v.name}:所属流程`;
    },
    [ListPageDefines.MethodsDefines.getTreeKey]() {
      return "BeModule";
    },
    async toggleEnable(r) {
      await this.$message.confirm({
        title: "提示",
        content: "确认要切换启用/停用吗?",
      });
      await this.$http.post(`/api/WorkFlow/toggleEnable/${r.Id}`);
      if (r.Enabled == "enabled") r.Enabled = "disabled";
      else if (r.Enabled == "disabled") r.Enabled = "enabled";
      this.$message.toast.success("切换成功！");
    },
  },
});
</script>
