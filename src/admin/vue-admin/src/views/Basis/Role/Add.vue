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
                        return this.canEdit
                          ? h(
                              "v-btn-toggle",
                              {
                                props: {
                                  multiple: true,
                                  value: value
                                    .filter(v => v.IsAuthorization && v.Id)
                                    .map(v => v.Id)
                                },
                                on: {
                                  change: arr => {
                                    if (!this.canEdit) {
                                      return;
                                    }
                                    for (const v of value) {
                                      if (arr.includes(v.Id)) {
                                        v.IsAuthorization = true;
                                      }
                                      this.$emit("change", this.value);
                                    }
                                  }
                                }
                              },
                              [
                                ...value.map(v =>
                                  h(
                                    "v-btn",
                                    {
                                      key: v.Id,
                                      props: {
                                        value: v.Id,
                                        small: true,
                                        text: true
                                      }
                                    },
                                    v.Name
                                  )
                                )
                              ]
                            )
                          : h("span", null, [
                              ...value.map(v =>
                                h(
                                  "span",
                                  {
                                    style: {
                                      "padding-left": "15px"
                                    }
                                  },
                                  `${v.Name} ${v.IsAuthorization ? "√" : "×"}`
                                )
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