<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import {
  makeFormPageInheritedFromBaseFormPage,
  FormPageDefines,
} from "../../../components/Page";

export default makeFormPageInheritedFromBaseFormPage({
  props: {
    key_name: String,
  },
  data() {
    return {
      ...pageInfo,
      pager: {
        sortBy: "Order",
        sortDesc: false,
      },
    };
  },
  methods: {
    [FormPageDefines.MethodsDefines.fmtModelObject](model) {
      return {
        ...model,
        Key: model.Id ? model.Key : this.key_name,
      };
    },
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      return [
        ...arr,
        {
          Name: "Key",
          Readonly: this.key_name ? "All" : "Edit",
        },
      ];
    },
  },
});
</script>
