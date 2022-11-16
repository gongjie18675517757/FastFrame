<script>
import { makeFormPageInheritedFromBaseFormPage } from "../../../components/Page";

export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      area: "{{AreaName}}",
      name: "{{ModuleName}}",
      direction: "{{Description}}",
    };
  },
});
</script>