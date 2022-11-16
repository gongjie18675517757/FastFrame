<script>
let pageInfo = { area: "OA", name: "OaLeave", direction: "请假单" };
import { makeFormPageInheritedFromBaseFormPage } from "../../../components/Page";
export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>
