 <script>
let pageInfo = { area: "Basis", name: "Dept", direction: "部门" };
import Page from "@/components/Page/ListPageCore.js";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
      treeChildComponent: () => import("./List.vue"),
    };
  },
  methods: {
    ...Page.methods,
    getColumns() {
      return Page.methods.getColumns.call(this, ...arguments).then((arr) => {
        return [
          ...arr,
          {
            Name: "Members",
            Description: "部门主管",
            getValueFunc: ({ value }) => value.map((v) => v.Name).join(","),
          },
        ];
      });
    },
  },
};
</script>
