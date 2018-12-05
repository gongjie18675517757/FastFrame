<template>
  <v-layout>
    <div class="chat-history">
      <v-toolbar dense class="chat-history-toolbar">
        <v-text-field flat solo full-width clearable prepend-icon="search" label="Search"></v-text-field>
      </v-toolbar>
      <vue-perfect-scrollbar class="chat-history--scrollbar">
        <v-divider></v-divider>
        <v-list two-line class="chat-history--list">
          <v-subheader>History</v-subheader>
          <template v-for="(item, index) in chats">
            <v-divider :key="index"></v-divider>
            <v-list-tile
              class="chat-list"
              avatar
              :key="item.title + index"
              :to="chatRoute(item.uuid)"
            >
              <v-list-tile-avatar :color="randomAvatarColor(item)">
                <img src="x" v-if="item.users.length === 1">
                <span v-else class="white--text headline">{{ firstLetter(item.title)}}</span>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{computeTitle(item)}}</v-list-tile-title>
                <v-list-tile-sub-title>Some Latest message</v-list-tile-sub-title>
              </v-list-tile-content>
              <v-list-tile-action>
                <v-list-tile-action-text>{{ formatChatTime(item.created_at) }}</v-list-tile-action-text>
                <v-circle dot small :color="chatStatusColor(item)"></v-circle>
              </v-list-tile-action>
            </v-list-tile>
          </template>
        </v-list>
      </vue-perfect-scrollbar>
    </div>
  </v-layout>
</template>

<script>
import VCircle from "@/components/circle/VCircle.js";
import { randomElement } from "@/utils";
import VuePerfectScrollbar from "vue-perfect-scrollbar";
export default {
  components: {
    VuePerfectScrollbar,
    VCircle
  },

  data: () => ({
    chats: [
      {
        uuid: "a44f8ade-513c-46b5-bae4-0acf809860e6",
        title: "nisi",
        users: ["da95e977-cd54-4077-a767-1b7f33ef6919"],
        created_by: "60d07662-bfec-42c7-b044-c81bc4ff8c7a",
        created_at: "2018-04-10T15:02:15.476Z"
      },
      {
        uuid: "c86f170d-9a36-4f2c-bb76-2de65aa8c7bf",
        title: "odio",
        users: [
          "46d6f992-5729-4588-b7f8-ce74f21157ba",
          "7d910620-84e1-49fc-951e-d375587b8189"
        ],
        created_by: "eef93cb1-7766-4413-a5cf-ecbf71fa3674",
        created_at: "2018-04-11T04:02:56.728Z"
      },
      {
        uuid: "9c750cd1-a04d-4b9b-afe2-3e5f1b8d04fa",
        title: "delectus",
        users: ["60d07662-bfec-42c7-b044-c81bc4ff8c7a"],
        created_by: "bd30e201-cceb-410e-8497-a4072bc399f5",
        created_at: "2018-04-10T10:35:26.982Z"
      },
      {
        uuid: "0b29c8d1-6467-4680-9210-01d7669d47c1",
        title: "placeat",
        users: ["da95e977-cd54-4077-a767-1b7f33ef6919"],
        created_by: "6124d4e8-77ed-4b34-868d-d312bfab5de2",
        created_at: "2018-04-10T22:33:14.365Z"
      },
      {
        uuid: "ff04dee6-34f0-4ac9-b38b-463a2e0227e9",
        title: "minima",
        users: [
          "5c44b666-baca-4f18-a3cb-23068c6edc14",
          "14ddae1e-986d-42f4-8d17-46a02d469b2b"
        ],
        created_by: "ee272550-36e8-4fe2-889d-c1ee701c5863",
        created_at: "2018-04-10T07:56:08.876Z"
      },
      {
        uuid: "42e3d8f8-a097-4049-bd6e-53eab86f3722",
        title: "ducimus",
        users: [
          "46d6f992-5729-4588-b7f8-ce74f21157ba",
          "6124d4e8-77ed-4b34-868d-d312bfab5de2"
        ],
        created_by: "77f4b102-9df5-43ba-966a-6f816806c5e2",
        created_at: "2018-04-11T00:04:45.012Z"
      },
      {
        uuid: "14c43a19-3938-41ec-90ca-9f09d9390a6f",
        title: "et",
        users: [
          "65a6eb21-67b5-45c3-9af7-faca2d9b60d4",
          "3782c174-1f2c-4dc4-b75d-0bedf400e023"
        ],
        created_by: "afdb5033-5bcc-4cec-b932-353a83410b44",
        created_at: "2018-04-10T20:30:02.955Z"
      },
      {
        uuid: "b42daaa7-ef3c-4cbe-89cc-52476f169232",
        title: "qui",
        users: ["afdb5033-5bcc-4cec-b932-353a83410b44"],
        created_by: "36a1ead7-57a0-4275-8a21-956194ab7cdf",
        created_at: "2018-04-11T05:27:15.635Z"
      },
      {
        uuid: "a4fb2a31-7e6f-4103-b512-3a0e1856b42d",
        title: "totam",
        users: [
          "65a6eb21-67b5-45c3-9af7-faca2d9b60d4",
          "5c44b666-baca-4f18-a3cb-23068c6edc14",
          "7d910620-84e1-49fc-951e-d375587b8189"
        ],
        created_by: "60d07662-bfec-42c7-b044-c81bc4ff8c7a",
        created_at: "2018-04-10T14:47:13.370Z"
      },
      {
        uuid: "b1f03c8b-837f-4579-a18b-974d3ce93f3b",
        title: "placeat",
        users: [
          "6a03248b-1752-4332-a3a9-7108528cc9d3",
          "28d9f265-74d7-4f85-83d4-6a21fca57dcf",
          "65a6eb21-67b5-45c3-9af7-faca2d9b60d4"
        ],
        created_by: "a41c6c4a-9cb1-45d1-8c6f-091044ba51ff",
        created_at: "2018-04-11T01:23:23.603Z"
      }
    ]
  }),

  methods: {
    chatRoute(id) {
      return "/chat/messaging/" + id;
    },
    firstLetter(title) {
      return title.charAt(0);
    },
    formatChatTime(s) {
      return new Date(s).toLocaleDateString();
    },
    computeTitle(item) {
      let username = item.users.length === 1 ? "Lora" : "";
      return item.users.length === 1 ? username : item.title;
    },
    randomAvatarColor(item) {
      return item.users.length === 1
        ? ""
        : randomElement(["blue", "indigo", "success", "error", "pink"]);
    },

    chatStatusColor(item) {
      return randomElement(["blue", "indigo", "success", "error", "pink"]);
    }
  }
};
</script>

