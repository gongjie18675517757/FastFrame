<template>
  <v-card class="notification">
    <v-toolbar card dense color="transparent">
      <v-toolbar-title>
        <h4>未读通知</h4>
      </v-toolbar-title>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text class="pa-0">
      <v-list two-line class="pa-0">
        <template v-for="(item, index) in items">
          <v-divider :key="index"></v-divider>
          <v-list-tile avatar :key="item.index" @click="handleClick">
            <v-list-tile-avatar :color="item.color">
              <v-icon dark>{{item.icon}}</v-icon>
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-sub-title v-text="item.title"></v-list-tile-sub-title>
            </v-list-tile-content>
            <v-list-tile-action class="caption">{{item.timeLabel}}</v-list-tile-action>
          </v-list-tile>
        </template>
      </v-list>
      <v-divider></v-divider>
      <v-btn block flat class="ma-0">All</v-btn>
      <v-divider></v-divider>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  data() {
    return {}
  },
  computed: {
    items() {
      return this.$store.state.notifys
    }
  },
  methods: {
    handleClick: e => {
      console.log(e)
    }
  }
}
</script>
 

