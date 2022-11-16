<script>
let pageInfo = { area: "Flow", name: "DFModule", direction: "动态表单模块" };
import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>
