<script>
let pageInfo = {
  area: "Basis",
  name: "Tenant",
  direction: "组织信息"
};

import {
  formData,
  makeChildProps,
  makeChildListeners,
  FormPageMixin,
  formInject,
  formProps,
  formComputed,
  formMethods
} from "@/components/Page/FormPageCore.js";

export default {
  mixins: [FormPageMixin],
  inject: [...formInject],
  props: {
    ...formProps
  },
  data() {
    return {
      ...formData,
      ...pageInfo
    };
  },
  computed: {
    ...formComputed
  },
  methods: {
    ...formMethods
  },
  render(h) {
    let props = makeChildProps.call(this);
    let listeners = makeChildListeners.call(this);
    return h("v-page", { props, on: listeners });
  }
};
</script>  