<script>
let pageInfo = {
  area: "Basis",
  name: "ApiRequestLog",
  direction: "接口访问记录",
};

import { makeListPageInheritedFromBaseListPage } from "../../../components/Page";

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
    };
  },
});
</script> 