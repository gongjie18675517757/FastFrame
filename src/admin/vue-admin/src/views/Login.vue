<template>
  <v-app id="login" class="primary">
    <v-main>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4 lg4 xl3>
            <v-card class="elevation-1 pa-3">
              <v-card-text>
                <div class="layout column align-center">
                  <img
                    src="@/assets/logo.png"
                    alt="Vue Material Admin"
                    width="120"
                    height="120"
                  />
                  <h1 class="flex my-4 primary--text">XXX管理平台</h1>
                </div>
                <v-form v-model="valid" lazy-validation ref="form">
                  <v-text-field
                    append-icon="person"
                    label="帐号"
                    type="text"
                    v-model="model.account"
                    required
                    @keydown.enter="login"
                    placeholder="请输入您的帐号"
                    :rules="accountRules"
                  ></v-text-field>
                  <v-text-field
                    append-icon="lock"
                    label="密码"
                    type="password"
                    required
                    v-model="model.password"
                    placeholder="请输入您的密码"
                    @keydown.enter="login"
                    :rules="passwordRules"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <SlideVerififyVue :v="v" @success="login">
                  <template v-slot:activator="{ attrs, on }">
                    <v-btn
                      block
                      color="primary"
                      v-bind="attrs"
                      @click.stop="handleClick(on, $event)"
                      :loading="loading"
                      >登陆</v-btn
                    >
                  </template>
                </SlideVerififyVue>
              </v-card-actions>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text small to="/regist">注册</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
import SlideVerififyVue from "../components/SlideVerifify.vue";
import { RSAEncrypt } from '../utils';

export default {
  components: {
    SlideVerififyVue,
  },
  data: () => ({
    v: new Date().getTime(),
    loading: false,
    model: {
      account: "admin",
      password: "000000",
    },
    valid: true,
    accountRules: [
      (v) => !!v || "请填写帐号",
      (v) => (v && v.length >= 4 && v.length < 20) || "长度不正确",
    ],
    passwordRules: [
      (v) => !!v || "请填写密码",
      (v) => (v && v.length >= 4 && v.length < 20) || "长度不正确",
    ],
    verifying: false,
  }),
  methods: {
    handleClick(on, e) {
      if (!this.$refs.form.validate()) {
        this.$message.toast.warning("请填写完整信息!");
        return;
      }
      on.click(e);
    },
    async login() {
      this.loading = true;
      try {
        const {account,password}=this.model
        /**
         * 登陆
         */
        await this.$http.post("/api/account/login", {
          account:await RSAEncrypt(account),
          password:await RSAEncrypt(password),
        });

        /**
         * 验证登陆状态
         */
        await this.$store.dispatch("existsIdentity");

        /**
         * 跳转
         */
        let redirect = this.$route.query.redirect || "/";
        this.$router.push(redirect);
      } catch {
        this.v = new Date().getTime();
      } finally {
        this.loading = false;
      }
    },
  },
};
</script>
<style scoped lang="css">
#login {
  height: 50%;
  width: 100%;
  position: absolute;
  top: 0;
  left: 0;
  content: "";
  z-index: 0;
}
</style>
