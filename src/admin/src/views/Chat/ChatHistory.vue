<template>
  <v-card>
    <v-card-title>
      <v-text-field
        append-icon="search"
        label="搜索联系人和群组"
        single-line
        hide-details
        v-model="search"
        @change="searchList"
        class="search-input"
      ></v-text-field>
    </v-card-title>
    <v-card-title>
      <vue-perfect-scrollbar class="drawer---scroll--formcontent">
        <v-list two-line class="chat-history--list">
          <template v-for="item in Friends">
            <v-divider :key="item.Id"></v-divider>
            <v-list-tile class="chat-list" avatar :key="item.Id" @click="handleClick">
              <v-list-tile-avatar :color="randomAvatarColor(item)">
                <img :src="`/api/resource/get/${item.HeadIcon_Id}`" v-if="item.HeadIcon_Id">
                <span v-else class="white--text headline">{{ firstLetter(item)}}</span>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title>{{computeTitle(item)}}</v-list-tile-title>
                <v-list-tile-sub-title>Some Latest message</v-list-tile-sub-title>
              </v-list-tile-content>
              <v-list-tile-action>
                <v-list-tile-action-text>{{ getLastTime(item) }}</v-list-tile-action-text>
              </v-list-tile-action>
            </v-list-tile>
          </template>
        </v-list>
      </vue-perfect-scrollbar>
    </v-card-title>
  </v-card>
</template>

<script>
import { randomElement } from "@/utils";
import VuePerfectScrollbar from "vue-perfect-scrollbar";
export default {
  components: {
    VuePerfectScrollbar
  },
  data() {
    return {
      search: "",
      Friends: []
    };
  },
  async created() {
    this.Friends = await this.$http.get("/api/Friend");
  },
  methods: {
    searchList() {},
    firstLetter({ Name }) {
      return Name.charAt(0);
    },
    handleClick() {},
    computeTitle(item) {
      return item.Name;
    },
    getLastTime(item) {
      let dt = new Date();
      return `${dt.getFullYear()}-${dt.getMonth()}`;
    },
    randomAvatarColor() {
      return randomElement(["blue", "indigo", "success", "error", "pink"]);
    }
  }
};
</script>

<style scoped lang="stylus">
.search-input {
  padding-top: 2px;
}

.drawer---scroll--formcontent {
  height: calc(100vh - 136px);
  overflow: auto;
}
</style>