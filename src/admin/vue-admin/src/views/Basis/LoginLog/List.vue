<script>
let pageInfo = {
  area: "Basis",
  name: "LoginLog",
  direction: "登录记录",
};

import Page from "../../../components/Page/ListPageCore.js";
import { getColumns } from "../../../generate";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo,
    };
  },
  methods: {
    ...Page.methods,
    getColumns() {
      return getColumns("LoginLogModel");
    },
    getRowOperateItems() {
      return Page.methods.getRowOperateItems
        .call(this, ...arguments)
        .then((arr) => {
          return [
            ...arr,
            (h, { model }) =>
              model.IsEnabled
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
                : null,
          ];
        });
    },
  },
};
</script>
