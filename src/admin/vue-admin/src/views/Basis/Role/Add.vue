<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色",
};

import Page from "@/components/Page/FormPageCore.js";
import RolePermissionVue from "./RolePermission.vue";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
    };
  },
  methods: {
    ...Page.methods,
    fmtModelObject() {
      return Page.methods.fmtModelObject
        .call(this, ...arguments)
        .then((model) => {
          return {
            ...model,
            Permissions: model.Permissions || [],
            Members: model.Members || [],
          };
        });
    },
    getModelObjectItems(opts) {
      return Page.methods.getModelObjectItems.call(this, opts).then((opts) => {
        opts.push({
          Name: "Members",
          Description: "角色成员",
          Relate: "User",
          Type: "Array",
          requestUrl: "/api/role/userList",
          colspan:2
        });
        opts.push({
          Name: "Permissions",
          GroupNames: ["角色权限"],
          template: RolePermissionVue,
        });
        return opts;
      });
    },
  },
};
</script> 