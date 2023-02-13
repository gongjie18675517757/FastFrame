 <template>
  <v-page v-bind="childProps" v-on="childListeners">
    <!-- <template #test>
     <input type="datetime" name="" id="" f>
    </template> -->
  </v-page>
</template>

 <script>
let pageInfo = {
  area: "Basis",
  name: "User",
  direction: "用户",
};

import {
  FormPageDefines,
  makeFormPageInheritedFromBaseFormPage,
} from "../../../components/Page";

export default makeFormPageInheritedFromBaseFormPage({
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    async [FormPageDefines.MethodsDefines.fmtModelObject](super_func) {
      return {
        Depts: this.super_id ? [{ Id: this.super_id }] : [],
        Roles: [],
        ...(await super_func()),
      };
    },
    async [FormPageDefines.MethodsDefines.fmtModelObjectItems](super_func) {
      return [
        ...(await super_func()),
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
      ];
    },
  },
});
</script>  