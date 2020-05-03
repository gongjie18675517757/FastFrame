 <script>
let pageInfo = { area: "Basis", name: "Notify", direction: "通知" };
import Page from "../../../components/Page/ListPageCore.js";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo
    };
  },
  methods:{
    ...Page.methods,
    getColumns(){
      return Page.methods.getColumns.call(this,...arguments).then(arr=>{
        return [
          ...arr.filter(v=>!['Content'].includes(v.Name))
        ]
      })
    }
  }
};
</script> 