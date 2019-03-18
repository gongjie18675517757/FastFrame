<template>
  <v-list-tile
    avatar
    @click.stop="$emit('click')"
    @dblclick.stop="handleDbClick"
    :class="{selected:selected}"
  >
    <v-list-tile-avatar>
      <v-icon v-if="item.IsFolder">folder</v-icon>
      <v-icon v-else-if="isImage">image</v-icon>
      <v-icon v-else>insert_drive_file</v-icon>
    </v-list-tile-avatar>
    <v-list-tile-content>
      <div class="container pl-0">
        <div class="layout row">
          <div class="flex">{{item.Name}}</div>
          <v-spacer></v-spacer>
          <div>{{item.Create_User.Name}}</div>
          <v-spacer></v-spacer>
          <div>{{item.CreateTime}}</div>
        </div>
      </div>
    </v-list-tile-content>
  </v-list-tile>
</template>

<script>
import GridItem from "./GridItem.vue";
export default {
  extends: GridItem
};
</script>
 
