<script>
import BaseTreeVue from "../../../components/Trees/BaseTree.vue";
export default {
  extends: BaseTreeVue,
  methods: {
    async init() {
      this.items = await this.requestData(null);
    },
    async requestData() {
      let arr = await this.$http.get(`/api/WorkFlow/GetChildrenBySuperId`);

      return arr.map(v => ({
        ...v,
        id: v.Id,
        name: v.Name,
        type: "node"
      }));
    },
    async requestChild(parm) {
      let { id, children } = parm || {};
      let arr = await this.requestData(id);
      if (Array.isArray(children)) children.push(...arr);
    }
  }
};
</script> 