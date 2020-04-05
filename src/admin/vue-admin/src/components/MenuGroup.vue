<template>
  <v-list-group :prepend-icon="icon"  no-action :sub-group="level>1">
     
    <template v-slot:activator>
      <v-list-item-content>
        <v-list-item-title>{{ title }}</v-list-item-title>
      </v-list-item-content>
    </template>

    <template v-for="(subItem, i) in items">
      <VSelf
        v-if="subItem.items && subItem.items.length>0"
        :key="subItem.name"
        v-bind="subItem"
        :level="level+1"
      />
      <v-list-item v-else :key="i" :to="subItem.path" :disabled="subItem.disabled" ripple="ripple">
        <v-list-item-title v-text="subItem.title"></v-list-item-title>
        <v-list-item-icon v-if="subItem.icon">
          <v-icon v-text="subItem.icon"></v-icon>
        </v-list-item-icon>
      </v-list-item>
    </template>
  </v-list-group>
</template>

<script>
export default {
  components: {
    VSelf: () => import("./MenuGroup.vue")
  },
  props: {
    icon: String,
    title: String,
    items: Array,
    level: Number
  }
};
</script>

 
