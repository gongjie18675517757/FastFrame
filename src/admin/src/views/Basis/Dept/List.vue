 <script> 
let pageInfo = { area: "Basis", name: "Dept", direction: "部门" };
import {
  ListPageMixin,
  pageInjects,
  pageProps,
  makePageData,
  pageComputed,
  pageMethods,
  makeChildProps,
  makeChildListeners
} from "@/components/Page/ListPageCore.js";
export default {
  mixins: [ListPageMixin],
  inject: pageInjects,
  props: pageProps,
  data() {
    let data = makePageData.call(this);
    return {
      ...data,
      ...pageInfo
    };
  },
  computed: pageComputed,
  methods: pageMethods,
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);
    return h("v-list-page", {
      props,
      on: listeners
    });
  }
};
</script>
