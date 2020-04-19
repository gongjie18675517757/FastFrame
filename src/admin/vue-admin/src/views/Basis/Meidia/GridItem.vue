<template>
  <v-card @dblclick="$emit('dblclick')" :dark="selected">
    <v-img
      :src="item.icon"
      :lazy-src="item.icon"
      class="white--text align-end"
      gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
      height="150px"
      max-width="100%"
      @click="$emit('click')"
      @dblclick="handleDbClick"
      :style="{cursor:item.IsFolder || 1 ?'pointer':''}"
    >
      <v-card-title v-text="item.Name"></v-card-title>
    </v-img>
    <v-divider></v-divider>
    <v-card-actions class="grid-item">
      <slot name="action" v-bind="item">
        <v-spacer></v-spacer>
        <v-btn icon v-if="!item.IsFolder" title="下载" @click="download" color="primary">
          <v-icon>mdi-download</v-icon>
        </v-btn>

        <v-btn icon title="重命名" @click="reName" color="primary">
          <v-icon>mdi-file-edit</v-icon>
        </v-btn>

        <v-btn icon title="删除" @click="remove" color="primary">
          <v-icon>mdi-delete</v-icon>
        </v-btn>
      </slot>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    item: {
      type: Object,
      default: function() {
        return {};
      }
    },
    selected: Boolean
  },
  computed: {},
  methods: {
    remove() {
      this.$emit("remove", this.item);
    },
    reName() {
      this.$emit("reName", this.item);
    },
    download() {
      this.$emit("download", this.item);
    },
    handleDbClick() {
      this.$emit("preview", this.item);
    }
  }
};
</script>

 
