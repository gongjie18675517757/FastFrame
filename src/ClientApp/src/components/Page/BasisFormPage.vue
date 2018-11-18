<template>
  <v-container grid-list-xl fluid app>
    <v-flex xs12>
      <v-card v-bind="flex">
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>{{title}}{{moduleInfo.direction}}</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn icon v-if="getId() && !changed" @click="canEdit=!canEdit" title="修改">
            <v-icon>edit</v-icon>
          </v-btn>
          <v-btn icon @click="refresh" title="刷新">
            <v-icon>refresh</v-icon>
          </v-btn>
          <v-menu offset-y>
            <v-btn icon slot="activator" title="设置">
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
                :canEdit="canEdit"
                @change="changed=true"
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
              <v-btn flat @click="cancel">取消</v-btn>
              <v-spacer></v-spacer>
              <v-btn
                v-if="canEdit && changed"
                color="primary"
                flat
                @click="submit"
                :loading="submiting"
              >保存</v-btn>
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
  inject: ['reload'],
  props: {
    moduleInfo: {
      type: Object,
      default: function() {
        return {
          name: '',
          direction: ''
        }
      }
    },
    pageInfo: {
      type: Object,
      default: function() {
        return {
          pars: {}
        }
      }
    }
  },
  data() {
    return {
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
      singleLine: false,
      showMamageField: false,
      canEdit: false,
      changed: false
    }
  },
  watch: {
    $route: function() {
      this.load()
    }
  },
  computed: {
    title() {
      if (!this.getId()) return '新增'
      else if (this.canEdit) return '修改'
      else return '查看'
    },
    flex() {
      if (!this.singleLine) {
        return {
          xs12: ''
        }
      } else {
        return {
          xs6: ''
        }
      }
    }
  },
  created() {
    let id = this.getId()
    if (!id) this.canEdit = true
  },
  async mounted() {
    let moduleName = this.moduleInfo.name

    let rules = await getRules(moduleName)
    if (typeof this.moduleInfo.formatterRules == 'function')
      rules =await this.moduleInfo.formatterRules(rules)
    this.rules = rules

    await this.load()

    let options = await getFormItems(moduleName)
    if (typeof this.moduleInfo.formatterRules == 'function')
      options =await this.moduleInfo.formatterOptions(options)
    this.options = options
  },
  methods: {
    getId() {
      let { q: id } = this.$route.query
      if (this.pageInfo.pars && this.pageInfo.pars.id) {
        return this.pageInfo.pars.id
      } else {
        let { q: id } = this.$route.query
        return id
      }
    },
    refresh() {
      this.reload()
    },
    async load() {
      let id = this.getId(),
        form
      if (id) {
        form = await this.$http.get(`/api/${this.moduleInfo.name}/get/${id}`)
      } else {
        let moduleName = this.moduleInfo.name
        form = await getDefaultModel(moduleName)
      }
      if (typeof this.moduleInfo.formatterForm == 'function')
        form =await this.moduleInfo.formatterForm(form)
      this.form = form
    },
    getRules(item) {
      return this.rules[item.Name].filter(f => f.length == 1)
    },
    goList() {
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
        if (typeof this.pageInfo.success == 'function') {
          this.pageInfo.success(data)
        } else {
          this.goList()
        }
      } catch (error) {
        // alert.error(error.message)
      } finally {
        this.submiting = false
      }
    },
    cancel() {
      this.$emit('close')
      if (typeof this.pageInfo.close == 'function') {
        this.pageInfo.close()
      } else {
        this.goList()
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
