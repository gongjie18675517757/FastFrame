<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{ singleLine: singleLine }">
      <v-flex v-bind="flex" :style="{ padding: isTab ? '1px' : null }">
        <v-card>
          <v-toolbar flat dense color="transparent">
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items v-if="!isDialog">
              <permission-facatory
                v-for="btn in visibleToolItems.filter(
                  () => !$vuetify.breakpoint.smAndDown
                )"
                :key="btn.key || btn.name"
                :permission="btn.permission"
              >
                <v-btn
                  @click="evalAction(btn)"
                  text
                  small
                  color="primary"
                  :disabled="evalDisabled(btn)"
                  v-bind="btn"
                >
                  <v-icon v-if="btn.iconName">{{ btn.iconName }}</v-icon>
                  &emsp; {{ btn.title }}
                </v-btn>
              </permission-facatory>
            </v-toolbar-items>
            <v-menu offset-y>
              <template v-slot:activator="{ on }">
                <v-btn v-on="on" color="primary" text small title="更多">
                  <v-icon>more_vert</v-icon>
                  设置
                </v-btn>
              </template>

              <v-list dense>
                <permission-facatory
                  v-for="item in [
                    ...($vuetify.breakpoint.smAndDown ? visibleToolItems : [])
                  ]"
                  :key="item.key || item.name"
                  :permission="item.permission"
                >
                  <v-list-item
                    :title="item.title"
                    :disabled="evalDisabled(item)"
                    @click="evalAction(item)"
                  >
                    <v-list-item-action>
                      <v-icon>{{ item.iconName }}</v-icon>
                    </v-list-item-action>

                    <v-list-item-content>
                      <v-list-item-title>{{ item.title }}</v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>
                </permission-facatory>

                <v-list-item @click="$emit('toggle:singleLine')">
                  <v-list-item-action>
                    <v-checkbox :value="singleLine"></v-checkbox>
                  </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>{{ "单行布局" }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item
                  v-if="hasManage && model.Id"
                  @click="$emit('toggle:showMamageField')"
                >
                  <v-list-item-action>
                    <v-checkbox :value="showMamageField"></v-checkbox>
                  </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>{{ "显示管理字段" }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-menu>
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-card-text
            class="form-content"
            v-if="model"
            :class="[
              isDialog ? 'dialogPage' : isTab ? 'tabPage' : 'fullPage',
              'form-page'
            ]"
          >
            <v-layout wrap style="padding: 0px; margin: 0px">
              <template v-for="group in formGroups">
                <v-flex
                  :key="group.key.title"
                  xs12
                  :xl10="!isDialog || singleLine"
                  style="padding: 5px; margin: 0 auto"
                >
                  <v-card v-if="group.values.length > 1" tile>
                    <v-toolbar flat color="transparent" dense>
                      <v-toolbar-title>{{ group.key.title }}:</v-toolbar-title>
                    </v-toolbar>
                    <v-divider></v-divider>
                    <v-card-text>
                      <component
                        :is="singleLine ? 'div' : 'v-layout'"
                        wrap
                        style="padding: 15px"
                      >
                        <Input
                          v-for="item in group.values"
                          v-bind="item"
                          :key="item.Name"
                          :model="model"
                          :canEdit="canEdit"
                          :disabled="!canEdit"
                          :singleLine="singleLine"
                          :errorMessages="formErrorMessages[item.Name]"
                          :value="model[item.Name]"
                          @change="
                            $emit('changed', { item: item, value: $event })
                          "
                          @input="handleInput(item, $event)"
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
                    :disabled="!canEdit"
                    :singleLine="singleLine"
                    :errorMessages="formErrorMessages[item.Name]"
                    :value="model[item.Name]"
                    @change="$emit('changed', { item: item, value: $event })"
                    @input="handleInput(item, $event)"
                    :ref="item.Name"
                  />
                </v-flex>
              </template>
            </v-layout>
          </v-card-text>
          <v-card-actions
            v-if="!hideToolitem && ($vuetify.breakpoint.smAndDown || isDialog)"
          >
            <v-spacer></v-spacer>
            <permission-facatory
              v-for="btn in visibleToolItems.filter(evalVisible)"
              :key="btn.key || btn.name"
              :permission="btn.permission"
            >
              <v-btn
                @click="evalAction(btn)"
                small
                color="primary"
                :disabled="evalDisabled(btn)"
                v-bind="btn"
                block
                :text="false"
              >
                <v-icon v-if="btn.iconName">{{ btn.iconName }}</v-icon>
                {{ btn.title }}
              </v-btn>
            </permission-facatory>
            <v-spacer></v-spacer>
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
    hideToolitem: Boolean,
    title: String,
    id: String,
    model: Object,
    formErrorMessages: Object,
    options: Array,
    singleLine: Boolean,
    showMamageField: Boolean,
    canEdit: Boolean,
    hasManage: Boolean,
    formGroups: Array,
    toolItems: Array
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
          xs12: true
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
    },
    visibleToolItems() {
      if (this.hideToolitem) return [];
      return this.toolItems.filter(this.evalVisible);
    }
  },

  methods: {
    handleInput(item, v) {
      this.model[item.Name] = v;
      this.$emit("tooggle:changed");
    },
    evalVisible({ visible }) {
      if (!visible) {
        return true;
      }
      let val = true;
      if (typeof visible == "function") val = visible.call(this, this.model);
      if (typeof visible == "boolean") val = visible;
      if (typeof visible == "string") val = !!visible;

      return val;
    },
    evalDisabled({ disabled }) {
      let val = false;
      if (typeof disabled == "function") val = disabled.call(this, this.model);
      if (typeof disabled == "boolean") val = disabled;
      if (typeof disabled == "string") val = !!disabled;

      return val;
    },
    evalAction(item) {
      this.$emit(`toolItemClick`, item);
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
  height: calc(100vh - 135px);
  overflow: auto;
  overflow-x: hidden;
}

.tabPage {
  height: calc(100vh - 170px);
  overflow: auto;
  overflow-x: hidden;
}

.dialogPage {
  max-height: calc(100vh - 255px);
  overflow: auto;
  overflow-x: hidden;
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
  padding: 0px;
}

.form-content .v-toolbar__title {
  font-size: 15px;
}
</style>

