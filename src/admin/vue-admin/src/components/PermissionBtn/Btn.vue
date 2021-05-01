<template>
  <component :is="tagName" @click="handleClick" v-bind="btnProps" v-if="hasPermission">
    <v-icon v-if="iconName" :color="color">{{ iconName }}</v-icon>
    <slot></slot>
  </component>
</template>
 <script>
export default {
  props: {
    permission: [Array, String],
    title: String,
    iconName: String,
    color: String,
    tagName: {
      type: String,
      default: "v-btn",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {};
  },
  computed: {
    hasPermission() {
      return (
        !this.permission ||
        this.$store.getters.existsPermission(this.permission)
      );
    },
    evalDisabled() {
      return this.disabled || !this.hasPermission;
    },
    evalTitle() {
      if (!this.hasPermission) return `${this.title}(权限不足)`;
      else return this.title;
    },
    btnProps() {
      return {
        ...this.$attrs,
        ...this.$props,
        disabled: this.evalDisabled,
        title: this.evalTitle,
      };
    },
  },
  methods: {
    handleClick($event) {
      this.$emit("click", $event);
    },
  },
};
</script>
 
