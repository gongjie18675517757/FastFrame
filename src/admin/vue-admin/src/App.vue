<template>
  <v-app>
    <v-fade-transition mode="out-in">
      <router-view v-if="resufreshed" />
    </v-fade-transition>
    <Alert />
    <v-dialog
      v-for="(dialog,i) in dialogs"
      :key="i"
      :value="true"
      :width="dialog.pars.width"
      colored-border 
    >
      <component
        :is="dialog.component"
        v-bind="dialog.pars"
        @success="handleDialogSuccess(dialog,$event)"
        @close="handleDialogClose(dialog,$event)"
      />
    </v-dialog>
  </v-app>
</template>

<script>
import Alert from "@/components/Alert.vue";

export default {
  components: {
    Alert
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
      this.$store.state.singlePageMode = val;
      this.$store.state.isXs = val;
    }
  },
  beforeCreate() {
    let isXs = this.$vuetify.breakpoint.xs;
    this.$store.state.singlePageMode = isXs;
    this.$store.state.isXs = isXs;
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
 

<style >
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

.v-data-table__wrapper {
  min-height: 100px;
}
</style>