<script>
let pageInfo = { area: "Basis", name: "Role", direction: "角色" };
import {
  makeListPageInheritedFromBaseListPage,
  ListPageDefines,
} from "../../../components/Page";

import { makeTree } from "../../../components/Trees";
const TreeComponent = makeTree({ type_name: "Role" });

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      [ListPageDefines.DataDefines.treeComponent]: TreeComponent,
    };
  },
});
</script>

