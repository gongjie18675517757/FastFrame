<script>
let pageInfo = {
  area: "Basis",
  name: "Notify",
  direction: "通知",
};

import {
  makeFormPageInheritedFromBaseFormPage,
  FormPageDefines,
} from "../../../components/Page";
import RichInput from "../../../components/Inputs/RichInput.vue";
export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      return [
        ...arr,
        {
          Name: "Content",
          GroupNames: ["通知内容"],
          template: RichInput,
        },
      ];
    },
  },
});
</script>
 
