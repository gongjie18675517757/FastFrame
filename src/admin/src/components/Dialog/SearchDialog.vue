<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center :class="{singleLine:singleLine}">
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{title}}</v-toolbar-title>
            <v-spacer></v-spacer>
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
                  <v-list-tile-content @click="singleLine=!singleLine">
                    <v-list-tile-title>{{'单行布局'}}</v-list-tile-title>
                  </v-list-tile-content>
                </v-list-tile>
              </v-list>
            </v-menu>
            <v-btn icon @click="cancel" title="关闭">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text>
              <vue-perfect-scrollbar class="dialog-page">
                <component :is="singleLine?'span':'v-layout'" wrap>
                  <Input
                    v-for="item in options"
                    v-bind="item"
                    :value="item.value"
                    :key="item.Name"
                    :singleLine="singleLine"
                    @input="item.value=$event"
                    canEdit
                  />
                </component>
              </vue-perfect-scrollbar>
            </v-card-text>
          </v-form>

          <v-card-actions>
            <v-btn flat @click="cancel">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="query">查询</v-btn>
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
  inject: ["reload"],
  components: { VuePerfectScrollbar, Input },
  props: {
    title: String,
    options: Array
  },
  data() {
    return {
      singleLine: false
    };
  },
  methods: {
    refresh() {
      for (const option of this.options) {
        option.value = null;
      }
      this.reload();
    },
    query() {
      this.$emit("success" /*this.form*/);
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
}
</style>
