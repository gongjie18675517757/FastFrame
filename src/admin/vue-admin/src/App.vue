<template>
  <div>
    <v-fade-transition mode="out-in">
      <router-view v-if="resufreshed" />
    </v-fade-transition>
    <Alert />
    <v-dialog v-for="(dialog,i) in dialogs" :key="i" :value="true" style="box-shadow:none;">
      <component
        :is="dialog.component"
        v-bind="dialog.pars"
        @success="handleDialogSuccess(dialog,$event)"
        @close="handleDialogClose(dialog,$event)"
      />
    </v-dialog>
  </div>
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
    }
  },
  created() {
    if (!this.$store.state.currUser || !this.$store.state.currUser.Id) {
      this.$http.get("/api/account/GetCurrent").then(user => {
        this.$store.dispatch({
          type: "login",
          user
        });
      });
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

.input-container {
  padding: 20px;
}

.input-container .v-input {
  padding: 0px;
}
.v-input {
  font-size: 14px;
}

.much-input .v-input__control {
  height: 30px;
}

.v-text-field {
  margin: 0px;
}
.input-container,
.much-input {
  min-height: 35px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.v-text-field__details {
  align-items: center;
}
.v-input input {
  font-size: 14px;
  line-height: 14px;
}
.v-text-field > .v-input__control > .v-input__slot:before,
.v-text-field > .v-input__control > .v-input__slot:after {
  display: none;
}

.v-text-field__slot input,
.v-select__slot {
  border-bottom: 1px solid #9c7a7a;
}
.v-select__selections {
  font-size: 14px;
  line-height: 14px;
}
</style>