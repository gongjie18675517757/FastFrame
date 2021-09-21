<template>
  <v-card style="margin-top:-16px;">
    <v-card-text style="max-height:50vh;overflow: auto">
      <v-select
        :items="fields"
        hide-details=""
        label="字段名称"
        item-text="Value"
        item-value="Key"
        v-model="filedVal"
        @change="handleFieldChange"
      ></v-select>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn text small color="p" @click="addCond">
        确定
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { getModuleStrut } from "../../../generate";

export default {
  props: {
    moduleName: String
  },
  data() {
    return {
      fields: [],

      filedVal: null,
      compareVal: null
    };
  },
  async mounted() {
    let { FieldInfoStruts } = await getModuleStrut(this.moduleName);
    this.fields = FieldInfoStruts.map(v => ({
      ...v,
      Key: v.Name,
      Value: v.Description
    }));
  },
  methods: {
    handleFieldChange() {
      this.compareVal = null;
      
    },
    addCond() {
      const arr = [];
      this.$emit("add-node-conds", arr);
    }
  }
};
</script>

<style>
</style>