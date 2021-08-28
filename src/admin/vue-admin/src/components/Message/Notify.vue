<template>
  <!-- <v-overlay :value="alerts.length>0" z-index="7"> -->
  <div class="notify-container">
    <transition-group name="list" tag="div" class="notify-item-container">
      <v-card
        class="notify-item list-item"
        v-for="item in newNotifys"
        :key="item.Id"
        :style="{ 'z-index': item == curr ? '2' : null }"
        @click="currId = item.Id"
        :ripple="false"
        tile
      >
        <v-toolbar flat dense tile>
          <v-toolbar-title>{{ item.Title }}</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-btn icon @click.stop="handleCose(item)">
            <v-icon>close</v-icon>
          </v-btn>
        </v-toolbar>
        <v-card-text :ripple="false">
          {{ item.Content }}
        </v-card-text>
        <v-card-actions v-if="item.ToUrl">
          <v-spacer></v-spacer>
          <v-btn text color="primary accent-4" @click="hanldeClickMore(item)">
            查看详情
          </v-btn>
        </v-card-actions>
      </v-card>
    </transition-group>
  </div>
  <!-- </v-overlay> -->
</template>

<script>
export default {
  data() {
    return {
      currId: null
    };
  },
  computed: {
    newNotifys() {
      return this.$store.state.newNotifys;
    },
    curr() {
      return (
        this.newNotifys.find(v => v.Id == this.currId) ||
        (this.newNotifys.length > 0 ? this.newNotifys[0] : null)
      );
    }
  },
  methods: {
    hanldeClickMore(item) {
      this.$router.push(item.ToUrl);
      this.handleCose(item);
    },
    handleCose(item) {
      let index = this.newNotifys.findIndex(v => v == item);
      this.newNotifys.splice(index, 1);
    }
  }
};
</script>

<style lang="stylus">
.notify-container {
  position: fixed;
  bottom: 0;
  right: 0;
  width: 100%;
  padding: 10px;
  z-index: 9;

  .v-card--link:before {
    display: none;
  }

  // max-height: calc(100vh - 60px);
  // overflow: auto;
  .notify-item-container {
    display: flex;
    align-items: flex-end;
    justify-content: center;
    flex-direction: column;

    .notify-item {
      min-width: 350px;
      margin-top: -50px;
      cursor: default;
      
    }
  }

  .list-item {
    display: inline-block;
    margin-right: 10px;
  }

  .list-enter-active, .list-leave-active {
    transition: all 1s;
  }

  .list-enter, .list-leave-to {
    opacity: 0;
    transform: translateY(30px);
  }
}
</style>

 
 
