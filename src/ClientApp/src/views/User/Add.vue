<template>
  <v-container grid-list-xl fluid app>
    <v-flex lg12>
      <v-card>
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>添加用户</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-menu offset-y>
            <v-btn icon slot="activator">
              <v-icon>settings</v-icon>
            </v-btn>
            <v-list>
              <v-list-tile>
                <v-list-tile-title>
                  <v-switch :label="singleLine?'单行':'多行'" v-model="singleLine"></v-switch>
                </v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
        </v-toolbar>
        <v-divider></v-divider>
        <v-form ref="form">
          <v-card-text>
            <component :is="singleLine?'v-flex':'v-layout'" wrap=""> 
              <TextInput v-for="item in options" :key="item.Name" :model="form" v-bind="item"/>
            </component>
            <v-divider class="mt-5"></v-divider>
            <v-card-actions>
              <v-btn flat @click="goList">取消</v-btn>
              <v-spacer></v-spacer>
              <v-btn color="primary" flat @click="submit" :loading="submiting">保存</v-btn>
            </v-card-actions>
          </v-card-text>
        </v-form>
      </v-card>
    </v-flex>
  </v-container>
</template>
<script>

import TextInput from '@/components/Inputs/TextInput.vue'
import timg from '@/assets/timg.jpg'
import { alert } from '@/utils'
import { list } from '@/api/user'
import { getDefaultModel, getFormItems } from '@/generate'
export default {
  components: {
    TextInput
  },
  data() {
    return {
      form: {},
      options: [],
      submiting: false,
      singleLine: true
    }
  },
  async created() {
    let moduleName = 'User'
    this.form = await getDefaultModel(moduleName)
    this.options = await getFormItems(moduleName)
  },
  methods: {
    goList() {
      this.$router.push('/user/list')
    },
    async submit() {
      this.submiting = true
      try {
        if (!this.$refs.form.validate()) {
          throw new Error('请填写完整信息')
        }
        let data = await this.$http.post('/api/user/post', this.form)
        this.form = data
        list.push(data)
        alert.success('添加成功')
        this.goList()
      } catch (error) {
        // alert.error(error.message)
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
