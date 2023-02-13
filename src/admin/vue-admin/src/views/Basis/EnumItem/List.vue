<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import {
  ListPageDefines,
  makeListPageInheritedFromBaseListPage,
} from "../../../components/Page";
import { makeTree } from "../../../components/Trees";
const TreeComponent = makeTree({ type_name: "EnumItem" });

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      treeComponent: TreeComponent,
    };
  },
  methods: {
    [ListPageDefines.MethodsDefines.getPageTitle](_, v) {
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

    [ListPageDefines.MethodsDefines.getFormPageParsBySelectedTreeItem](
      super_func,
      tree
    ) {
      const v = super_func();
      return {
        ...v,
        key_name: tree.Key,
      };
    },
  },
});
</script>
