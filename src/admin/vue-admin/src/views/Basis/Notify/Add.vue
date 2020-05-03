<script>
let pageInfo = {
  area: "Basis",
  name: "Notify",
  direction: "通知"
};

import Page from "../../../components/Page/FormPageCore.js";
import RichInput from "../../../components/Inputs/RichInput.vue";
export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo
    };
  },
  methods: {
    ...Page.methods,
    getModelObjectItems() {
      return Page.methods.getModelObjectItems.call(this).then(opts => {
        return [
          ...opts,
          {
            Name: "Content",
            GroupNames: ["通知内容"],
            template: RichInput
          }
        ];
      });
    }
  }
};
</script>
 
