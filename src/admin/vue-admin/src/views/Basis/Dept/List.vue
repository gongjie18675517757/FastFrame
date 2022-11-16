 <script>
let pageInfo = { area: "Basis", name: "Dept", direction: "部门" };
import {
  makeListPageInheritedFromBaseListPage,
  ListPageDefines,
} from "../../../components/Page";
import DeptTreeVue from "../../../components/Trees/DeptTree.vue";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      treeComponent: DeptTreeVue,
    };
  },
  methods: {
    [ListPageDefines.MethodsDefines.getColumns](arr) {
      return [
        ...arr,
        {
          Name: "Members",
          Description: "部门主管",
          getValueFunc: ({ value }) => value.map((v) => v.Name).join(","),
        },
      ];
    },
    [ListPageDefines.MethodsDefines.getPageTitle](_,v) {
      if (v == null) {
        return "全部部门";
      }

      return `${v.name}:下级部门`;
    },
  },
});
</script>
