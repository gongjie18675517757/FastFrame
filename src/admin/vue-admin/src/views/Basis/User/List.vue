<script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户"
};
import Page from "../../../components/Page/ListPageCore.js";
import { makeTree } from "../../../components/Trees";
const TreeComponent = makeTree({ type_name: "Dept" });

export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
      toolItems: data.toolItems.concat(pageInfo.toolItems),
      treeComponent: TreeComponent
    };
  },
  methods: {
    ...Page.methods,
    getToolItems() {
      return Page.methods.getToolItems.call(this, ...arguments).then(arr => {
        return [
          ...arr,
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
              let index = rows.findIndex(r => r.Id == Id);
              if (index > -1) {
                rows.splice(index, 1, result);
              }
            }
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
              let result = await this.$http.put(
                `/api/user/ToogleDisabled/${Id}`
              );
              this.$message.toast.success("切换成功!");
              let index = rows.findIndex(r => r.Id == Id);
              if (index > -1) {
                rows.splice(index, 1, result);
              }
            }
          }
        ];
      });
    },
    getColumns() {
      return Page.methods.getColumns.call(this, ...arguments).then(arr => {
        return [
          ...arr,
          {
            Name: "Depts",
            Description: "所属科室",
            getValueFunc: ({ value }) => value.map(v => v.Value).join(",")
          },
          {
            Name: "Roles",
            Description: "拥有角色",
            getValueFunc: ({ value }) => value.map(v => v.Value).join(",")
          }
        ];
      });
    },
    getPageTitle(v) {
      if (v == null) {
        return "全部用户";
      }

      return `${v.Name}:下级用户`;
    },
    
  }
};
</script>




 
