<template>
  <v-container grid-list-xl fluid app ref="container" v-resize="handleResize">
    <v-layout align-center justify-center :class="{ singleLine: singleLine }">
      <v-flex v-bind="flex" :style="{ padding: isTab ? '1px' : null }">
        <v-card flat tile>
          <v-toolbar flat dense height="30px" v-if="!isTab">
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
              <v-icon>close</v-icon>
            </v-btn>
            <v-menu offset-y v-if="$vuetify.breakpoint.smAndDown && visibleToolItems.length > 0">
              <template v-slot:activator="{ on }">
                <v-btn v-on="on" icon color="primary" small title="更多">
                  <v-icon>more_vert</v-icon>
                  <!-- 设置 -->
                </v-btn>
              </template>

              <v-list dense>
                <permission-facatory
                  v-for="item in [
                    ...($vuetify.breakpoint.smAndDown ? visibleToolItems : []),
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

                <!-- <v-list-item @click="$emit('toggle:singleLine')">
                  <v-list-item-action>
                    <v-checkbox :value="singleLine"></v-checkbox>
                  </v-list-item-action>
                  <v-list-item-content>
                    <v-list-item-title>{{ "单行布局" }}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item> -->
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
          </v-toolbar>
          <!-- <v-divider></v-divider> -->
          <v-card-text
            class="form-content"
            v-if="model"
            :class="[
              isDialog ? 'dialogPage' : isTab ? 'tabPage' : 'fullPage',
              'form-page',
            ]"
          >
            <v-layout wrap style="padding: 0px; margin: 0px">
              <template v-for="group in formGroups">
                <v-flex
                  :key="group.key.title"
                  xs12
                  :xl10="!isDialog && !singleLine && containerWidth > 1440"
                  :xl8="singleLine && containerWidth > 1440"
                  style="padding: 5px; margin: 0 auto"
                >
                  <v-card
                    v-if="group.values.length > 1"
                    tile
                    elevation="1"
                    :class="{ 'no-box-shadow': formGroups.length == 1 }"
                  >
                    <v-toolbar
                      v-if="formGroups.length > 1"
                      flat
                      color="transparent"
                      dense
                      height="30px"
                    >
                      <v-toolbar-title>{{ group.key.title }}:</v-toolbar-title>
                      <v-spacer></v-spacer>
                      <v-toolbar-items>
                        <v-btn
                          icon
                          @click="toggleFold(group.key.title)"
                          color="p"
                        >
                          <v-icon v-if="isFold(group.key.title)"
                            >expand_more</v-icon
                          >
                          <v-icon v-else>expand_less</v-icon>
                        </v-btn>
                      </v-toolbar-items>
                    </v-toolbar>
                    <!-- <v-divider></v-divider> -->
                    <v-card-text v-show="!isFold(group.key.title)">
                      <component :is="singleLine ? 'div' : 'v-layout'" wrap>
                        <template v-for="item in group.values">
                          <slot :name="item.Name">
                            <Input
                              :key="item.Name"
                              v-bind="item"
                              :model="model"
                              :canEdit="canEdit"
                              :disabled="!canEdit"
                              :singleLine="singleLine"
                              :errorMessages="formErrorMessages[item.Name]"
                              :value="model[item.Name]"
                              :flex="{ xs12: singleLine, sm6: !singleLine }"
                              :labelWidth="labelWidth"
                              @change="
                                $emit('changed', { item: item, value: $event })
                              "
                              @input="handleInput(item, $event)"
                              :ref="item.Name"
                            />
                          </slot>
                        </template>
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
                    :isFold="isFold(group.key.title)"
                    @change="$emit('changed', { item: item, value: $event })"
                    @input="handleInput(item, $event)"
                    :ref="item.Name"
                  >
                    <template #fold-button>
                      <v-toolbar-items>
                        <v-btn
                          icon
                          @click="toggleFold(group.key.title)"
                          color="p"
                        >
                          <v-icon v-if="isFold(group.key.title)"
                            >expand_more</v-icon
                          >
                          <v-icon v-else>expand_less</v-icon>
                        </v-btn>
                      </v-toolbar-items>
                    </template>
                  </component>
                </v-flex>
              </template>
            </v-layout>
          </v-card-text>
          <v-card-actions v-if="!hideToolitem">
            <v-spacer></v-spacer>
            <permission-facatory
              v-for="btn in visibleToolItems.filter(evalVisible)"
              :key="btn.key || btn.name"
              :permission="btn.permission"
            >
              <component
                :is="btn.component || 'v-btn'"
                @click="evalAction(btn)"
                v-bind="btn"
                :disabled="evalDisabled(btn)"
                :block="$vuetify.breakpoint.smAndDown"
                tile
              >
                <v-icon left v-if="btn.iconName">{{ btn.iconName }}</v-icon>
                {{ btn.title }}
              </component>
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
    VuePerfectScrollbar,
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
    toolItems: Array,
  },
  data() {
    return {
      folds: [],
      containerWidth: 0,
    };
  },
  computed: {
    flex() {
      if (!this.singleLine) {
        return {
          xs12: true,
        };
      } else {
        return {
          xs12: true,
        };
      }
    },
    formGroupExpandValue: {
      get() {
        return this.formGroups.map((r) => r.key.value);
      },
      set(val) {
        for (let index = 0; index < val.length; index++) {
          const value = val[index];
          this.formGroups[index].value = value;
        }
      },
    },
    visibleToolItems() {
      if (this.hideToolitem) return [];
      return this.toolItems.filter(this.evalVisible);
    },
    labelWidth() {
      // return this.formGroups.map((v) =>
      //   v.values.map((x) => (x.Description ? x.Description.length : 0))
      // );
      return this.containerWidth > 1220 ? "10em" : "10em";
    },
  },

  methods: {
    isFold(key) {
      return this.folds.includes(key);
    },
    toggleFold(key) {
      if (this.isFold(key)) {
        this.folds = this.folds.filter((v) => v != key);
      } else {
        this.folds.push(key);
      }
    },
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
    },
    handleResize() {
      this.containerWidth = this.$refs.container.clientWidth;
    },
  },
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
  height: calc(100vh - 167px);
  overflow: auto;
  overflow-x: hidden;
}

.tabPage {
  height: calc(100vh - 200px);
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

