<template>
  <v-list-tile :to="path" ripple="ripple" :disabled="disabled" rel="noopener">
    <v-list-tile-action v-if="icon">
      <v-badge color="red" overlap v-if="count>0">
        <span slot="badge">{{count}}</span>
        <v-icon medium>{{ icon }}</v-icon>
      </v-badge>
      <v-icon v-else>{{ icon }}</v-icon>
    </v-list-tile-action>
    <v-list-tile-content>
      <v-list-tile-title>{{ title }}</v-list-tile-title>
    </v-list-tile-content>
  </v-list-tile>
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
 
