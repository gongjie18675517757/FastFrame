<template>
  <v-list-tile
    avatar
    @click.stop="$emit('click')"
    @dblclick="$emit('dblclick')"
    :class="{selected:selected}"
  >
    <v-list-tile-avatar @dblclick="handleDbClick">
      <v-icon v-if="item.IsFolder">folder</v-icon>
      <v-icon v-else-if="isImage">image</v-icon>
      <v-icon v-else>insert_drive_file</v-icon>
    </v-list-tile-avatar>
    <v-list-tile-content @dblclick="handleDbClick">
      <div class="container pl-0">
        <div class="layout row">
          <div class="flex">{{item.Name}}</div>
          <v-spacer></v-spacer>
          <div>{{item.Create_User.Name}}</div> 
          上传于.
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
 
