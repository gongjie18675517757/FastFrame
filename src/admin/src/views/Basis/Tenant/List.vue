<script>
import {
  ListPageMixin,
  data,
  makeChildProps,
  makeChildListeners
} from "@/components/Page/ListPageCore.js";

export default {
  mixins: [ListPageMixin],
  data() {
    return {
      ...data,
      area: "Basis",
      name: "Tenant",
      direction: "组织信息"
    };
  },
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);
    return h("v-list-page", {
      props,
      on: listeners
    });
  }
};
</script>


