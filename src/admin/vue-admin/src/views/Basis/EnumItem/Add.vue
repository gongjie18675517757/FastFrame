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
    fmtModelObject(frm) {
      return Page.methods.fmtModelObject.call(this, frm).then(frm => {
        return frm;
      });
    },
    getModelObjectItems() {
      return Page.methods.getModelObjectItems.call(this).then(opts => {         
        return opts;
      });
    }
  }
};
</script>
