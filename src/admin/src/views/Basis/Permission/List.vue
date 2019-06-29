<script>
let pageInfo = {
  area: "Basis",
  name: "Permission",
  direction: "权限",
  childToolItems: [
    {
      name: "InitPermission",
      title: "初始化权限",
      icon: "error_outline",
      async action() {
        await this.$http.post(`/api/Permission/InitPermission`);
        alert.success("初始化成功!");
        this.reload();
      }
    }
  ]
};

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
