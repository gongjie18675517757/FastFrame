 <template>
  <v-page v-bind="childProps" v-on="childListeners">
    <template #test>
      <h1>
        测试插槽能力
      </h1>
    </template>
  </v-page>
</template>

 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户",
};

import Page from "../../../components/Page/FormPageCore.js";
 
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

    fmtModelObject() {
      return Page.methods.fmtModelObject
        .call(this, ...arguments)
        .then((model) => {
          return {
            Depts: this.super_id ? [{ Id: this.super_id }] : [],
            Roles: [],
            ...model,
          };
        });
    },
    getModelObjectItems() {
      return Page.methods.getModelObjectItems
        .call(this, ...arguments)
        .then((opts) => {
          return [
            ...opts,

            {
              Name: "Depts",
              Description: "用户所在部门",
              Relate: "Dept",
              Type: "Array",
              requestUrl: `/api/user/deptList`,
              flex: { xs6: true },
              visible: () => !this.super_id,
            },
            {
              Name: "Roles",
              Description: "用户拥有角色",
              Relate: "Role",
              Type: "Array",
              requestUrl: `/api/user/roleList`,
              flex: { xs6: true },
            },
            {
              Name: "test",
             
            },
          ];
        });
    },
  },
};
</script>  