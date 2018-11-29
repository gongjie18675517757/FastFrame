<template>
  <span>
    <a-btn
      class="btn-link"
      v-if="info.IsLink"
      flat
      small
      color="primary"
      :moduleName="moduleName"
      name="Get"
      @click="$emit('toEdit')"
    >{{value}}</a-btn>
    <a-btn
      class="btn-link"
      v-else-if="isRelate && RelatePermission"
      flat
      small
      color="primary"
      :moduleName="moduleName"
      name="Get"
      @click="toRelate"
    >{{value}}</a-btn>
    <span v-else>{{value}}</span>
  </span>
</template>

<script>
import { existBtn } from "@/permission.js";
import { showDialog } from "@/utils";
export default {
  props: {
    info: {
      type: Object,
      default: function() {
        return {};
      }
    },
    model: {
      type: Object,
      default: function() {
        return {};
      }
    },
    moduleName: String
  },
  data() {
    return {
      RelatePermission: false
    };
  },
  async created() {
    if (this.info.Relate) {
      let exists = await existBtn(this.info.Relate.ModuleName, "Get");
      this.RelatePermission = exists;
    }
  },
  computed: {
    isRelate() {
      return !!this.info.Relate;
    },
    value() {
      let val = this.model[this.info.Name];
      if (this.info.Type == "Boolean") {
        if (val) return "是";
        else return "否";
      }
      if (this.info.Relate) {
        let tempName = this.info.Name.replace("_Id", "");
        let name = this.info.Relate.RelateFields[0];
        if (this.model[tempName]) {
          return this.model[tempName][name];
        }
      }
      return val;
    }
  },
  methods: {
    toRelate() {
      let { ModuleName } = this.info.Relate;
      showDialog(`${ModuleName}_Add`, {
        id: this.model[this.info.Name]
      });
    }
  }
};
</script>

<style>
.btn-link .v-btn__content {
  font-size: 13px;
}
</style>
