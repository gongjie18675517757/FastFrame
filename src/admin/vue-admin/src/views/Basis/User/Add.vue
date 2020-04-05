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
    getModelObjectItems(opts) {
      return Page.methods.getModelObjectItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Depts",
          GroupNames: ["用户所在部门"],
          template: SelectDetailTable,
          dialogComponent: () => import("../User/List.vue"),
          typeName: "Dept"
        });
        opts.push({
          Name: "Depts",
          GroupNames: ["用户所在部门"],
          template: SelectDetailTable,
          dialogComponent: () => import("../User/List.vue"),
          typeName: "Dept"
        });
        opts.push({
          Name: "Permissions", 
          Description:'权限',
          Relate:'Permission',
          Type:'Array'
        });
        return opts;
      });
    },
    fmtModelObject(frm) {
      return Page.methods.fmtModelObject.call(this, frm).then(frm => {
        console.log(frm);
        
        frm.Depts = frm.Depts || [];
        frm.Roles = frm.Roles || [];
        frm.Permissions = frm.Permissions || [];
        return frm;
      });
    }
  }
};
</script>  