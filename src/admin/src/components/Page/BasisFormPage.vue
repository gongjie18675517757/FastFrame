<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{singleLine:singleLine}">
      <v-flex v-bind="flex">
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{title}}{{moduleInfo.direction}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <a-btn
              icon
              v-if="hasManage && getId() && !changed"
              :moduleName="moduleInfo.name"
              name="Update"
              @click="handleEdit"
              title="修改"
            >
              <v-icon>edit</v-icon>
            </a-btn>
            <v-btn icon @click="refresh" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <v-menu offset-y>
              <v-btn icon slot="activator" title="设置">
                <v-icon>more_vert</v-icon>
              </v-btn>
              <v-list>
                <v-list-tile @click="singleLine=!singleLine">
                  <v-list-tile-action>
                    <v-checkbox :value="singleLine"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>{{'单行布局'}}</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-list-tile v-if="hasManage &&  form.Id" @click="showMamageField=!showMamageField">
                  <v-list-tile-action>
                    <v-checkbox :value="showMamageField"></v-checkbox>
                  </v-list-tile-action>
                  <v-list-tile-content>
                    <v-list-tile-title>{{ '显示管理字段'}}</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-menu>
            <v-btn icon @click="cancel" title="关闭" v-if="pageInfo.success">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text>
              <vue-perfect-scrollbar :class="[!this.pageInfo.close?'fullPage':'dialogPage','form-page']">
                <v-expansion-panel expand v-model="formGroupExpandValue" style="margin-top:-15px;">
                  <v-expansion-panel-content v-for="group in formGroup" :key="group.key.title">
                    <template v-slot:header>
                      <div class="form-page-group-header">{{group.key.title}}</div>
                    </template>
                    <v-card>
                      <v-card-text>
                        <component :is="singleLine?'span':'v-layout'" wrap>
                          <Input
                            v-for="item in group.values"
                            v-bind="item"
                            :key="item.Name"
                            :model="form"
                            :rules="getRules(item)"
                            :canEdit="canEdit"
                            :singleLine="singleLine"
                            :errorMessages="formErrorMessages[item.Name]"
                            @change="changed=true"
                            @update_errorMessages="formErrorMessages[item.Name]=$event"
                            :ref="item.Name"
                          />
                        </component>
                      </v-card-text>
                    </v-card>
                  </v-expansion-panel-content>
                </v-expansion-panel>
              </vue-perfect-scrollbar>
            </v-card-text>
          </v-form>

          <v-card-actions>
            <v-btn flat @click="cancel">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn
              v-if="hasManage && canEdit && changed"
              color="primary"
              flat
              @click="submit"
              :loading="submiting"
            >保存</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import Input from "@/components/Inputs";
// import timg from "@/assets/timg.jpg";
import { alert, groupBy, selectMany } from "@/utils";
import { getDefaultModel, getFormItems, getRules, hasManage } from "@/generate";
export default {
  components: {
    Input,
    VuePerfectScrollbar
  },
  inject: ["reload"],
  props: {
    moduleInfo: {
      type: Object,
      default: function() {
        return {
          name: "",
          direction: ""
        };
      }
    },
    pageInfo: {
      type: Object,
      default: function() {
        return {
          pars: {}
        };
      }
    }
  },
  data() {
    return {
      form: {},
      formErrorMessages: {},
      options: [],
      rules: {},
      manageOptions: [
        { Name: "Create_User.Name", Description: "创建人" },
        { Name: "CreateTime", Description: "创建时间" },
        { Name: "Modify_User.Name", Description: "最后修改人" },
        { Name: "ModifyTime", Description: "最后修改时间" }
      ].map(r => {
        return {
          Name: r.Name,
          Type: "String",
          Description: r.Description,
          Readonly: "All",
          FormGroup: ["管理字段"]
        };
      }),
      submiting: false,
      singleLine: false,
      showMamageField: false,
      canEdit: false,
      changed: false,
      hasManage: false
    };
  },
  watch: {
    $route: function() {
      this.load();
    }
  },
  computed: {
    title() {
      if (!this.getId()) return "新增";
      else if (this.canEdit) return "修改";
      else return "查看";
    },
    flex() {
      if (!this.singleLine) {
        return {
          xs12: true
        };
      } else {
        return {
          xs12: true,
          sm10: true,
          md8: true,
          lg6: true,
          xl6: true
        };
      }
    },
    formGroup() {
      let opts = this.options || [];
      if (this.hasManage && this.showMamageField && this.form && this.form.Id) {
        opts = [...opts, ...this.manageOptions];
      }

      let brr = selectMany(opts, r => {
        return (r.FormGroup || ["基础信息"]).map(p => {
          return {
            ...r,
            title: p
          };
        });
      });

      let arr = groupBy(
        brr,
        r => ({
          title: r.title,
          value: true
        }),
        (a, b) => a.title == b.title
      );
      return arr;
    },
    formGroupExpandValue: {
      get() {
        return this.formGroup.map(r => r.key.value);
      },
      set(val) {
        for (let index = 0; index < val.length; index++) {
          const value = val[index];
          this.formGroup[index].value = value;
        }
      }
    }
  },
  created() {
    let id = this.getId();
    if (!id) this.canEdit = true;
  },
  async mounted() {
    let moduleName = this.moduleInfo.name;
    this.hasManage = await hasManage(moduleName);

    let rules = await getRules(moduleName);
    if (typeof this.moduleInfo.formatterRules == "function")
      rules = await this.moduleInfo.formatterRules(rules);
    this.rules = rules;

    await this.load();

    let options = await getFormItems(moduleName);

    if (typeof this.moduleInfo.formatterOptions == "function") {
      options = await this.moduleInfo.formatterOptions(options);
    }

    this.options = options;

    this.$eventBus.$on(`${this.moduleInfo.name}_DataUpdated`, this.DataUpdated);
    this.$eventBus.$on(`${this.moduleInfo.name}_DataDeleted`, this.DataDeleted);
  },
  destroyed() {
    this.$eventBus.$off(
      `${this.moduleInfo.name}_DataUpdated`,
      this.DataUpdated
    );
    this.$eventBus.$off(
      `${this.moduleInfo.name}_DataDeleted`,
      this.DataDeleted
    );
  },
  methods: {
    DataUpdated() {
      this.load();
    },
    async DataDeleted() {
      await this.$message.alert("提示", "当前内容已被其它人删除!");
      this.cancel();
    },
    handleEdit() {
      this.canEdit = !this.canEdit;
    },
    getId() {
      if (this.pageInfo.pars && this.pageInfo.pars.id) {
        return this.pageInfo.pars.id;
      } else {
        let { q: id } = this.$route.query;
        return id;
      }
    },
    refresh() {
      this.reload();
    },
    async load() {
      let id = this.getId(),
        form;
      if (id) {
        form = await this.$http.get(`/api/${this.moduleInfo.name}/get/${id}`);
      } else {
        let moduleName = this.moduleInfo.name;
        form = await getDefaultModel(moduleName);
      }
      if (typeof this.moduleInfo.formatterForm == "function")
        form = await this.moduleInfo.formatterForm(form);

      let formErrs = {};
      for (const name of Object.keys(form)) {
        formErrs[name] = [];
      }

      this.formErrorMessages = formErrs;
      this.form = form;
    },
    getRules(item) {
      return this.rules[item.Name];
    },
    goList() {
      this.$router.push(`/${this.moduleInfo.name}/list`);
    },
    async submit() {
      this.submiting = true;
      try {
        let errs = [];
        let names = Object.keys(this.form);
        for (const name of names) {
          if (
            this.$refs[name] &&
            this.$refs[name].length > 0 &&
            typeof this.$refs[name][0].evalRules == "function"
          ) {
            let err = await this.$refs[name][0].evalRules();
            errs.push(...err);
          }
        }

        if (errs.length > 0) {
          alert.error("表单填写不完整");
          return;
        }

        let id = this.getId(),
          data,
          postData = { ...this.form };
        delete postData.Create_User;
        delete postData.Modify_User;
        if (!id) {
          data = await this.$http.post(
            `/api/${this.moduleInfo.name}/post`,
            postData
          );
        } else {
          data = await this.$http.put(
            `/api/${this.moduleInfo.name}/put`,
            postData
          );
        }
        this.$emit("success", data);
        if (typeof this.pageInfo.success == "function") {
          this.pageInfo.success(data);
        } else {
          this.goList();
        }
      } catch (error) {
        alert.error(error.message);
      } finally {
        this.submiting = false;
      }
    },
    cancel() {
      this.$emit("close");
      if (typeof this.pageInfo.close == "function") {
        this.pageInfo.close();
      } else {
        this.goList();
      }
    }
  }
};
</script>

<style scoped lang="stylus">
.subheader {
  padding: 0px;
}

.handIcon {
  cursor: pointer;
}

.fullPage {
  height: calc(100vh - 225px);
  overflow: auto;
}

.dialogPage {
  height: calc(100vh - 245px);
  overflow: auto;
}

.singleLine .v-card__text {
  padding: 5px;
}

.container.grid-list-xl .layout.singleLine .flex {
  padding: 5px;
}
</style>
