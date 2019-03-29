<script>
import { alert } from "@/utils";
import {
  ListPageMixin,
  data,
  pageProps,
  pageListeners
} from "@/components/Page/ListPageCore.js";

export default {
  mixins: [ListPageMixin],
  data() {
    return {
      ...data,
      area: "Basis",
      name: "Role",
      direction: "角色",
      childToolItems: [
        {
          name: "SetRolePermission",
          title: "分配权限",
          icon: "error_outline",
          disabled({ selection }) {
            return selection.length != 1;
          },
          async action({ selection }) {
            let { Id } = selection[0];
            let data = await this.$http.get(
              `/api/role/GetRolePermission/${Id}`
            );
            let ids = this.$message.dialog(
              () => import("@/components/Page/TreeSelect"),
              {
                title: "设置权限",
                requestUrl: "/api/Permission/list",
                model: data.map(r => r.Id)
              }
            );
            await this.$http.put(`/api/role/SetRolePermission/${Id}`, ids);

            alert.success("设置成功!");
          }
        },
        {
          name: "SetRoleMember",
          title: "分配成员",
          icon: "error_outline",
          disabled({ selection }) {
            return selection.length != 1;
          },
          async action({ selection }) {
            let { Id } = selection[0];
            let data = await this.$http.get(`/api/role/GetRoleMember/${Id}`);
            let ids = await this.$message.dialog(
              () => import("@/components/Page/CheckGroup"),
              {
                title: "角色成员",
                requestUrl: "/api/User/list",
                model: data.map(r => r.Id),
                labelFormatter(item) {
                  return `${item.Name}[${item.Account}]`;
                }
              }
            );
            await this.$http.put(`/api/role/SetRoleMember/${Id}`, ids);

            alert.success("设置成功!");
          }
        }
      ]
    };
  },
  render(h) {
    let props = pageProps.call(this),
      listeners = pageListeners.call(this);
    return h("v-list-page", {
      props,
      on: listeners
    });
  }
};
</script>

