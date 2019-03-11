<template>
  <div>
    <v-fade-transition mode="out-in">
      <router-view v-if="resufreshed"/>
    </v-fade-transition>
    <Alert/>
    <component v-for="dialog in dialogs" :key="dialog.key" :is="dialog.render"/>
  </div>
</template>

<script>
import Alert from "@/components/Alert.vue";

export default {
  components: {
    Alert
  },
  // provide() {
  //   return {
  //     reload: this.resufresh
  //   };
  // },
  data() {
    return {
      resufreshed: true
    };
  },
  computed: {
    dialogs() {
      return this.$store.state.dialogs;
    }
  },
  async created() {},
  methods: {
    resufresh() {
      this.resufreshed = false;
      this.$nextTick(function() {
        this.resufreshed = true;
      });
    }
  }
};
</script>

<style>
.form {
  padding: 5px;
}

.v-card__title {
  padding: 2px;
}

/* .input-container,.input-container .flex {
  padding: 0px;
} */

.input-container .v-input {
  padding: 0px;
}
.v-input {
  font-size: 14px;
}
.v-text-field__details {
  display: none;
}
</style>