<script>
let pageInfo = {
  area: "Proxy",
  name: "ProxyClient",
  direction: "内网穿透服务",
};
import Page from "../../../components/Page/ListPageCore.js";
export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
    };
  },
};
</script>
