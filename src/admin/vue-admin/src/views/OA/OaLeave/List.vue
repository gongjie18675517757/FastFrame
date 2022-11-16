<script>
let pageInfo = { area: "OA", name: "OaLeave", direction: "请假单" };
import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>
