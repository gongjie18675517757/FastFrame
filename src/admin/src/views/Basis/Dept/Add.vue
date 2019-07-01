 <script>
let pageInfo = {
  area: "Basis",
  name: "Dept",
  direction: "部门"
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
    frmLoadForm(frm) {
      return formMethods.frmLoadForm.call(this, frm).then(frm => {
        frm.Members = frm.Members || [];
        frm.Managers = frm.Managers || [];
        return frm;
      });
    },
    getFormItems(opts) {
      return formMethods.getFormItems.call(this, opts).then(opts => {
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
  },
  render(h) {
    let props = makeChildProps.call(this);
    let listeners = makeChildListeners.call(this);
    return h("v-page", { props, on: listeners });
  }
};
</script>

 
