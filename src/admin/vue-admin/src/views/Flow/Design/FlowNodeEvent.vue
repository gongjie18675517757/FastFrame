<template>
  <v-card style="margin-top:-16px;">
    <v-card-text style="padding:5px;max-height:50vh;overflow: auto">
      <v-radio-group v-model="EventTrigger" label="触发时间:">
        <v-radio
          v-for="kv in EventTriggerKvs"
          :key="kv.Key"
          :label="kv.Value"
          :value="kv.Key"
        ></v-radio>
      </v-radio-group>
      <legend
        class="v-label theme--light"
        style="left: 0px; right: auto; position: relative;cursor: text;font-size: 14px;height: auto;"
      >
        <br />
        通知目标:
      </legend>
      <v-checkbox
        v-for="kv in EventTargetKvs"
        :key="kv.Key"
        :label="kv.Value"
        :value="kv.Key"
        v-model="EventTarget"
        hide-details=""
      />
      <legend
        class="v-label theme--light"
        style="left: 0px; right: auto; position: relative;cursor: text;font-size: 14px;height: auto;"
      >
        <br />
        通知方式:
      </legend>
      <v-checkbox
        v-for="kv in EventNotifyKvs"
        :key="kv.Key"
        :label="kv.Value"
        :value="kv.Key"
        v-model="EventNotify"
        hide-details=""
      />
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        text
        small
        color="p"
        @click="addEvent"
        :disabled="
          EventTrigger == null ||
            EventNotify.length == 0 ||
            EventTarget.length == 0
        "
      >
        确定
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { getEnumValues } from "../../../generate";
import { mapMany } from "../../../utils";
export default {
  data() {
    return {
      EventTrigger: null,
      EventNotify: [],
      EventTarget: [],

      EventTriggerKvs: [],
      EventNotifyKvs: [],
      EventTargetKvs: []
    };
  },
  async mounted() {
    this.EventTriggerKvs = await getEnumValues("FlowNodeEvent", "EventTrigger");
    this.EventNotifyKvs = await getEnumValues("FlowNodeEvent", "EventNotify");
    this.EventTargetKvs = await getEnumValues("FlowNodeEvent", "EventTarget");
  },
  methods: {
    addEvent() {
      let arr = mapMany(
        this.EventNotify.map(v =>
          this.EventTarget.map(x => ({
            EventTrigger: this.EventTrigger,
            EventNotify: v,
            EventTarget: x
          }))
        ),
        v => v
      );

      this.$emit("add-node-events", arr);
    }
  }
};
</script>

<style>
</style>