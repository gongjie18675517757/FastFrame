<script>
let pageInfo = {
  area: "Basis",
  name: "Role",
  direction: "角色"
};

import {
  formData,
  makeChildProps,
  makeChildListeners,
  FormPageMixin,
  formInject,
  formProps,
  formComputed,
  formMethods
} from "@/components/Page/FormPageCore.js";

import { alert } from "@/utils";
import { FormDetailTable,SelectDetailTable  } from "@/components/Table";

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
                        flat: true,
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
  mixins: [FormPageMixin],
  inject: [...formInject],
  props: {
    ...formProps
  },
  data() {
    return {
      ...formData,
      ...pageInfo
    };
  },
  computed: {
    ...formComputed
  },
  methods: {
    ...formMethods,
    frmLoadForm(frm) {
      return formMethods.frmLoadForm.call(this, frm).then(frm => {
        frm.Members = frm.Members || [];
        frm.Permissions = frm.Permissions || [];
        return frm;
      });
    },
    getFormItems(opts) {
      return formMethods.getFormItems.call(this, opts).then(opts => {
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
  },
  render(h) {
    let props = makeChildProps.call(this);
    let listeners = makeChildListeners.call(this);
    return h("v-page", { props, on: listeners });
  }
};
</script> 