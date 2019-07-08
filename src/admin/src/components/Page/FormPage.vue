<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{singleLine:singleLine}">
      <v-flex v-bind="flex">
        <v-card>
          <v-toolbar flat dense card color="transparent">
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
              <v-btn icon @click="refresh" title="刷新">
                <v-icon>refresh</v-icon>
              </v-btn>
              <v-menu offset-y>
                <v-btn icon slot="activator" title="更多">
                  <v-icon>more_vert</v-icon>
                </v-btn>
                <v-list>
                  <v-list-tile @click="$emit('toggle:singleLine')">
                    <v-list-tile-action>
                      <v-checkbox :value="singleLine"></v-checkbox>
                    </v-list-tile-action>
                    <v-list-tile-content>
                      <v-list-tile-title>{{'单行布局'}}</v-list-tile-title>
                    </v-list-tile-content>
                  </v-list-tile>
                  <v-list-tile
                    v-if="hasManage &&  form.Id"
                    @click="$emit('toggle:showMamageField')"
                  >
                    <v-list-tile-action>
                      <v-checkbox :value="showMamageField"></v-checkbox>
                    </v-list-tile-action>
                    <v-list-tile-content>
                      <v-list-tile-title>{{ '显示管理字段'}}</v-list-tile-title>
                    </v-list-tile-content>
                  </v-list-tile>
                </v-list>
              </v-menu>
              <v-btn icon @click="$emit('cancel')" title="关闭" v-if="isDialog">
                <v-icon>close</v-icon>
              </v-btn>
            </v-toolbar-items>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text class="form-content">
              <vue-perfect-scrollbar :class="[!isDialog?'fullPage':'dialogPage','form-page']">
                <template v-for="group in formGroups">
                  <v-flex :key="group.key.title" xs12>
                    <v-card v-if="group.values.length>1" tile>
                      <v-toolbar flat dense card color="transparent">
                        <v-toolbar-title>{{group.key.title}}:</v-toolbar-title>
                      </v-toolbar>
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
                            @change="$emit('changed',{item:item,value:$event})"
                            @input="$emit('tooggle:changed')"
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
                      :model="form"
                      :canEdit="canEdit"
                      :singleLine="singleLine"
                      :errorMessages="formErrorMessages[item.Name]"
                      v-model="form[item.Name]"
                      @change="$emit('changed',{item:item,value:$event})"
                      @input="$emit('tooggle:changed')"
                      :ref="item.Name"
                    />
                  </v-flex>
                </template>
              </vue-perfect-scrollbar>
            </v-card-text>
          </v-form>

          <v-card-actions>
            <v-btn flat @click="$emit('cancel')">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn
              v-if="hasManage && canEdit && changed"
              color="primary"
              flat
              @click="$emit('submit')"
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

export default {
  components: {
    Input,
    VuePerfectScrollbar
  },
  inject: ["reload"],
  props: {
    isDialog: Boolean,
    title: String,
    id: String,
    name: String,
    form: Object,
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
  watch: {
    $route: function() {
      this.$emit("reload");
    }
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
      this.reload();
    },
    getRules(item) {
      return item.rules || this.rules[item.Name];
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
<style lang="stylus">
.form-content.v-card__text {
  padding: 5px;
}
</style>

