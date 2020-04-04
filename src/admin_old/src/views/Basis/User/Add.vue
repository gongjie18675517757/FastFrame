 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户"
};
import { SelectDetailTable } from "@/components/Table";
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
    getFormItems(opts) {
      return Page.methods.getFormItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Depts",
          GroupNames: ["用户所在部门"],
          template: SelectDetailTable,
          typeName: "Dept"
        });
        opts.push({
          Name: "Roles",
          GroupNames: ["用户归属角色"],
          template: SelectDetailTable,
          typeName: "Role"
        });
        return opts;
      });
    },
    frmLoadForm(frm) {
      return Page.methods.frmLoadForm.call(this, frm).then(frm => {
        frm.Depts = frm.Depts || [];
        frm.Roles = frm.Roles || [];
        return frm;
      });
    }
  }
};
</script>  