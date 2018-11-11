<template>
  <span>
    <v-btn v-bind="$attrs" @click="handleClick" outline color="indigo">
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
    console.log(this)
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
