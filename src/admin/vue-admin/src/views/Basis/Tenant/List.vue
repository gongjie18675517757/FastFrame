<script>
let pageInfo = { area: "Basis", name: "Tenant", direction: "组织信息" };
import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>


