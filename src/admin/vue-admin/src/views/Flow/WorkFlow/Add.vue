<script>
let pageInfo = { area: "Flow", name: "WorkFlow", direction: "工作流" };
import Page from "../../../components/Page/FormPageCore.js";
import FlowDesignVue from "../Design/FlowDesign.vue";

export default {
  ...Page,

  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
      kvs: []
    };
  },
  methods: {
    ...Page.methods,
    async init() {
      this.kvs = await this.$http.get(`/api/common/HaveCheckModuleList`);
      await Page.methods.init.call(this);
    },
    async fmtModelObject() {
      let v = await Page.methods.fmtModelObject.call(this, ...arguments);
      return {
        Nodes: v.Nodes || [
          {
            nodeType: "start"
          },
          {
            nodeType: "end"
          }
        ],
        ...v,
        BeModule: v.BeModule || this.super_id,
        Version: v.Id || (await this.getVersion(this.super_id))
      };
    },
    getVersion(val) {
      return this.$http.get(`/api/WorkFlow/GetLastVersion/${val}`);
    },
    async fmtModelObjectItems() {
      let kvs = this.kvs;
      let arr = await Page.methods.fmtModelObjectItems.call(this, ...arguments);

      return [
        ...arr,
        {
          Name: "BeModule",
          EnumValues: kvs,
          Readonly: () => !!this.super_id,
          callback: ({ model, value }) => {
            model.BeModuleName = null;
            model.Version = 1;
            if (value) {
              model.BeModuleName = value.Value;

              /**
               * 请求模块最大的版本号
               */
              this.getVersion(value.Key).then(v => {
                model.Version = v;
              });
            }
          }
        },
        {
          Name: "BeModuleName",
          visible: false
        },
        {
          Name: "Remarks",
          Length: 200
        },
        {
          Name: "Nodes",
          GroupNames:['审核过程'],
          template:FlowDesignVue
        },
      ];
    }
  }
};
</script>
