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
    async [ListPageDefines.MethodsDefines.fmtColumns](super_func) {
      return [
        ...(await super_func()).filter((v) => !["Content"].includes(v.Name)),
      ];
    },
  },
});
</script> 