<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色"
};

import Page from "@/components/Page/FormPageCore.js";

import { FormDetailTable, SelectDetailTable } from "@/components/Table";

const PermissionTable = {
  ...FormDetailTable,
  computed: {
    ...FormDetailTable.computed,
    dynamicColumns() {
      if (this.canEdit) {
        return [
          {
            Name: "Operate",
            Description: "操作",
            width: "150px",
            component: {
              props: ["model"],
              render(h) {
                return h("span", null, [
                  h(
                    "v-btn",
                    {
                      props: {
                        text: true,
                        icon: true,
                        small: true
                      },
                      on: {
                        click: () => this.$emit("remove", this.model)
                      }
                    },
                    [h("v-icon", null, "delete")]
                  )
                ]);
              }
            }
          }
        ].concat(this.columns);
      } else {
        return this.columns;
      }
    }
  },
  methods: {
    ...FormDetailTable.methods,
    add() {
      this.$message
        .dialog(() => import("@/components/Page/TreeSelect"), {
          title: "设置权限",
          width: "500px",
          requestUrl: "/api/Permission/list",
          model: this.value.map(r => r.Id)
        })
        .then(selection => {
          this.$emit("input", selection);
          this.$emit("change", this.value);
        });
    }
  }
};

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
        frm.Permissions = frm.Permissions || [];
        return frm;
      });
    },
    getFormItems(opts) {
      return Page.methods.getFormItems.call(this, opts).then(opts => {
        opts.push({
          Name: "Members",
          GroupNames: ["角色成员"],
          template: SelectDetailTable,
          typeName: "User"
        });
        opts.push({
          Name: "Permissions",
          GroupNames: ["角色权限"],
          template: PermissionTable,
          typeName: "Permission"
        });
        return opts;
      });
    }
  }
};
</script> 