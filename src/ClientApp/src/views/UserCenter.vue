<template>
  <v-layout justify-center app class="form">
    <v-flex xs12 sm10 md8 lg6>
      <v-card>
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>个人中心</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-divider></v-divider>
        <v-form ref="form">
          <v-card-text>
            <v-text-field v-model="form.Account" label="帐号" readonly required></v-text-field>
            <v-text-field
              :rules="[rules.required,rules.length(3,50)]"
              v-model="form.Name"
              label="名称"
              required
            ></v-text-field>
            <v-text-field
              :rules="[rules.required,rules.length(3,50)]"
              v-model="form.Password"
              label="密码"
              type="password"
              required
            ></v-text-field>
            <v-text-field v-model="form.Email" :rules="[rules.email]" label="邮箱" required></v-text-field>
            <v-text-field v-model="form.PhoneNumber" :rules="[rules.phone]" label="手机号码" required></v-text-field>
          </v-card-text>
        </v-form>
        <v-divider class="mt-5"></v-divider>
        <v-card-actions>
          <!-- <v-btn flat>取消</v-btn> -->
          <v-spacer></v-spacer>
          <!-- <v-slide-x-reverse-transition>
            <v-tooltip v-if="formHasErrors" left>
              <v-btn slot="activator" icon class="my-0" @click="resetForm">
                <v-icon>refresh</v-icon>
              </v-btn>
              <span>Refresh form</span>
            </v-tooltip>
          </v-slide-x-reverse-transition>-->

          <v-btn color="primary" flat @click="submit" :loading="submiting">保存</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import rules from '@/rules'
import { alert } from '@/utils'
export default {
  data() {
    return {
      rules: {
        ...rules
      },
      errMsgs: {},
      initform: null,
      form: {},
      submiting: false
    }
  },
  async created() {
    let request = await this.$http.get('/api/account/GetCurrent')
    this.form = request
  },
  methods: {
    async submit() {
      this.submiting = true
      try {
        if (!this.$refs.form.validate()) {
          throw new Error('请填写完整信息')
        }
        let request = await this.$http.put(
          '/api/Account/UpdateCurrUserInfo',
          this.form
        )
        this.form = request
        alert.success('更新成功')
      } catch (error) {
        alert.error(error.message)
      } finally {
        this.submiting = false
      }
    }
  }
}
</script>

<style>
.form {
  padding: 5px;
}
</style>
