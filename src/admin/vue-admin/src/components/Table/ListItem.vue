<template>
  <v-list-item
    :class="{selected:selected}"
    :value="item"
    @click="$emit('click')"
    @dblclick="handleDbClick"
  >
    <v-list-item-avatar>
      <v-img :src="icon || item.icon"></v-img>
    </v-list-item-avatar>
    <v-list-item-content>
      <v-list-item-title>{{item.Name}}</v-list-item-title>
      <v-list-item-subtitle>由 {{item.CreateName}} 于 {{item.CreateTime}} {{item.IsFolder?'创建':'上传'}}</v-list-item-subtitle>
    </v-list-item-content>

    <v-list-item-icon>
      <v-btn icon v-if="!item.IsFolder" title="下载" @click.stop="download" color="primary">
        <v-icon>mdi-download</v-icon>
      </v-btn>
      <v-btn icon title="重命名" v-if="reNameable" @click.stop="reName" color="primary">
        <v-icon>mdi-file-edit</v-icon>
      </v-btn>
      <v-btn icon title="删除" v-if="!readonly" @click.stop="remove" color="primary">
        <v-icon>mdi-delete</v-icon>
      </v-btn>
    </v-list-item-icon>
  </v-list-item>
</template>

<script>
import GridItem from "./GridItem.vue";
export default {
  extends: GridItem
};
</script>
 
