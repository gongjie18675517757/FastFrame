<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{singleLine:singleLine}">
      <v-flex xs12 md8 xl6>
        <v-card>
          <v-toolbar flat dense color="transparent" height="30px">
            <v-toolbar-title>{{title}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="refresh" title="刷新">
              <v-icon>refresh</v-icon>
            </v-btn>
            <v-menu offset-y>
              <template v-slot:activator="{ on }">
                <v-btn v-on="on" title="添加组" text small>
                  <v-icon>add</v-icon>添加查询组
                </v-btn>
              </template>
              <v-list dense>
                <v-list-item @click="addQueryOption('and')">
                  <v-list-item-content>
                    <v-list-item-title>且</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
                <v-list-item @click="addQueryOption('or')">
                  <v-list-item-content>
                    <v-list-item-title>或</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-menu>
            <v-menu offset-y>
              <template v-slot:activator="{ on }">
                <v-btn icon v-on="on" title="设置">
                  <v-icon>more_vert</v-icon>
                </v-btn>
              </template>
              <v-list dense>
                <v-list-item>
                  <v-list-item-action>
                    <v-checkbox v-model="singleLine"></v-checkbox>
                  </v-list-item-action>
                  <v-list-item-content @click="singleLine=!singleLine">
                    <v-list-item-title>{{'单行布局'}}</v-list-item-title>
                  </v-list-item-content>
                </v-list-item>
              </v-list>
            </v-menu>
            <v-btn icon @click="cancel" title="关闭">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>

          <v-card-text>
            <div class="dialog-page">
              <v-expansion-panels multiple :value="groupValues">
                <v-expansion-panel v-for="(v,i) in options" :key="i">
                  <v-expansion-panel-header>
                    查询组{{i+1}}({{conds[v.Key]}})
                    <template slot="actions">
                      <v-btn icon v-if="options.length>1" @click="removeGroup(i)">
                        <v-icon>clear</v-icon>
                      </v-btn>
                    </template>
                  </v-expansion-panel-header>
                  <v-expansion-panel-content>
                    <component :is="singleLine?'span':'v-layout'" wrap>
                      <Input
                        v-for="item in v.Value"
                        v-bind="item"
                        :value="item.value"
                        :key="item.Description"
                        :singleLine="singleLine"
                        @input="handleInput(item,$event)"
                        canEdit
                      />
                    </component>
                  </v-expansion-panel-content>
                </v-expansion-panel>
              </v-expansion-panels>
            </div>
          </v-card-text>

          <v-card-actions>
            <v-btn text @click="cancel">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="query">查询</v-btn>
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
  components: { VuePerfectScrollbar, Input },
  props: {
    title: String,
    options: Array,
    makeOptionsFunc: Function
  },
  data() {
    return {
      singleLine: false,
      conds: {
        and: "且",
        or: "或"
      }
    };
  },
  computed: {
    groupValues() {
      return this.options.map((_, i) => i);
    }
  },
  methods: {
    removeGroup(index) {
      this.options.splice(index, 1);
    },
    addQueryOption(key) {
      this.options.push({
        Key: key,
        Value: this.makeOptionsFunc()
      });
    },
    handleInput(item, val) {
      item.value = val;
    },

    refresh() {
      this.options.splice(0, this.options.length);
      this.options.push({
        Key: "and",
        Value: this.makeOptionsFunc()
      });
    },
    query() {
      this.$emit("success", this.options);
    },
    cancel() {
      this.$emit("close"); 
    }
  }
};
</script>

<style scoped>
.dialog-page {
  height: calc(100vh - 265px);
  overflow: auto;
  padding: 12px;
}
</style>
