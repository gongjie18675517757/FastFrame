 <script>
let pageInfo = {
  area: "Basis",
  name: "Dept",
  direction: "部门"
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
    frmLoadForm(frm) {
      return Page.methods.frmLoadForm.call(this, frm).then(frm => {
        frm.Members = frm.Members || [];
        frm.Managers = frm.Managers || [];
        return frm;
      });
    },
    getFormItems(opts) {
      return Page.methods.getFormItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Members",
          GroupNames: ["部门所有成员"],
          template: SelectDetailTable,
          typeName: "User"
        });
        opts.push({
          Name: "Managers",
          GroupNames: ["部门管理人员"],
          template: SelectDetailTable,
          typeName: "User"
        });
        return opts;
      });
    }
  }
};
</script>

 
