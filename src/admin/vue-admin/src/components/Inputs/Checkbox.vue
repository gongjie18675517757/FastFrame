<template>
  <v-input
    dense
    class="v-input__slot_checkbox_container v-input-no-border"
    style="margin-top: 0px"
  >
    <template #default>
      <slot>
        <v-checkbox
          :input-value="value"
          @change="change"
          :disabled="readonly"
          hide-details
          style="margin-top: 0px"
          dense
        ></v-checkbox>
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

    readonly: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean,
    multiple: Boolean,
    values: Array,
  },
  methods: {
    change(val) {
      val = val || false;
      this.$emit("change", val);
      this.$emit("input", val);
    },
  },
};
</script>
 