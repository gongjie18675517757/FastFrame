<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{singleLine:singleLine}">
      <v-flex v-bind="flex">
        <v-card>
          <v-toolbar flat dense color="transparent">
            <v-toolbar-title>{{title}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <a-btn
                icon
                v-if="hasManage && id && !changed"
                :moduleName="name"
                name="Update"
                @click="handleEdit"
                title="修改"
                color="info"
              >
                <v-icon>edit</v-icon>
              </a-btn>
              <!-- <v-btn icon @click="refresh" title="刷新">
                <v-icon>refresh</v-icon>
              </v-btn> -->
              <v-menu offset-y>
                <template v-slot:activator="{ on }">
                  <v-btn icon v-on="on" title="更多">
                    <v-icon>more_vert</v-icon>
                  </v-btn>
                </template>
                <v-list>
                  <v-list-item @click="$emit('toggle:singleLine')">
                    <v-list-item-action>
                      <v-checkbox :value="singleLine"></v-checkbox>
                    </v-list-item-action>
                    <v-list-item-content>
                      <v-list-item-title>{{'单行布局'}}</v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>
                  <v-list-item
                    v-if="hasManage &&  model.Id"
                    @click="$emit('toggle:showMamageField')"
                  >
                    <v-list-item-action>
                      <v-checkbox :value="showMamageField"></v-checkbox>
                    </v-list-item-action>
                    <v-list-item-content>
                      <v-list-item-title>{{ '显示管理字段'}}</v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>
                </v-list>
              </v-menu>
              <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
                <v-icon>close</v-icon>
              </v-btn>
            </v-toolbar-items>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text class="form-content" v-if="model">
              <div :class="[isDialog?'dialogPage':isTab?'tabPage':'fullPage','form-page']">
                <template v-for="group in formGroups">
                  <v-flex :key="group.key.title" xs12>
                    <v-card v-if="group.values.length>1" tile>
                      <v-toolbar flat dense color="transparent">
                        <v-toolbar-title>{{group.key.title}}:</v-toolbar-title>
                      </v-toolbar>
                      <v-card-text>
                        <component :is="singleLine?'span':'v-layout'" wrap>
                          <Input
                            v-for="item in group.values"
                            v-bind="item"
                            :key="item.Name"
                            :model="model"
                            :rules="getRules(item)"
                            :canEdit="canEdit"
                            :singleLine="singleLine"
                            :errorMessages="formErrorMessages[item.Name]"
                            :value="model[item.Name]"
                            @change="$emit('changed',{item:item,value:$event})"
                            @input="handleInput(item,$event)"
                            :ref="item.Name"
                          />
                        </component>
                      </v-card-text>
                    </v-card>
                    <component
                      v-else
                      v-for="item in group.values"
                      :key="item.Name"
                      :is="item.template"
                      v-bind="item"
                      :model="model"
                      :canEdit="canEdit"
                      :singleLine="singleLine"
                      :errorMessages="formErrorMessages[item.Name]"
                      :value="model[item.Name]"
                      @change="$emit('changed',{item:item,value:$event})"
                      @input="handleInput(item,$event)"
                      :ref="item.Name"
                    />
                  </v-flex>
                </template>
              </div>
            </v-card-text>
          </v-form>

          <v-card-actions>
            <v-btn text @click="$emit('close')">取消</v-btn>
            <v-spacer></v-spacer>
            <a-btn
              v-if="hasManage && canEdit && changed"
              :module-name="name"
              :name="model.Id?'Update':'Add'"
              color="primary"
              @click="$emit('submit')"
              :loading="submiting"
            >保存</a-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import Input from "@/components/Inputs";

export default {
  components: {
    Input,
    VuePerfectScrollbar
  },

  props: {
    isDialog: Boolean,
    isTab: Boolean,
    title: String,
    id: String,
    name: String,
    model: Object,
    formErrorMessages: Object,
    options: Array,
    rules: Object,
    submiting: Boolean,
    singleLine: Boolean,
    showMamageField: Boolean,
    canEdit: Boolean,
    changed: Boolean,
    hasManage: Boolean,
    formGroups: Array
  },
  data() {
    return {};
  },
  computed: {
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
    formGroupExpandValue: {
      get() {
        return this.formGroups.map(r => r.key.value);
      },
      set(val) {
        for (let index = 0; index < val.length; index++) {
          const value = val[index];
          this.formGroups[index].value = value;
        }
      }
    }
  },

  methods: {
    handleEdit() {
      this.$emit("tooggle:canEdit");
    },
    refresh() {
      this.$emit("reload");
    },
    getRules(item) {
      return item.rules || this.rules[item.Name];
    },
    handleInput(item, v) {
      this.model[item.Name] = v;
      this.$emit("tooggle:changed");
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

.tabPage {
  height: calc(100vh - 266px);
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
<style lang="stylus">
.form-content.v-card__text {
  padding: 5px;
}
</style>

