<template>
  <span>
    <a-btn
      v-for="item in toolitems"
      :key="item.name"
      icon
      v-if="evalShow(item)"
      :title="item.title"
      :disabled="evalDisabled(item)"
      @click="evalAction(item)"
    >
      <v-icon>{{item.icon}}</v-icon>
    </a-btn>
  </span>
</template>

<script>
export default {
  props: {
    toolitems: {
      Type: Array,
      default: function() {
        return []
      }
    }
  }
}
</script>

<style>
</style>
