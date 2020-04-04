<template>
  <v-list-item :to="path" ripple="ripple" :disabled="disabled" rel="noopener">
    <v-list-item-action v-if="icon">
      <v-badge color="red" overlap v-if="count>0">
        <span slot="badge">{{count}}</span>
        <v-icon medium>{{ icon }}</v-icon>
      </v-badge>
      <v-icon v-else>{{ icon }}</v-icon>
    </v-list-item-action>
    <v-list-item-content>
      <v-list-item-title>{{ title }}</v-list-item-title>
    </v-list-item-content>
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
}
</script>
 
