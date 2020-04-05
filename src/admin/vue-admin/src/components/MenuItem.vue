<template>
  <v-list-item :to="path" link>
    <v-list-item-icon v-if="icon">
      <v-icon>{{icon}}</v-icon>
    </v-list-item-icon>
    <v-list-item-title>{{title}}</v-list-item-title>
  </v-list-item>
</template>

<script>
export default {
  props: {
    path: String,
    icon: String,
    title: String,
    disabled: Boolean,
    count: {
      type: Number,
      default: 0
    }
  }
};
</script>
 
