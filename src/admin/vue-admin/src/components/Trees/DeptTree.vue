<script>
import BaseTreeVue from "./BaseTree.vue";
export default {
  extends: BaseTreeVue,
  methods: {
    async init() {
      this.items = await this.requestData(null);
    },
    async requestData(id) {
      let arr = await this.$http.get(`/api/Dept/GetChildrenBySuperId/${id || ""}`);

      return arr.map(v => ({
        ...v,
        id: v.Id,
        name: [v.Name, v.EnCode]
          .filter(v => v)
          .map((v, i) => (i > 0 ? `(${v})` : v))
          .join(""),
        ...(v.ChildCount > 0 ? { children: [] } : {}),
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