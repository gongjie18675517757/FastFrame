<template>
  <v-container grid-list-xl fluid app>
    <v-flex lg12>
      <v-card>
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>添加{{moduleInfo.direction}}</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-menu offset-y>
            <v-btn icon slot="activator">
              <v-icon>more_vert</v-icon>
            </v-btn>
            <v-list>
              <v-list-tile>
                <v-list-tile-action>
                  <v-checkbox v-model="singleLine"></v-checkbox>
                </v-list-tile-action>
                <v-list-tile-content>
                  <v-list-tile-title>{{singleLine?'单行':'多行'}}</v-list-tile-title>
                </v-list-tile-content>
              </v-list-tile>
              <v-list-tile>
                <v-list-tile-action>
                  <v-checkbox v-model="showMamageField"></v-checkbox>
                </v-list-tile-action>
                <v-list-tile-content>
                  <v-list-tile-title>{{(showMamageField?'显示':'隐藏')+'管理字段'}}</v-list-tile-title>
                </v-list-tile-content>
              </v-list-tile>
            </v-list>
          </v-menu>
        </v-toolbar>
        <v-divider></v-divider>
        <v-form ref="form">
          <v-card-text>
            <component :is="singleLine?'v-flex':'v-layout'" wrap="">
              <TextInput
                v-for="item in options"
                :key="item.Name"
                :model="form"
                v-bind="item"
                :rules="getRules(item)"
              />
            </component>
            <component :is="singleLine?'v-flex':'v-layout'" wrap="">
              <TextInput
                v-if="showMamageField && form.Id"
                v-for="item in manageOptions"
                :key="item.Name"
                :model="form"
                v-bind="item"
              />
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
import { alert, mapMany } from '@/utils'
import { getDefaultModel, getFormItems, getRules } from '@/generate'
export default {
  components: {
    TextInput
  },
  props: {
    moduleInfo: {
      type: Object,
      default: function() {
        return {
          name: '',
          direction: '',
          pars: {}
        }
      }
    }
  },
  data() {
    return {
      timg: timg,

      form: {},
      options: [],
      rules: {},
      manageOptions: [
        { Name: 'CreateName', Description: '创建人' },
        { Name: 'CreateTime', Description: '创建时间' },
        { Name: 'ModifyName', Description: '最后修改人' },
        { Name: 'ModifyTime', Description: '最后修改时间' }
      ].map(r => {
        return {
          Name: r.Name,
          Type: 'String',
          Description: r.Description,
          Readonly: 'all'
        }
      }),
      submiting: false,
      singleLine: true,
      showMamageField: false
    }
  },

  watch: {
    $route: function() {
      this.load()
    }
  },
  async mounted() {
    let moduleName = this.moduleInfo.name
    this.rules = await getRules(moduleName)
    await this.load()
    this.options = await getFormItems(moduleName)
  },
  methods: {
    getId() {
      let { q: id } = this.$route.query
      if (this.moduleInfo.pars && this.moduleInfo.pars.id) {
        return this.moduleInfo.pars.id
      } else {
        let { q: id } = this.$route.query
        return id
      }
    },
    async load() {
      let id = this.getId()
      if (id) {
        this.form = await this.$http.get(
          `/api/${this.moduleInfo.name}/get/${id}`
        )
      } else {
        let moduleName = this.moduleInfo.name
        this.form = await getDefaultModel(moduleName)
      }
    },
    getRules(item) {
      return this.rules[item.Name].filter(f => f.length == 1)
    },
    goList() {
      this.$emit('success')
      this.$router.push(`/${this.moduleInfo.name}/list`)
    },
    async submit() {
      this.submiting = true
      try {
        if (!this.$refs.form.validate()) {
          throw new Error('请填写完整信息')
        }
        let id = this.getId()
        let data
        if (!id) {
          data = await this.$http.post(
            `/api/${this.moduleInfo.name}/post`,
            this.form
          )
        } else {
          data = await this.$http.put(
            `/api/${this.moduleInfo.name}/put`,
            this.form
          )
        }
        this.$emit('success', data)
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
