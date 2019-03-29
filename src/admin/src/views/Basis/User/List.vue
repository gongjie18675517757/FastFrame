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
        },
        {
          name: "SetUserRoles",
          title: "分配角色",
          icon: "error_outline",
          disabled({ selection }) {
            return selection.length != 1;
          },
          async action({ selection }) {
            let { Id } = selection[0];
            let data = await this.$http.get(`/api/user/GetUserRoles/${Id}`);
            let ids = this.$message.dialog(
              () => import("@/components/Page/CheckGroup"),
              {
                title: "角色成员",
                requestUrl: "/api/Role/list",
                model: data.map(r => r.Id),
                labelFormatter(item) {
                  return `${item.Name}[${item.EnCode}]`;
                }
              }
            );
            await this.$http.put(`/api/user/SetUserRoles/${Id}`, ids);

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




 
