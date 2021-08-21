<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import Page from "../../../components/Page/ListPageCore.js";
import EnumTreeVue from "./EnumTree.vue";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
      treeComponent: EnumTreeVue,
    };
  },
  methods: {
    ...Page.methods,
    getPageTitle(v) {
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
    getRequestParsByTreeItem(v) {
      switch (v.type) {
        case "root":
          return [
            {
              Name: "Key",
              compare: "==",
              value: v.id,
            },
          ];
        default:
          return [
            {
              Name: "Super_Id",
              compare: "==",
              value: v.id,
            },
          ];
      }
    },
    getFormPageParsBySelectedTreeItem(v) {
      return {
        keyname: v.Key,
        superid: v.Id || "",
      };
    },
  },
};
</script>
