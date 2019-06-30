 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户"
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

import { SelectDetailTable } from "@/components/Table";

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
    getFormItems(opts) {
      return formMethods.getFormItems.call(this,opts).then(opts => {
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
      return formMethods.frmLoadForm.call(this,frm).then(frm => {
        frm.Depts = frm.Depts || [];
        frm.Roles = frm.Roles || [];
        return frm;
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