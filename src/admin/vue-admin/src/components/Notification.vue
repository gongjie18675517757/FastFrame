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
          <v-list-item :key="item.Id" @click="handleClick(item)">
            <v-list-item-content>
              <v-list-item-title>{{ item.Title }}</v-list-item-title>
              <v-list-item-subtitle>{{ item.Content }}</v-list-item-subtitle>
            </v-list-item-content>
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
    handleClick(item) {
      if(item.ToUrl){
        this.$router.push(item.ToUrl)
      }
    }
  }
};
</script>
 

