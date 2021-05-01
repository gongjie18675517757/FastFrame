<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <v-app-bar color="primary" fixed dark dense app>
      <v-toolbar-title v-text="title"></v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <!-- <router-view/> -->
      <v-layout justify-center app class="form">
        <v-flex xs12 sm10 md8 lg6>
          <v-card>
            <v-toolbar flat   color="transparent"  >
              <v-toolbar-title>注册帐号</v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-divider></v-divider>
            <v-card-text>
              <v-form ref="form">
                <v-card-text>
                  <v-text-field
                    v-model="form.Account"
                    label="帐号"
                    :rules="[rules.required(), rules.stringLength('', 3, 50)]"
                  ></v-text-field>
                  <v-text-field
                    :rules="[rules.required(), rules.stringLength('', 3, 50)]"
                    v-model="form.Name"
                    label="名称"
                    required
                  ></v-text-field>
                  <v-text-field
                    :rules="[rules.required(), rules.stringLength('', 3, 50)]"
                    v-model="form.Password"
                    label="密码"
                    type="password"
                    required
                  ></v-text-field>
                  <v-text-field
                    v-model="form.Email"
                    :rules="[rules.email()]"
                    label="邮箱"
                    required
                  ></v-text-field>
                  <v-text-field
                    v-model="form.PhoneNumber"
                    :rules="[rules.phone()]"
                    label="手机号码"
                    required
                  ></v-text-field>
                   
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <SlideVerififyVue @success="submiting">
                      <template v-slot:activator="{ attrs, on }">
                        <v-btn
                          color="primary"
                          text 
                          :loading="submiting"
                          @click.stop="handleClick(on, $event)"
                          >注册</v-btn
                        >
                      </template>
                    </SlideVerififyVue>
                  </v-card-actions>
                  <v-footer :fixed="fixed" app inset>
                    <span>&copy; 2017</span>
                  </v-footer>
                </v-card-text>
              </v-form>
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>
    </v-main>
  </v-app>
</template>

<script>
import rules from "@/rules";
import SlideVerififyVue from "../components/SlideVerifify.vue";
export default {
  components: { SlideVerififyVue },
  data() {
    return {
      title: "XXX管理后台",
      fixed: true,
      rules: {
        ...rules,
      },
      errMsgs: {},
      initform: null,
      form: {
        Account: "",
        Password: "",
        Name: "",
        Email: "",
        PhoneNumber: "",
        Id: "",
      },

      submiting: false,
    };
  },
  computed: {},
  async created() {},
  methods: {
    handleClick(on,e) {
      if (!this.$refs.form.validate()) {
        this.$message.toast.warning("请填写完整信息!");
        return;
      }
      on.click(e);
    },
    async submit() {
      this.submiting = true;
      try {
        let request = await this.$http.post("/api/Account/Regist", this.form);
        this.form = request;
        this.$message.toast.success("注册成功");
        this.$router.push("/login");
      } catch (error) {
        this.$message.toast.error(error.message);
      } finally {
        this.submiting = false;
      }
    },
  },
};
</script>
<style scoped>
.v-toolbar {
  box-shadow: none;
  -webkit-box-shadow: none;
}
body {
  overflow: hidden;
}
</style>

 