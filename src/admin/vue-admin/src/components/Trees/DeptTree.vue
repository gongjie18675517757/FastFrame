<script>
import BaseTreeVue from "./BaseTree.vue";

/**
 * 弃用了,使用工厂函数生成
 */
export default {
  extends: BaseTreeVue,
  methods: {
    async init() {
      this.items = await this.requestData(null);
    },
    async requestData(id) {
      let arr = await this.$http.get(`/api/Dept/TreeList/${id || ""}`);

      return arr.map(v => ({
        ...v,
        id: v.Id,
        name:v.Value,
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