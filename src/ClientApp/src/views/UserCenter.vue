<template>
  <v-layout justify-center app class="form">
    <v-flex xs12 sm10 md8>
      <v-card>
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>个人中心</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-divider></v-divider>
        <v-form ref="form">
          <v-card-text>
            <v-subheader class="subheader">头像</v-subheader>
            <v-flex xs12 sm6 md8 align-center justify-center layout text-xs-center>
              <v-avatar
                size="100"
                color="grey lighten-4"
                @click="uploadImg"
                class="handIcon"
                title="更换"
              >
                <img :src="handIcon" alt="avatar">
              </v-avatar>
            </v-flex>
            <v-text-field v-model="form.Account" label="帐号" readonly required></v-text-field>
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
            <v-text-field v-model="form.PhoneNumber" :rules="[rules.phone()]" label="手机号码" required></v-text-field>
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
          </v-card-text>
        </v-form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import rules from '@/rules'
import timg from '@/assets/timg.jpg'
import { alert, upload } from '@/utils'
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
  computed: {
    handIcon() {
      let id = this.form.HandIconId
      return id ? `/api/resource/get/${id}` : timg
    }
  },
  async created() {
    let request = await this.$http.get('/api/account/GetCurrent')
    this.form = request
  },
  methods: {
    async uploadImg() {
      let accept = 'image/gif, image/jpeg'
      let resources = await upload({ accept })
      this.form.HandIconId = resources[0].Id
    },
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

<style scoped> 
.subheader {
  padding: 0px;
}
.handIcon {
  cursor: pointer;
}
</style>
