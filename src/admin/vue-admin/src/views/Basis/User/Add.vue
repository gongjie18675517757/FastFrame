 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户",
};
import Page from "@/components/Page/FormPageCore.js";
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
    getModelObjectItems() {
      return Page.methods.getModelObjectItems
        .call(this, ...arguments)
        .then((opts) => {
          return [
            ...opts,
             
            {
              Name: "Depts",
              Description: "用户所在部门",
              Relate: "Dept",
              Type: "Array",
              requestUrl: `/api/user/deptList`,
              flex: { xs6: true },
               
            },
            {
              Name: "Roles",
              Description: "用户拥有角色",
              Relate: "Role",
              Type: "Array",
              requestUrl: `/api/user/roleList`,
              flex: { xs6: true },
            },
          ];
        });
    },
    fmtModelObject(frm) {
      return Page.methods.fmtModelObject.call(this, frm).then((frm) => {
        frm.Depts = frm.Depts || [];
        frm.Roles = frm.Roles || [];
        return frm;
      });
    },
  },
};
</script>  