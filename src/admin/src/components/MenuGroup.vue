<template>
  <v-list-group :prepend-icon="icon" no-action="no-action">
    <v-list-tile slot="activator" ripple="ripple">
      <v-list-tile-content>
        <v-list-tile-title>{{ title }}</v-list-tile-title>
      </v-list-tile-content>
      <!-- <v-list-tile-action>
        <v-icon class="success--text">dashboard</v-icon>
      </v-list-tile-action> -->
    </v-list-tile>
    <template v-for="(subItem, i) in items">
      <VSelf v-if="subItem.items" :key="subItem.name" v-bind="subItem"/>
      <v-list-tile
        v-else
        :key="i"
        :to="subItem.path" 
        :disabled="subItem.disabled" 
        ripple="ripple"
      >
        <v-list-tile-content>
          <v-list-tile-title>
            <span>{{ subItem.title }}</span>
          </v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </template>
  </v-list-group>
</template>

<script>
export default {
  components: {
    VSelf: () => import('./MenuGroup.vue')
  },
  props: {
    icon: String,
    title: String,
    items: Array
  }
}
</script>

<style>
</style>
