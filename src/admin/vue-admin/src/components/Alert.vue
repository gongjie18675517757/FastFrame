<template>
  <div class="alert-container">
    <div class="alert-item-container">
      <v-alert
        class="alert-item"
        v-for="(item, index) in alerts"
        :key="item.key"
        :type="item.type"
        :color="item.color"
        border="left"
        elevation="2"
        colored-border
        dismissible
        @input="remove(index)"
        transition="scale-transition"
      >
        {{ item.msg }}
      </v-alert>
    </div>
  </div>
</template>

<script>
function S4() {
  return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function guid() {
  return (
    S4() +
    S4() +
    "-" +
    S4() +
    "-" +
    S4() +
    "-" +
    S4() +
    "-" +
    S4() +
    S4() +
    S4()
  );
}
export default {
  data() {
    return {
      alerts: [],
    };
  },
  created() {
    let self = this;
    let colorDic = {
      warning: "orange",
      error: "red",
      success: "green",
      info: "blue",
    };
    this.$eventBus.$on(
      "alert",
      function ({ type = "success", msg = "", timeout = 5000, color }) {
        color = color || colorDic[type];
        let item = {
          type,
          color,
          msg,
          timeout,
          key: guid(),
        };

        self.alerts.push(item);
        setTimeout(
          function () {
            let key = arguments[0];
            let index = self.alerts.findIndex((item) => item.key == key);
            if (index > -1) self.alerts.splice(index, 1);
          },
          timeout + 100,
          item.key
        );
      }
    );
  },
  methods: {
    remove(index) {
      this.alerts.splice(index, 1);
    },
  },
};
</script>

<style lang="stylus">
.alert-container {
  position: fixed;
  /* top: 10px; */
  /* left: 50%; */
  width: 100%;
  padding: 10px;
  z-index: 99999999;

  .alert-item-container {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;

    .alert-item {
      min-width: 500px;
    }
  }
}
</style>

 
 
