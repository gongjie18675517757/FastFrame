<template>
  <v-layout justify-center app class="form">
    <v-flex xs12 sm10 md8>
      <v-card>
        <v-toolbar flat dense color="transparent">
          <v-toolbar-title>后台信息</v-toolbar-title>
          <v-spacer></v-spacer>
          <permission-facatory permission="Tenant.UpdateTenantInfo">
            <v-btn
              icon
              v-if="!canEdit"
              @click="canEdit = !canEdit"
              title="修改"
            >
              <v-icon>edit</v-icon>
            </v-btn>
          </permission-facatory>
        </v-toolbar>
        <v-divider></v-divider>
        <v-form ref="form">
          <v-card-text>
            <v-subheader class="subheader">头像</v-subheader>
            <v-flex
              xs12
              sm6
              md8
              align-center
              justify-center
              layout
              text-xs-center
            >
              <v-avatar
                size="100"
                color="grey lighten-4"
                @click="uploadImg"
                class="handIcon"
                title="更换"
              >
                <img :src="handIcon" alt="avatar" />
              </v-avatar>
            </v-flex>
            <v-text-field
              v-model="form.FullName"
              label="后台名称"
              :readonly="!canEdit"
              :rules="[rules.required(), rules.stringLength('', 3, 50)]"
            ></v-text-field>
            <v-text-field
              :rules="[rules.required(), rules.stringLength('', 3, 50)]"
              v-model="form.ShortName"
              label="公司简称"
              :readonly="!canEdit"
              required
            ></v-text-field>
            <!-- <v-text-field v-model="form.Email" :rules="[rules.email()]" label="邮箱" required></v-text-field>
            <v-text-field v-model="form.PhoneNumber" :rules="[rules.phone()]" label="手机号码" required></v-text-field>
            -->
            <v-divider class="mt-5"></v-divider>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="primary"
                text
                :disabled="!canEdit"
                @click="submit"
                :loading="submiting"
                >保存</v-btn
              >
            </v-card-actions>
          </v-card-text>
        </v-form>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import rules from "@/rules";
import timg from "@/assets/logo.png";
import { upload } from "@/utils";
import { getDownLoadPath } from "../config";
export default {
  data() {
    return {
      rules: {
        ...rules,
      },
      errMsgs: {},
      initform: null,
      form: {},
      submiting: false,
      canEdit: false,
    };
  },
  computed: {
    handIcon() {
      let id = this.form.HandIcon_Id;
      return id ? getDownLoadPath(id) : timg;
    },
  },
  async created() {
    let request = await this.$http.get("/api/Tenant/GetCurrent");
    this.form = request;
  },
  methods: {
    async uploadImg() {
      if (!this.canEdit) {
        window.open(this.handIcon);
        return;
      }
      let accept = "image/gif, image/jpeg";
      let [{ Id }] = await upload({ accept });
      this.form.HandIcon_Id = Id;
    },
    async submit() {
      this.submiting = true;
      try {
        if (!this.$refs.form.validate()) {
          throw new Error("请填写完整信息");
        }
        let request = await this.$http.put(
          "/api/Tenant/UpdateTenantInfo",
          this.form
        );
        this.form = request;
        this.canEdit = false;
        this.$store.commit({
          type: "setTenant",
          info: JSON.parse(JSON.stringify(request)),
        });
        this.$message.toast.success("更新成功");
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
.subheader {
  padding: 0px;
}
.handIcon {
  cursor: pointer;
}
</style>
