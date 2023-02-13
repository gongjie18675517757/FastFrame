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
    async [FormPageDefines.MethodsDefines.fmtModelObject](super_func) {
      const model = await super_func();
      return {
        Members: [],
        Managers: [],
        ...model,
      };
    },
    async [FormPageDefines.MethodsDefines.fmtModelObjectItems](super_func) {
      const arr =await super_func();
      return [
        ...arr,
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

 
