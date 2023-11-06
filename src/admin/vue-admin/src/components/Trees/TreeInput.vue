<template>
  <div class="v-text-field">
    <v-input dense :disabled="disabled" :error-messages="errorMessages">
      <template #default>
        <v-menu
          :close-on-content-click="false"
          :disabled="disabled"
          v-model="menuVisible"
          offset-y
          max-width="300px"
        >
          <template v-slot:activator="{ on }">
            <div
              v-on="on"
              style="align-items: center; width: 100%; display: flex"
            >
              <input
                type="text"
                :value="value"
                :placeholder="description"
                @blur="handleBlur"
                @input="handleSearchChange"
              />
              <v-icon small>search</v-icon>
            </div>
          </template>
          <v-card v-if="menuVisible">
            <v-toolbar dense flat>
              <v-toolbar-title> 请选择: </v-toolbar-title>
            </v-toolbar>
            <v-card-text style="padding: 0px">
              <BaseTree
                :requestUrl="requestUrl"
                :init_super_key="init_super_key"
                @input="handleInput"
                height="300px"
              />
            </v-card-text>
          </v-card>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" text :disabled="!temp_value" @click="success"
              >确认
            </v-btn>
          </v-card-actions>
        </v-menu>
      </template>
      <template #prepend>
        <slot name="prepend"></slot>
      </template>
    </v-input>
  </div>
</template>

<script>
import BaseTree from "./BaseTree.vue";
export default {
  name: "v-tree-input",
  components: {
    BaseTree,
  },
  props: {
    requestUrl: String,
    init_super_key: String,
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean,
  },
  data() {
    return {
      x: 0,
      y: 0,
      menuVisible: false,
      temp_value: null,
      kw: null,
    };
  },
  computed: {},
  methods: {
    handleSearchChange(kw) {
      this.kw = kw;
    },
    handleBlur() {},
    handleInput(v) {
      this.temp_value = v;
    },
    success() {
      if (this.temp_value) this.$emit("input", this.temp_value);
      this.menuVisible = false;
    },
  },
};
</script>

 
