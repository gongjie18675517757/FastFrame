<script>
let pageInfo = { area: "Flow", name: "WorkFlow", direction: "工作流" };
import Page from "../../../components/Page/ListPageCore.js";
import FlowTreeVue from "./FlowTree.vue";
export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
      treeComponent: FlowTreeVue
    };
  },
  methods: {
    ...Page.methods,

    getPageTitle(v) {
      if (v == null) {
        return "全部流程";
      }

      return `${v.name}:所属流程`;
    },
    getTreeKey(){
        return 'BeModule'
    },
    
  }
};
</script>
