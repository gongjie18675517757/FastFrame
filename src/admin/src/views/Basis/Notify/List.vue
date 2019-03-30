 <script>
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
      name: "Notify",
      direction: "通知"
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
