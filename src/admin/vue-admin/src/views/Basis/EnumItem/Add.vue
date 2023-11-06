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
    async [FormPageDefines.MethodsDefines.fmtModelObject](super_func) {
      const model = await super_func();
      return {
        ...model,
        KeyEnum: model.Id
          ? model.KeyEnum
          : this.key_name
          ? parseInt(this.key_name)
          : null,
      };
    },
    async [FormPageDefines.MethodsDefines.fmtModelObjectItems](super_func) {
      const arr = await super_func();
      const kvs = await this.$http.get(`/api/EnumItem/EnumValues/0`);

      return [
        ...arr,
        {
          Name: "KeyEnum",
          EnumValues: Object.entries(kvs).map(([Key, Value]) => ({
            Key,
            Value,
          })),
          EnumItemInfo: null,
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
