<script>
let pageInfo = { area: "Flow", name: "WorkFlow", direction: "工作流" };
import Page from "../../../components/Page/FormPageCore.js";
export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo
    };
  },
  methods: {
    ...Page.methods,
    fmtModelObject() {
      return Page.methods.fmtModelObject.call(this, ...arguments).then(v => {
        return {
          ...v,
          Nodes: v.Nodes || [],
          Lines: v.Lines || []
        };
      });
    },
    fmtModelObjectItems() {
      return Page.methods.fmtModelObjectItems
        .call(this, ...arguments)
        .then(arr => {
          return this.$http.get(`/api/common/HaveCheckModuleList`).then(kvs => {
            return [
              ...arr,
              {
                Name: "BeModule",
                EnumValues: kvs,
                callback: ({ model, value }) => {
                  model.BeModuleName = null;
                  model.Version = 1;
                  if (value) {
                    model.BeModuleName = value.Value;

                    /**
                     * 请求模块最大的版本号
                     */
                    this.$http
                      .get(`/api/WorkFlow/GetLastVersion/${value.Key}`)
                      .then(v => {
                        model.Version = v;
                      });
                  }
                }
              },
              {
                Name: "BeModuleName",
                Hide: "All"
              }
            ];
          });
        });
    }
  }
};
</script>
