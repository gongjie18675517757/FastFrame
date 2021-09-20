<template>
  <v-card style="margin-top:-16px;">
    <v-card-text
      style="padding:5px;max-height:50vh;overflow: auto"
      v-if="!CheckerEnum || !hasSelectEnum.includes(CheckerEnum)"
    >
      <v-radio-group v-model="CheckerEnum" label="审核人类型:">
        <v-radio
          v-for="kv in CheckerEnumKvs"
          :key="kv.Key"
          :label="kv.Value"
          :value="kv.Key"
        ></v-radio>
      </v-radio-group>
    </v-card-text>
    <v-card-text
      style="padding:5px;max-height:50vh;overflow: auto"
      v-if="CheckerEnum && hasSelectEnum.includes(CheckerEnum)"
    >
      <v-text-field
        placeholder="输入关键字:"
        v-model="kw"
        hide-details
        append-icon="search"
      ></v-text-field>
      <v-divider></v-divider>
      <v-chip
        v-for="(r, rIndex) in selectedList"
        :key="r.Key"
        @click:close="hanldeRemoveSelected(r, rIndex)"
        label
        close
        small
        color="p"
        text-color="#fff"
        style="margin:2px;"
        >{{ r.Value }}</v-chip
      >
      <v-divider></v-divider>
      <v-list dense>
        <v-list-item
          v-for="(r, rIndex) in list"
          :key="r.Key"
          @click="hanldeAddSelected(r, rIndex)"
        >
          <v-list-item-title>
            {{ r.Value }}
          </v-list-item-title>
        </v-list-item>
      </v-list>
    </v-card-text>
    <v-card-actions>
      <v-btn text small color="p" v-if="CheckerEnum && hasSelectEnum.includes(CheckerEnum)" @click="CheckerEnum = null">
        取消
      </v-btn>
      <v-spacer></v-spacer>
      <v-btn
        text
        small
        color="p"
        @click="addEvent"
        :disabled="CheckerEnum == null || (hasSelectEnum.includes(CheckerEnum) && selectedList.length==0)"
      >
        确定
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { getEnumValues } from "../../../generate";

export default {
  data() {
    return {
      CheckerEnumKvs: [],
      CheckerEnum: null,
      hasSelectEnum: ["user", "role", "field", "dept"],
      kw: null,
      list: [],
      selectedList: []
    };
  },
  watch: {
    CheckerEnum() {
      if (this.CheckerEnum && this.hasSelectEnum.includes(this.CheckerEnum)) {
        this.kw = null;
        this.list = [];
        this.selectedList = [];
        this.loadList();
      }
    }
  },
  async mounted() {
    this.CheckerEnumKvs = await getEnumValues("FlowNodeChecker", "CheckerEnum");
  },
  methods: {
    addEvent() {
      
      this.$emit("add-node-checker");
    },
    async loadList() {
      let res = await this.$http.get(
        `/api/WorkFlow/CheckerList?checkerEnum=${
          this.CheckerEnum
        }&moduleName=&kw=${this.kw || ""}`
      );

      this.list = res.filter(v => !this.selectedList.some(x => x.Key == v.Key));
    },
    hanldeAddSelected(r, rIndex) {
      this.selectedList.push(r);
      this.list.splice(rIndex, 1);
    },
    hanldeRemoveSelected(r, rIndex) {
      this.list.splice(0, 0, r);
      this.selectedList.splice(rIndex, 1);
    }
  }
};
</script>

<style>
</style>