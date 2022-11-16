<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色",
};

import {
  FormPageDefines,
  makeFormPageInheritedFromBaseFormPage,
} from "../../../components/Page";
import RolePermissionVue from "./RolePermission.vue";

export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    [FormPageDefines.MethodsDefines.fmtModelObject](model) {
      return {
        Permissions: [],
        Members: [],
        ...model,
      };
    },
    [FormPageDefines.MethodsDefines.fmtModelObjectItems](arr) {
      return [
        ...arr,
        {
          Name: "Members",
          Description: "角色成员",
          Relate: "User",
          Type: "Array",
          requestUrl: "/api/role/userList",
          colspan: 2,
        },
        {
          Name: "Permissions",
          GroupNames: ["角色权限"],
          template: RolePermissionVue,
        },
      ];
    },
  },
});
</script> 