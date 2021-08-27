<template>
  <v-card class="notification">
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>
        <h4>未读通知</h4>
      </v-toolbar-title>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text class="pa-0">
      <v-list two-line class="pa-0">
        <template v-for="(item, index) in items">
          <v-divider :key="index"></v-divider>
          <v-list-item avatar :key="item.index" @click="handleClick">
            <v-list-item-avatar :color="item.color">
              <v-icon dark>{{ item.icon }}</v-icon>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-subtitle v-text="item.title"></v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action class="caption">{{
              item.timeLabel
            }}</v-list-item-action>
          </v-list-item>
        </template>
      </v-list>
      <v-divider></v-divider>
      <v-btn block text class="ma-0">All</v-btn>
      <v-divider></v-divider>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  data() {
    return {};
  },
  computed: {
    items() {
      return this.$store.state.notifys;
    }
  },
  methods: {
    handleClick: () => {
      // console.log(e)
    }
  }
};
</script>
 

