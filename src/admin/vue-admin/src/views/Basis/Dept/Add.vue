 <script>
let pageInfo = {
  area: "Basis",
  name: "Dept",
  direction: "部门"
};
import Page from "../../../components/Page/FormPageCore.js";
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
    fmtModelObject() {
      return Page.methods.fmtModelObject
        .call(this, ...arguments)
        .then(model => {
          return {
            Members: [],
            Managers: [],
            ...model,
            Super_Id:model.Super_Id || this.super_id
          };
        });
    },
    getModelObjectItems() {
      return Page.methods.getModelObjectItems
        .call(this, ...arguments)
        .then(arr => {
          return [
            ...arr,
            {
              Name: "Super_Id",
              visible: () => !this.super_id
            },
            {
              Name: "Members",
              Description: "部门成员",
              Relate: "User",
              Type: "Array",
              requestUrl: `/api/dept/UserList`
            },
            {
              Name: "Managers",
              Description: "部门主管",
              Type: "Array",
              EnumValues: model =>
                model.Members.map(v => ({ Key: v.Id, Value: v.Name }))
            }
          ];
        });
    }
  }
};
</script>

 
