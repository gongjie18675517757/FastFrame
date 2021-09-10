<script>
let pageInfo = { area: "Basis", name: "EnumItem", direction: "数据字典" };
import Page from "@/components/Page/FormPageCore.js";
export default {
  ...Page,
  props: {
    ...Page.props,
    keyname: String,
    superid: String,
  },
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
      pager: {
        sortBy: "Order",
        sortDesc: false,
      },
    };
  },
  methods: {
    ...Page.methods,
    fmtModelObject() {
      return Page.methods.fmtModelObject
        .call(this, ...arguments)
        .then((model) => {
          return {
            ...model,
            Key: model.Key || this.keyname || null,
            Super_Id: model.Super_Id || this.superid || null,
          };
        });
    },

    getModelObjectItems() {
      return Page.methods.getModelObjectItems
        .call(this, ...arguments)
        .then((arr) => {
          return [
            ...arr,
            {
              Name: "Key",
              Readonly: () => !!this.model.Id || !!this.superid,
            },
            {
              Name: "Super_Id",
              visible: !!this.model.Id || !this.superid,
              requestUrl: (v) => `/api/EnumItem/EnumItemList/${v.Key || ""}`,
            },
          ];
        });
    },
  },
};
</script>
