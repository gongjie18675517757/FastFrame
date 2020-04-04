<template>
  <span>
    <a-btn
      v-for="item in toolitems.filter(r=>evalShow(r))"
      :key="item.name"
      icon
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
        return [];
      }
    }
  }
};
</script>

 
