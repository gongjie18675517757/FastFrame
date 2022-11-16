<script>
import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";
export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      area: "{{AreaName}}",
      name: "{{ModuleName}}",
      direction: "{{Description}}",
    };
  },
});
</script>
