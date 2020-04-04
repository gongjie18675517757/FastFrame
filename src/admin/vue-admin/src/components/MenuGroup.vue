<template>
  <v-list-group :prepend-icon="icon" no-action="no-action">
    <v-list-item slot="activator" ripple="ripple">
      <v-list-item-content>
        <v-list-item-title>{{ title }}</v-list-item-title>
      </v-list-item-content> 
    </v-list-item>
    <template v-for="(subItem, i) in items">
      <VSelf v-if="subItem.items" :key="subItem.name" v-bind="subItem"/>
      <v-list-item
        v-else
        :key="i"
        :to="subItem.path" 
        :disabled="subItem.disabled" 
        ripple="ripple"
      >
        <v-list-item-content>
          <v-list-item-title>
            <span>{{ subItem.title }}</span>
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
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

 
