<script>
let pageInfo = { area: "Flow", name: "DFModule", direction: "动态表单模块" };
import { makeFormPageInheritedFromBaseFormPage } from "../../../components/Page";
export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script>
