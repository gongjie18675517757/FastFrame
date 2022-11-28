<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import {
  makeFormPageInheritedFromBaseFormPage,
  FormPageDefines,
} from "../../../components/Page";

export default makeFormPageInheritedFromBaseFormPage({
  props: {
    key_name: String,
    super_id: String,
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
        Key: model.Key || this.key_name || null,
        Super_Id: model.Super_Id || this.super_id || null,
      };
    },
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
  
      return [
        ...arr,
        {
          Name: "Key",
          Readonly: () => !!this.model.Id || !!this.super_id || !!this.key_name,
        },
        {
          Name: "Super_Value",
          visible: !!this.model.Id || !this.super_id,
          requestUrl: (v) => { 
            return `/api/EnumItem/EnumItemList/${v.Key || ""}`
          }
        },
      ];
    },
  },
});
</script>
