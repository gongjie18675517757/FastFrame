<script>
import { getEnumValues } from "../../../generate";
import BaseTreeVue from "../../../components/Trees/BaseTree.vue";
export default {
  extends: BaseTreeVue,
  methods: {
    init() {
      Promise.all([
        getEnumValues("EnumItem", "Key"),
        this.$http.get(`/api/enumitem/GetHasChildrenNames`)
      ]).then(([arr, brr]) => {
        this.items = arr.map(v => ({
          ...v,
          id: v.Key,
          name: v.Value,
          type: "root",
          ...(brr.includes(v.Key) ? { children: [] } : {})
        }));
      });
    },
    async requestChild({ id, type, children }) {
      let arr = [];
      switch (type) {
        case "root":
          arr = await this.$http.get(`/api/EnumItem/GetChildrenByName/${id}`);
          break;
        default:
          arr = await this.$http.get(
            `/api/EnumItem/GetChildrenBySuperId/${id}`
          );
          break;
      }

      arr = arr.map(v => ({
        ...v,
        id: v.Id,
        name: [v.Value, v.Code]
          .filter(v => v)
          .map((v, i) => (i > 0 ? `(${v})` : v))
          .join(""),
        ...(v.ChildCount > 0 ? { children: [] } : {}),
        type: "node"
      }));

      children.push(...arr);
    }
  }
};
</script> 