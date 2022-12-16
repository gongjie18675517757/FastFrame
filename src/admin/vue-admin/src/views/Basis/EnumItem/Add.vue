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
        KeyEnum: model.Id ? model.KeyEnum : parseInt(this.key_name),
      };
    },
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      return [
        ...arr,
        {
          Name: "KeyEnum",
          Readonly: this.key_name ? "All" : "Edit",
        },
        {
          Name: "Super_Value",
          requestUrl: ({ KeyEnum }) =>
            `/api/EnumItem/EnumItemList/${
              Number.isInteger(KeyEnum) ? KeyEnum : ""
            }`,
        },
      ];
    },
  },
});
</script>
