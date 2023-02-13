 <script>
let pageInfo = { area: "Basis", name: "Dept", direction: "部门" };
import {
  makeListPageInheritedFromBaseListPage,
  ListPageDefines,
} from "../../../components/Page";
import { makeTree } from "../../../components/Trees";
const TreeComponent = makeTree({ type_name: "Dept" });

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      treeComponent: TreeComponent,
    };
  },
  methods: {
    async [ListPageDefines.MethodsDefines.fmtColumns](super_func) {
      const arr = await super_func();
      return [
        ...arr,
        {
          Name: "Members",
          Description: "部门主管",
          getValueFunc: ({ value }) => value.map((v) => v.Name).join(","),
        },
      ];
    },
    [ListPageDefines.MethodsDefines.getPageTitle](_, v) {
      if (v == null) {
        return "全部部门";
      }

      return `${v.name}:下级部门`;
    },
  },
});
</script>
