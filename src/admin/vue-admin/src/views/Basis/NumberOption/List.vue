<script>
let pageInfo = { area: "Basis", name: "NumberOption", direction: "编号设置" };
import {
  ListPageDefines,
  makeListPageInheritedFromBaseListPage,
} from "../../../components/Page";
import { getHasNumberModules } from "../../../generate.js";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    async [ListPageDefines.MethodsDefines.fmtColumns](super_func) {
      const EnumValues = await getHasNumberModules();
      return [
        ...(await super_func()),
        {
          Name: "BeModule",
          EnumValues,
        },
      ];
    },
  },
});
</script>
