 
 <script>
import {
  formData,
  pageProps,
  pageListeners,
  FormPageMixin
} from "@/components/Page/FormPageCore.js";

export default {
  mixins: [FormPageMixin],
  data() {
    return {
      ...formData,
      area: "Basis",
      name: "Notify",
      direction: "通知"
    };
  },
  render(h) {
    let props = pageProps.call(this);
    let listeners = pageListeners.call(this);
    return h("v-page", { props, on: listeners });
  }
};
</script>

 


<style>
</style>
