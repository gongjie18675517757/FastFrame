<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import {
  ListPageDefines,
  makeListPageInheritedFromBaseListPage,
} from "../../../components/Page";
import EnumTreeVue from "./EnumTree.vue";


export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      treeComponent: EnumTreeVue,
    };
  },
  methods: {
    [ListPageDefines.MethodsDefines.getPageTitle](_,v) {
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
    [ListPageDefines.MethodsDefines.getTreeKey](title,v) {
      switch (v.type) {
        case "root":
          return "Key";
        default:
          return title;
      }
    },
    [ListPageDefines.MethodsDefines.getFormPageParsBySelectedTreeItem](_,v) {
      return {
        key_name: v.Key,
        super_id: v.Id || "",
      };
    },
  },
});
</script>
