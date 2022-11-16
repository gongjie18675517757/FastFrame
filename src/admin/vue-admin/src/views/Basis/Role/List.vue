<script>
let pageInfo = { area: "Basis", name: "Role", direction: "角色" };
import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>

