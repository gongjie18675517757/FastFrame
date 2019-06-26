 
 <script>
import {
  formData,
  makeChildProps,
  makeChildListeners,
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
    let props = makeChildProps.call(this);
    let listeners = makeChildListeners.call(this);
    return h("v-page", { props, on: listeners });
  }
};
</script>

 


<style>
</style>
