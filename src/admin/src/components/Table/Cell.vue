<template>
  <span>
    <a
      v-if="info.IsLink && val"
      :moduleName="moduleName"
      name="Get"
      @click="$emit('toEdit',model)"
    >{{value}}</a>
    <a v-else-if="isFile &&  val" @click="toRelate">下载</a>
    <!-- <a v-else-if="Foreignkey" :moduleName="moduleName" name="Get" @click="toRelate">{{value}}</a> -->
    <span v-else>{{value}}</span>
  </span>
</template>

<script>
import { existBtn } from "@/permission.js";
import { showDialog, getValue } from "@/utils";
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
    isFile() {
      return this.isRelate && this.info.Relate.ModuleName == "Resource";
    },
    val() {
      return getValue(this.model, this.info.Name);
    },
    Foreignkey() {
      return this.isRelate ? this.val : "";
    },
    value() {
      let val = this.val;
      let length = this.info.Length || 0;
      if (length >= 4000) return val.replace(/<[^>]+>/g,"").substring(0, 200);
      if (this.info.Type == "Boolean") {
        if (val) return "是";
        else return "否";
      }
      if (this.info.EnumValues && this.info.EnumValues.length > 0) {
        let kv = this.info.EnumValues.find(r => r.Key == val) || {};
        return kv.Value || "";
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
      if (this.isFile && this.val) {
        let url = `/api/resource/get/${this.val}`;
        window.open(url);
        return;
      }
      let { ModuleName } = this.info.Relate;
      if (this.Foreignkey)
        showDialog(`${ModuleName}_Add`, {
          id: this.Foreignkey
        });
    }
  }
};
</script>

 
