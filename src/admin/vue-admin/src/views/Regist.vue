<template>
  <v-app color="primary" fixed :dark="$vuetify.dark" app>
    <v-toolbar color="primary" fixed app dark dense>
      <v-toolbar-title v-text="title"></v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-content>
      <!-- <router-view/> -->
      <v-layout justify-center app class="form">
        <v-flex xs12 sm10 md8 lg6>
          <v-card>
            <v-toolbar text dense   color="transparent">
              <v-toolbar-title>注册帐号</v-toolbar-title>
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-divider></v-divider>
            <v-form ref="form">
              <v-card-text>
                <v-text-field
                  v-model="form.Account"
                  label="帐号"
                  :rules="[rules.required(),rules.stringLength('',3,50)]"
                ></v-text-field>
                <v-text-field
                  :rules="[rules.required(),rules.stringLength('',3,50)]"
                  v-model="form.Name"
                  label="名称"
                  required
                ></v-text-field>
                <v-text-field
                  :rules="[rules.required(),rules.stringLength('',3,50)]"
                  v-model="form.Password"
                  label="密码"
                  type="password"
                  required
                ></v-text-field>
                <v-text-field v-model="form.Email" :rules="[rules.email()]" label="邮箱" required></v-text-field>
                <v-text-field
                  v-model="form.PhoneNumber"
                  :rules="[rules.phone()]"
                  label="手机号码"
                  required
                ></v-text-field>
                <v-divider class="mt-5"></v-divider>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="primary" text @click="submit" :loading="submiting">注册</v-btn>
                </v-card-actions>
                <v-footer :fixed="fixed" app inset>
                  <span>&copy; 2017</span>
                </v-footer>
              </v-card-text>
            </v-form>
          </v-card>
        </v-flex>
      </v-layout>
    </v-content>
  </v-app>
</template>

<script>
 

import rules from '@/rules'
 
import { alert } from '@/utils'
export default {
  components: {
     
  },
  data() {
    return {
      title: 'XXX管理后台',
      fixed: true,
      rules: {
        ...rules
      },
      errMsgs: {},
      initform: null,
      form: {
        Account: '',
        Password: '',
        Name: '',
        Email: '',
        PhoneNumber: '',
        Id: ''
      },

      submiting: false
    }
  },
  computed: {},
  async created() {},
  methods: {
    async submit() {
      this.submiting = true
      try {
        if (!this.$refs.form.validate()) {
          throw new Error('请填写完整信息')
        }
        let request = await this.$http.post('/api/Account/Regist', this.form)
        this.form = request
        alert.success('注册成功')
        this.$router.push('/login')
      } catch (error) {
        alert.error(error.message)
      } finally {
        this.submiting = false
      }
    }
  }
}
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

 