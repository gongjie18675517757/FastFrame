 <script>
let pageInfo = {
  area: "Basis",
  name: "Dept",
  direction: "部门",
};
import {
  makeFormPageInheritedFromBaseFormPage,
  FormPageDefines,
} from "../../../components/Page";
export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    [FormPageDefines.MethodsDefines.fmtModelObject](model) {
      return {
        Members: [],
        Managers: [],
        ...model,
        Super_Id: model.Super_Id || this.super_id,
      };
    },
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      return [
        ...arr,
        {
          Name: "Super_Value",
          visible: () => !this.super_id, 
        },
        {
          Name: "Members",
          Description: "部门成员",
          Relate: "User",
          Type: "Array",
          requestUrl: `/api/dept/UserList`,
        },
        {
          Name: "Managers",
          Description: "部门主管",
          Type: "Array",
          EnumValues: (model) =>
            model.Members.map((v) => ({ Key: v.Id, Value: v.Value })),
        },
      ];
    },
  },
});
</script>

 
