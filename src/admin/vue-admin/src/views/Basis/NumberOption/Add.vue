<script>
let pageInfo = { area: "Basis", name: "NumberOption", direction: "编号设置" };
import {
  makeFormPageInheritedFromBaseFormPage,
  FormPageDefines,
} from "../../../components/Page";
import { getHasNumberModules } from "../../../generate.js";

export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    async [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      const EnumValues = await getHasNumberModules();
      return [
        ...arr,
        {
          Name: "BeModule",
          EnumValues,
        },
      ];
    },
  },
});
</script> 