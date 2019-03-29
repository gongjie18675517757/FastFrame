 
<script>
import { alert } from "@/utils";
import {
  ListPageMixin,
  data,
  pageProps,
  pageListeners
} from "@/components/Page/ListPageCore.js";

export default {
  mixins: [ListPageMixin],
  data() {
    return {
      ...data,
      area: "Basis",
      name: "Permission",
      direction: "权限",
      childToolItems: [
        {
          name: "InitPermission",
          title: "初始化权限",
          icon: "error_outline",
          async action() {
            await this.$http.post(`/api/Permission/InitPermission`);
            alert.success("初始化成功!");
            this.reload();
          }
        }
      ]
    };
  },
  render(h) {
    let props = pageProps.call(this),
      listeners = pageListeners.call(this);
    return h("v-list-page", {
      props,
      on: listeners
    });
  }
};
</script>
