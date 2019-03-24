<template>
  <span>
    <v-btn v-bind="$attrs" @click="handleClick" :disabled="evalDisabled" :title="evalTitle">
      <slot></slot>
    </v-btn>
  </span>
</template>

<script>
import { existBtn } from '@/permission'
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
    return {
      attrs: {},
      permission: true
    }
  },
  computed: {
    evalDisabled() {
      return this.disabled || !this.permission
    },
    evalTitle() {
      if (!this.permission) return `${this.title}(权限不足)`
      else return this.title
    }
  },
  async created() {
    this.attrs = this.$attrs
    this.permission = await existBtn(this.moduleName, this.name)
  },
  methods: {
    handleClick($event) {
      this.$emit('click', $event)
    }
  }
}
</script>
 
