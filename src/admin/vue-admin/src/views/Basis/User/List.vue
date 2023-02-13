<script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户",
};
import {
  makeListPageInheritedFromBaseListPage,
  ListPageDefines,
} from "../../../components/Page";

import { makeTree } from "../../../components/Trees";
const TreeComponent = makeTree({ type_name: "Dept" });

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      treeComponent: TreeComponent,
    };
  },
  methods: {
    async [ListPageDefines.MethodsDefines.getToolItems](super_func) {
      return [
        ...(await super_func()),
        {
          name: "ToogleAdminIdentity",
          permission: "User.ToogleAdminIdentity",
          title: "切换身份[管理员<>普通用户]",
          iconName: "perm_identity",
          disabled({ selection }) {
            return selection.length != 1;
          },
          async action({ selection, rows }) {
            let { Id } = selection[0];
            let result = await this.$http.put(
              `/api/user/ToogleAdminIdentity/${Id}`
            );
            this.$message.toast.success("切换成功!");
            let index = rows.findIndex((r) => r.Id == Id);
            if (index > -1) {
              rows.splice(index, 1, result);
            }
          },
        },
        {
          name: "ToogleDisabled",
          permission: "User.ToogleDisabled",
          title: "切换状态[禁用<>启用]",
          iconName: "sync_disabled",
          disabled({ selection }) {
            return selection.length != 1;
          },
          async action({ selection, rows }) {
            let { Id } = selection[0];
            let result = await this.$http.put(`/api/user/ToogleDisabled/${Id}`);
            this.$message.toast.success("切换成功!");
            let index = rows.findIndex((r) => r.Id == Id);
            if (index > -1) {
              rows.splice(index, 1, result);
            }
          },
        },
      ];
    },
    async [ListPageDefines.MethodsDefines.getColumns](super_func) {
      return [
        ...(await super_func()),
        {
          Name: "Depts",
          Description: "所属科室",
          getValueFunc: ({ value }) => value.map((v) => v.Value).join(","),
        },
        {
          Name: "Roles",
          Description: "拥有角色",
          getValueFunc: ({ value }) => value.map((v) => v.Value).join(","),
        },
      ];
    },
    [ListPageDefines.MethodsDefines.getPageTitle](super_func) {
      const v = super_func();
      if (v == null) {
        return "全部用户";
      }

      return `${v.Name}:下级用户`;
    },
  },
});
</script>




 
