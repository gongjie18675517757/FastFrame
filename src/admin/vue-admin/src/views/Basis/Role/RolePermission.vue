<template>
  <v-card>
    <v-toolbar flat dense>
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text>
      <v-tabs height="35px">
        <v-tab>模块视图</v-tab>
        <v-tab>菜单视图</v-tab>

        <v-tab-item>
          <ModuleViewVue
            :value="value"
            :disabled="disabled"
            @input="
              $emit('input', $event);
              $emit('change', $event);
            "
          />
        </v-tab-item>
        <v-tab-item>
          <MenuViewVue
            :value="value"
            :disabled="disabled"
            @input="
              $emit('input', $event);
              $emit('change', $event);
            "
          />
        </v-tab-item>
      </v-tabs>
    </v-card-text>
  </v-card>
</template>

<script>
import MenuViewVue from "./MenuView.vue";
import ModuleViewVue from "./ModuleView.vue";

export default {
  props: {
    title: String,
    value: Array,
    disabled: Boolean,
  },
  components: {
    MenuViewVue,
    ModuleViewVue,
  },
};
</script>

<style>
</style>