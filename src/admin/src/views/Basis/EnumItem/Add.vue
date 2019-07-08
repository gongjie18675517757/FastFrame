<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
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
    frmLoadForm(frm) {
      return Page.methods.frmLoadForm.call(this, frm).then(frm => {
        return frm;
      });
    },
    getFormItems() {
      return Page.methods.getFormItems.call(this).then(opts => {         
        return opts;
      });
    }
  }
};
</script>
