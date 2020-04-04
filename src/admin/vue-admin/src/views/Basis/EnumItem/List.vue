<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import Page from "@/components/Page/ListPageCore.js";
export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo
    };
  }
};
</script>
