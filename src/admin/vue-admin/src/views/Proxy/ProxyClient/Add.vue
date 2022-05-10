 <template>
  <v-page v-bind="childProps" v-on="childListeners">
    <template #test>
      <h1>测试插槽能力</h1>
    </template>
  </v-page>
</template>
<script>
let pageInfo = {
  area: "Proxy",
  name: "ProxyClient",
  direction: "内网穿透服务",
};
import Page from "../../../components/Page/FormPageCore.js";
import { getDefaultModel, getEnumValues } from "../../../generate.js";
import { createObject } from "../../../utils/index.js";
export default {
  ...Page,
  data() {
    let data = Page.data.call(this);
    return {
      ...data,
      ...pageInfo,
    };
  },
  methods: {
    ...Page.methods,
    async fmtModelObject() {
      const model = await Page.methods.fmtModelObject.call(this, ...arguments);
      const proxy_enum_obj = createObject(
        await getEnumValues("ProxyTarget", "TargetEnum"),
        (v) => v.Key,
        () => null
      );

      return {
        ProxyDic: proxy_enum_obj,
        ...model,
      };
    },
    async fmtModelObjectItems() {
      const arr = await Page.methods.fmtModelObjectItems.call(
        this,
        ...arguments
      );
      const proxy_enum_obj = createObject(
        await getEnumValues("ProxyTarget", "TargetEnum")
      );

      const defaultModel = await getDefaultModel("ProxyTarget");
      return [
        ...arr,
        {
          Name: `ProxyDic`,
          Description: `代理内容`,
          GroupNames: ["代理选项"],
          component: {
            functional: true,
            render: (h, { props, listeners }) => {
              const { value } = props;
              return h(
                "fragments-facatory",
                null,
                Object.entries(proxy_enum_obj).map(([k, v]) =>
                  h("v-checkbox", {
                    key: k,
                    props: {
                      inputValue: Object.entries(proxy_enum_obj)
                        .filter(([k]) => value[k])
                        .map(([k]) => k),
                      label: v,
                      value: k,
                      multiple: true,
                    },
                    on: {
                      change: (arr) => {
                        for (const [k] of Object.entries(proxy_enum_obj)) {
                          if (arr.includes(k) && !value[k]) {
                            value[k] = { ...defaultModel };
                          }
                          if (!arr.includes(k) && value[k]) {
                            value[k] = null;
                          }
                        }

                        listeners?.input(value);
                        listeners?.change(value);
                      },
                    },
                  })
                )
              );
            },
          },
        },
        // ...mapMany(Object.entries(proxy_enum_obj).map(([k, v]) => [])),
      ];
    },
  },
};
</script>
