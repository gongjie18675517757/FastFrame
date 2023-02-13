<script>
let pageInfo = {
  area: "Basis",
  name: "LoginLog",
  direction: "登录记录",
};

import {
  makeListPageInheritedFromBaseListPage,
  ListPageDefines,
} from "../../../components/Page";
 

export default makeListPageInheritedFromBaseListPage({
  data() {
    return {
      ...pageInfo,
      [ListPageDefines.DataDefines.strutName]: "LoginLogModel",
    };
  },
  methods: {
    async [ListPageDefines.MethodsDefines.getRowOperateItems](super_func) {
      return [
        ...await super_func(),
        function (h, { model }) {
          return model.IsEnabled
            ? h(
                "permission-facatory",
                {
                  props: {
                    permission: "LoginLog.SetTokenFailure",
                  },
                },
                [
                  h(
                    "v-btn",
                    {
                      attrs: {
                        text: true,
                        small: true,
                        color: "primary",
                      },
                      on: {
                        click: () => {
                          event.stopPropagation();
                          this.$message
                            .confirm({
                              title: "提示",
                              content: "确认要强制失效这个token吗?",
                            })
                            .then(() => {
                              this.$http
                                .post(
                                  `/api/${pageInfo.name}/SetTokenFailure/${model.Id}`
                                )
                                .then(() => {
                                  model.IsEnabled = false;
                                  return this.$message.toast.success(
                                    "操作成功"
                                  );
                                });
                            });
                        },
                      },
                    },
                    "强制失效"
                  ),
                ]
              )
            : null;
        }.bind(this),
      ];
    },
  },
});
</script>
