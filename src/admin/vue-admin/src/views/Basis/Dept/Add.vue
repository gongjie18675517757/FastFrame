 <script>
let pageInfo = {
  area: "Basis",
  name: "Dept",
  direction: "部门"
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
    fmtModelObject(frm) {
      return Page.methods.fmtModelObject.call(this, frm).then(frm => {
        frm.Members = frm.Members || [];
        frm.Managers = frm.Managers || [];
        return frm;
      });
    },
    getModelObjectItems(opts) {
      return Page.methods.getModelObjectItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Members",
          Description: "部门成员",
          Relate: "User",
          Type: "Array",
          requestUrl: `/api/dept/UserList`
        });
        opts.push({
          Name: "Managers",
          Description: "部门主管",
          Type: "Array",
          EnumValues: model =>
            model.Members.map(v => ({ Key: v.Id, Value: v.Name }))
        });
        return opts;
      });
    }
  }
};
</script>

 
