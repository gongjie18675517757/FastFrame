<template>
  <!-- <v-overlay :value="alerts.length>0" z-index="7"> -->
  <div class="alert-container">
    <transition-group name="list" tag="div" class="alert-item-container">
      <v-alert
        class="alert-item list-item"
        v-for="item in alerts"
        :key="item.id"
        :type="item.type"
        :color="item.color"
        border="left"
        elevation="2"
        colored-border
        dismissible
        @input="item.onClose(item.id)"
        transition="scale-transition"
      >
        {{ item.msg }}
      </v-alert>
    </transition-group>
  </div>
  <!-- </v-overlay> -->
</template>

<script>
export default {
  data() {
    return {
      colorDic: {
        warning: "orange",
        error: "red",
        success: "green",
        info: "blue"
      }
    };
  },
  computed: {
    alerts() {
      return this.$store.state.toasts;
    }
  },
  created() {}
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

 
 
