<template>
  <span>
    <span v-if="disabled && !isXs">{{value ? "是" : "否"}}</span>
    <v-checkbox v-else v-bind="childProps" dense v-on="listeners" :value="value"></v-checkbox>
  </span>
</template>

<script>
export default {
  // functional:true,
  props: {
    value: Boolean,
    disabled: Boolean,
    readonly: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean
  },
  computed: {
    childProps() {
      return {
        ...this.$attrs,
        value: this.value,
        readonly: this.readonly || this.disabled,
        label: this.label,
        placeholder: this.description,
        errorMessages: this.errorMessages
      };
    },
    listeners() {
      return {
        ...this.$listeners,
        change: val => {
          this.$emit("change", val || false);
          this.$emit("input", val || false);
        }
      };
    }
  }
};
</script>