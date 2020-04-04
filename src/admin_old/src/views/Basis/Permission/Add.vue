<script>
let pageInfo = {
  area: "Basis",
  name: "Permission",
  direction: "权限"
};

import Page from "@/components/Page/FormPageCore.js";
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
 

 
