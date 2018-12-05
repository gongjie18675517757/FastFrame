<template>
  <v-container class="fill-height pa-0 ma-0 messaging fluid" id="messaging">
    <chat-menu :items="menus" class="chat-drawer--menu"></chat-menu>
    <ChatMessaging/>
  </v-container>
</template>

<script>
import ChatMenu from "./ChatMenu";
import ChatMessaging from "./ChatMessaging";

import VuePerfectScrollbar from "vue-perfect-scrollbar";
export default {
  components: {
    VuePerfectScrollbar,
    ChatMenu,
    ChatMessaging
  },
  data: () => ({
    menus: [
      {
        text: "Chat",
        icon: "chat",
        to: { path: "/chat/messaging" }
      },
      {
        text: "Contacts",
        icon: "contacts",
        to: { path: "/chat/contact" }
      },
      {
        text: "File",
        icon: "insert_drive_file",
        to: { path: "/chat/media" }
      },
      {
        text: "Settings",
        icon: "settings",
        to: { path: "/chat/settings" }
      }
    ]
  }),
  computed: {
    hideBottomNav() {
      return (
        this.$route.params.uuid !== undefined &&
        this.$route.name === "ChatMessaging"
      );
    }
  },
  methods: {
    handleClick() {
      this.$router.go(-1);
    }
  }
};
</script>

<style scoped>
.chat-drawer--menu {
  width: 68px;
  float: left;
}
</style>
