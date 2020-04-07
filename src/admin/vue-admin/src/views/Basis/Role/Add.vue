<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色"
};

import Page from "@/components/Page/FormPageCore.js";
import { BasisDetaiTable } from "@/components/Table";

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
      return Page.methods.fmtModelObject.call(this, ...arguments).then(frm => {
        if (!frm.Id) {
          return this.$http.get(`/api/Role/PermissionList`).then(v => {
            frm.Permissions = v;
            frm.Members = frm.Members || [];
            return frm;
          });
        } else {
          return frm;
        }
      });
    },
    getModelObjectItems(opts) {
      return Page.methods.getModelObjectItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Members",
          Description: "角色成员",
          Relate: "User",
          Type: "Array",
          requestUrl: "/api/role/userList"
        });
        opts.push({
          Name: "Permissions",
          GroupNames: ["角色权限"],
          template: {
            props: ["value", "canEdit", "title"],
            render(h) {
              return h(BasisDetaiTable, {
                props: {
                  ...this.$props,
                  columns: [
                    { Name: "EnCode", Description: "标记", width: "100px" },
                    { Name: "Name", Description: "名称", width: "100px" },
                    {
                      Name: "Children",
                      Description: "子级权限",
                      width: "500px",
                      render: (h, { value }) => {
                        let indeterminate =
                          !!value.find(v => v.IsAuthorization) &&
                          !!value.find(v => v.IsAuthorization);
                        let isAllCheck = !value.find(v => !v.IsAuthorization);
                        return h("v-row", null, [
                          // this.canEdit
                          //   ? h("v-checkbox", {
                          //       props: {
                          //         indeterminate,
                          //         value: indeterminate ? null : isAllCheck,
                          //         label: "全选",
                          //         dense: true
                          //       },
                          //       on: {
                          //         change: val => {
                          //           value.forEach(
                          //             v => (v.IsAuthorization = val || false)
                          //           );
                          //           this.$emit("change", this.value);
                          //         }
                          //       }
                          //     })
                          //   : null,
                          ...value.map(v =>
                            h("v-checkbox", {
                              key: v.Id,
                              props: {
                                value: v.IsAuthorization,
                                label: v.Name,
                                dense: true,
                                readonly: !this.canEdit
                              },
                              on: {
                                change: val => {
                                  v.IsAuthorization = val || false;
                                  this.$emit("change", this.value);
                                }
                              }
                            })
                          )
                        ]);
                      }
                    }
                  ]
                }
              });
            }
          }
        });
        return opts;
      });
    }
  }
};
</script> 