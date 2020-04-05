<template>
  <v-btn @click="handleClick" v-bind="btnProps">
    <slot />
  </v-btn>
</template>
 <script>
export default {
  props: {
    name: String,
    moduleName: String,
    title: String,
    disabled: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {};
  },
  computed: {
    evalDisabled() {
      return this.disabled || !this.permission;
    },
    evalTitle() {
      if (!this.permission) return `${this.title}(权限不足)`;
      else return this.title;
    },
    permission() {
      return this.$store.getters.existsPermission(this.moduleName, this.name);
    },
    btnProps() {
      return {
        ...this.$attrs,
        disabled: this.evalDisabled,
        title: this.evalTitle
      };
    }
  },
  methods: {
    handleClick($event) {
      this.$emit("click", $event);
    }
  }
};
</script>
 
