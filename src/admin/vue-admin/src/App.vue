<template>
  <v-app>
    <v-fade-transition mode="out-in">
      <router-view v-if="resufreshed" />
    </v-fade-transition>
    <ToastVue />
    <NotifyVue />
    <v-dialog
      v-for="(dialog, i) in dialogs"
      :key="i"
      :value="true"
      :width="dialog.pars.width"
      colored-border
    >
      <component
        :is="dialog.component"
        v-bind="dialog.pars"
        @success="handleDialogSuccess(dialog, $event)"
        @close="handleDialogClose(dialog, $event)"
      />
    </v-dialog>
  </v-app>
</template>

<script>
import ToastVue from "./components/Message/Toast.vue";
import NotifyVue from "./components/Message/Notify.vue";

export default {
  components: {
    ToastVue,
    NotifyVue
  },
  data() {
    return {
      resufreshed: true
    };
  },
  computed: {
    dialogs() {
      return this.$store.state.dialogs;
    },
    isXs() {
      return this.$vuetify.breakpoint.xs;
    }
  },
  watch: {
    isXs(val) {
      this.$store.state.isXs = val;

      if (val && this.$store.state.singlePageMode) {
        this.$store.state.singlePageMode = true;
      }
    }
  },
  beforeCreate() {
    let isXs = this.$vuetify.breakpoint.xs;
    this.$store.state.isXs = isXs;
    if (isXs && this.$store.state.singlePageMode) {
      this.$store.state.singlePageMode = true;
    }
  },
  methods: {
    resufresh() {
      this.resufreshed = false;
      this.$nextTick(function() {
        this.resufreshed = true;
      });
    },
    handleDialogSuccess(dialog, ...arges) {
      dialog.resolve(...arges);
      let index = this.dialogs.findIndex(v => v == dialog);
      this.dialogs.splice(index, 1);
    },
    handleDialogClose(dialog, ...arges) {
      dialog.reject(...arges);
      let index = this.dialogs.findIndex(v => v == dialog);
      this.dialogs.splice(index, 1);
    }
  }
};
</script>
 

<style lang="stylus">
html {
  overflow-y: hidden;
}

 

.form-page .v-expansion-panel__container--active .v-expansion-panel__header {
  padding: 0px;
}

.form-page-group-header {
  font-weight: bold;
  font-size: 20px;
}

.v-dialog {
  box-shadow: none !important;
  -webkit-box-shadow: none;
  overflow: hidden;
}

.form {
  padding: 5px;
}

.v-card__title {
  padding: 2px;
}

.input-container .v-input {
  font-size: 13px;
}

.input-container .theme--light.v-input.v-input--is-readonly {
  color: #8e5656;
}

.v-input__prepend-outer {
  height: 100%;
}

.v-data-table__wrapper {
  min-height: 100px;
}

.full-page {
  height: calc(100vh - 140px);
  overflow: auto;
}

.tab-page {
  height: calc(100vh - 200px);
  overflow: auto;
}

.dialog-page {
  height: calc(100vh - 255px);
  overflow: auto;
}
</style>