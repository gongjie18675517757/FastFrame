 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户"
};
import Page from "@/components/Page/FormPageCore.js";
export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo
    };
  },
  methods: {
    ...Page.methods,
    getModelObjectItems(opts) {
      return Page.methods.getModelObjectItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Depts",
          Description: "用户所在部门",
          Relate: "Dept",
          Type: "Array",
          requestUrl: `/api/user/deptList`,
          flex: { xs6: true }
        });
        opts.push({
          Name: "Roles",
          Description: "用户拥有角色",
          Relate: "Role",
          Type: "Array",
          requestUrl: `/api/user/roleList`, 
          flex: { xs6: true }
        });
        return opts;
      });
    },
    fmtModelObject(frm) {
      return Page.methods.fmtModelObject.call(this, frm).then(frm => {
        frm.Depts = frm.Depts || [];
        frm.Roles = frm.Roles || [];
        return frm;
      });
    }
  }
};
</script>  