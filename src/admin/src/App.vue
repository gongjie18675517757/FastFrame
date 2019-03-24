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
<style lang="stylus">
/* Theme */
.fixed-header {
  & {
    display: flex;
    flex-direction: column;
    height: 100%;
  }

  table {
    table-layout: fixed;
  }

  th {
    position: sticky;
    top: 0;
    z-index: 5;

    &:after {
      content: '';
      position: absolute;
      left: 0;
      bottom: 0;
      width: 100%;
    }
  }

  tr.v-datatable__progress {
    th {
      // top: 56px
      height: 1px;
    }
  }

  .v-table__overflow {
    flex-grow: 1;
    flex-shrink: 1;
    overflow-x: auto;
    overflow-y: auto;
    // overflow: auto
    // height: 100%
  }

  .v-datatable.v-table {
    width: auto;
    min-width: 100%;
    flex-grow: 0;
    flex-shrink: 1;

    .v-datatable__actions {
      flex-wrap: nowrap;

      .v-datatable__actions__pagination {
        white-space: nowrap;
      }
    }
  }
}
</style>

<style >
.form-page .v-expansion-panel__container--active .v-expansion-panel__header {
  padding: 0px;
}
.form-page-group-header{
  font-weight: bold;
  font-size: 20px;
}
.v-dialog {
  box-shadow: none;
  -webkit-box-shadow: none;
}
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
/* .v-text-field__details {
  display: none;
} */

.much-input .v-input__control {
  height: 30px;
}
/* .v-text-field__slot >input {
  border: 1px solid #000;
} */
</style>