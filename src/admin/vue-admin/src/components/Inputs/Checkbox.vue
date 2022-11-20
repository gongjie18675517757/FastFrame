<template>
  <v-input
    dense
    class="v-input__slot_checkbox_container v-text-field"
    style="margin-top: 0px"
    :disabled="disabled"
  >
    <template #default>
      <slot>
        <v-radio-group
          :value="value"
          @change="change"
          :disabled="disabled"
          hide-details
          dense
          row
        >
          <v-radio dense label="是" :value="true"></v-radio>
          <v-radio dense label="否" :value="false"></v-radio>
          <v-radio dense label="待定" :value="null"></v-radio>
        </v-radio-group>
      </slot>
    </template>
    <template #prepend>
      <slot name="prepend"></slot>
    </template>
  </v-input>
</template>
 
<script>
export default {
  // functional:true,
  props: {
    value: Boolean,

    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean,
    multiple: Boolean,
    values: Array,
  },
  methods: {
    change(val) { 
      this.$emit("change", val);
      this.$emit("input", val);
    },
  },
};
</script>
 