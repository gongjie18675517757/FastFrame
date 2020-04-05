<template>
  <v-card>
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>
        <h4>Message</h4>
      </v-toolbar-title>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text class="pa-0">
      <v-list two-line class="pa-0">
        <template v-for="(item, index) in items">
          <v-subheader v-if="item.header" :key="item.header">{{ item.header }}</v-subheader>
          <v-divider v-else-if="item.divider" :key="index"></v-divider>
          <v-list-item avatar v-else :key="item.title" @click="handleClick">
            <v-list-item-avatar>
              <img :src="item.avatar">
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title v-html="item.title"></v-list-item-title>
              <v-list-item-subtitle v-html="item.subtitle"></v-list-item-subtitle>
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
  data: () => ({
    items: [
      {
        avatar: 'https://randomuser.me/api/portraits/men/1.jpg',
        title: 'Brunch this weekend?',
        subtitle:
          "<span class='text--primary'>Ali Connors</span> &mdash; I'll be in your neighborhood ?"
      },
      { divider: true, inset: true },
      {
        avatar: 'https://randomuser.me/api/portraits/men/2.jpg',
        title: 'Summer BBQ <span class="grey--text text--lighten-1">4</span>',
        subtitle:
          "<span class='text--primary'>to Alex, Scott, Jennifer</span> &mdash; Wish I could come, but I'm out of town this weekend."
      },
      { divider: true, inset: true },
      {
        avatar: 'https://randomuser.me/api/portraits/men/3.jpg',
        title: 'Oui oui',
        subtitle:
          "<span class='text--primary'>Sandra Adams</span> &mdash; Do you have Paris recommendations? Have you ever been?"
      },
      { divider: true, inset: true },
      {
        avatar: 'https://randomuser.me/api/portraits/men/1.jpg',
        title: 'Dash',
        subtitle:
          "<span class='text--primary'>Sandra Adams</span> &mdash; Do you have Paris recommendations? Have you ever been?"
      }
    ]
  }),
  methods: {
    handleClick(){}
  }
}
</script>
