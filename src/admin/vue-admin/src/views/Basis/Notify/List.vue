 <script>
let pageInfo = { area: "Basis", name: "Notify", direction: "通知" };
import {
  ListPageDefines,
  makeListPageInheritedFromBaseListPage,
} from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    [ListPageDefines.MethodsDefines.fmtColumns](arr) {
      return [...arr.filter((v) => !["Content"].includes(v.Name))];
    },
  },
});
</script> 