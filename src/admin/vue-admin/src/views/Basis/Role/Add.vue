<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色"
};

import Page from "@/components/Page/FormPageCore.js";
import { BasisDetaiTable } from "../../../components/Table";
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
            ...model,
            Permissions: model.Permissions || [],
            Members: model.Members || []
          };
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
            props: ["value", "canEdit", "title", "model"],
            render(h) {
              return h(BasisDetaiTable, {
                props: {
                  title: this.title,
                  value: this.$store.state.permissionList,
                  rowKey: "PermissionKey",
                  columns: [
                    {
                      Name: "PermissionKey",
                      Description: "标记",
                      width: "100px"
                    },
                    {
                      Name: "PermissionText",
                      Description: "名称",
                      width: "100px"
                    },
                    {
                      Name: "Child",
                      Description: "子级权限",
                      width: "500px",
                      render: (h, { value, model }) => {
                        return this.canEdit
                          ? h(
                              "v-btn-toggle",
                              {
                                props: {
                                  multiple: true,
                                  value: this.value
                                    .filter(
                                      v =>
                                        v.SuperPermissionKey ==
                                        model.PermissionKey
                                    )
                                    .map(v => v.PermissionKey)
                                },
                                on: {
                                  change: arr => {
                                    if (!this.canEdit) {
                                      return;
                                    }

                                    //移除点掉的权限
                                    for (const v of this.value) {
                                      if (
                                        !arr.includes(v.PermissionKey) &&
                                        v.SuperPermissionKey ==
                                          model.PermissionKey
                                      ) {
                                        let index = this.value.findIndex(
                                          r => r == v
                                        );
                                        this.value.splice(index, 1);
                                      }
                                    }

                                    //添加勾上的权限
                                    for (const key of arr) {
                                      let index = this.value.findIndex(
                                        v =>
                                          v.SuperPermissionKey ==
                                            model.PermissionKey &&
                                          v.PermissionKey == key
                                      );

                                      if (index == -1) {
                                        this.value.push({
                                          PermissionKey: key,
                                          SuperPermissionKey:
                                            model.PermissionKey
                                        });
                                      }
                                    }

                                    this.$emit("change", this.value);
                                  }
                                }
                              },
                              [
                                ...value.map(v =>
                                  h(
                                    "v-btn",
                                    {
                                      key: v.PermissionKey,
                                      props: {
                                        value: v.PermissionKey,
                                        small: true,
                                        text: true
                                      }
                                    },
                                    v.PermissionText
                                  )
                                )
                              ]
                            )
                          : h("span", null, [
                              ...value.map(r =>
                                h(
                                  "span",
                                  {
                                    style: {
                                      "padding-left": "15px"
                                    }
                                  },
                                  `${r.PermissionText} ${
                                    this.value.find(
                                      v =>
                                        v.SuperPermissionKey ==
                                          model.PermissionKey &&
                                        v.PermissionKey == r.PermissionKey
                                    )
                                      ? "√"
                                      : "×"
                                  }`
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