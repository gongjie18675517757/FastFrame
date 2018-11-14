<template>
  <span>
    <v-btn v-bind="$attrs" @click="handleClick">
      <slot></slot>
    </v-btn>
  </span>
</template>

<script>
export default {
  props: {
    authority: String
  },
  created() {    
  },
  methods: {
    handleClick($event) {
      this.$emit('click',$event)
    }
  }
}
</script>

<style>
</style>
