<template>
  <span>
    <v-btn v-bind="$attrs" @click="handleClick" :disabled="evalDisabled" :title="evalTitle">
      <slot></slot>
    </v-btn>
  </span>
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
    }
  },
  methods: {
    handleClick($event) {
      this.$emit("click", $event);
    }
  }
};
</script>
 
