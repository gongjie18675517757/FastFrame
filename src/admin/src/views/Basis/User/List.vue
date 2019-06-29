<script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户",
  childToolItems: [
    {
      name: "ToogleAdminIdentity",
      title: "切换身份[管理员<>普通用户]",
      icon: "perm_identity",
      disabled({ selection }) {
        return selection.length != 1;
      },
      async action({ selection, rows }) {
        let { Id } = selection[0];
        let result = await this.$http.put(
          `/api/user/ToogleAdminIdentity/${Id}`
        );
        alert.success("切换成功!");
        let index = rows.findIndex(r => r.Id == Id);
        if (index > -1) {
          rows.splice(index, 1, result);
        }
      }
    },
    {
      name: "ToogleDisabled",
      title: "切换状态[禁用<>启用]",
      icon: "sync_disabled",
      disabled({ selection }) {
        return selection.length != 1;
      },
      async action({ selection, rows }) {
        let { Id } = selection[0];
        let result = await this.$http.put(`/api/user/ToogleDisabled/${Id}`);
        alert.success("切换成功!");
        let index = rows.findIndex(r => r.Id == Id);
        if (index > -1) {
          rows.splice(index, 1, result);
        }
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




 
