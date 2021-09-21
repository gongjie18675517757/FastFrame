<template>
  <v-navigation-drawer
    :value="value"
    @input="handleInput"
    absolute
    temporary
    right
    width="300px"
    style="border-radius: 0px;"
  >
    <v-card v-if="selectedNode" style="box-shadow: none;">
      <v-toolbar flat dense color="transparent">
        <v-toolbar-title>{{ selectedNode.Title }}节点属性</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon @click="handleClose" title="关闭">
          <v-icon>close</v-icon>
        </v-btn>
      </v-toolbar>
      <v-divider></v-divider>
      <v-card-text :style="{ height: isDialog ? 'calc(100vh - 102px)' : null }">
        <v-text-field
          label="节点名称:"
          v-model="selectedNode.Title"
          hide-details
          :disabled="disabled"
        ></v-text-field>
        <template v-if="['check'].includes(selectedNode.NodeEnum)">
          <v-radio-group
            v-model="selectedNode.CheckEnum"
            label="审批方式:"
            hide-details
            :disabled="disabled"
          >
            <v-radio label="或签(多人时,一个人审核即通过)" value="or"></v-radio>
            <v-radio
              label="会签(多人时,全部人审核即通过)"
              value="and"
            ></v-radio>
          </v-radio-group>
          <legend
            class="v-label theme--light"
            style="left: 0px; right: auto; position: relative;cursor: text;font-size: 14px;height: auto;"
          >
            <br />
            流程节点审批人:
            <v-menu
              v-model="addCheckerEnum"
              offset-y
              :close-on-content-click="false"
              min-width="200px"
              v-if="!disabled"
            >
              <template #activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on" text small>
                  添加审批人
                </v-btn>
              </template>
              <flow-node-checker-vue
                v-if="addCheckerEnum"
                @add-node-checker="handleAddNodeCheckers"
                :moduleName="moduleName"
              />
            </v-menu>
            <v-list dense>
              <v-list-item v-if="selectedNode.Checkers.length == 0">
                <v-list-item-title>
                  未定义审核人
                  <v-btn
                    v-if="!disabled"
                    text
                    small
                    color="info"
                    @click="addCheckerEnum = true"
                    >添加</v-btn
                  >
                </v-list-item-title>
              </v-list-item>

              <v-list-item
                v-for="(r, rIndex) in selectedNode.Checkers"
                :key="rIndex"
              >
                <v-list-item-title>
                  <v-chip
                    label
                    small
                    style="margin-left:5px;"
                    color="secondary"
                    >{{ CheckerEnumObj[r.CheckerEnum] }}</v-chip
                  >
                  <v-chip
                    label
                    small
                    style="margin-left:5px;"
                    color="p"
                    text-color="#fff"
                    v-if="r.CheckerName"
                    >{{ r.CheckerName }}</v-chip
                  >
                </v-list-item-title>
                <v-list-item-icon v-if="!disabled">
                  <v-icon color="p" @click="removeChecker(rIndex)"
                    >close</v-icon
                  >
                </v-list-item-icon>
              </v-list-item></v-list
            >
          </legend>
          <legend
            class="v-label theme--light"
            style="left: 0px; right: auto; position: relative;cursor: text;font-size: 14px;height: auto;"
          >
            <br />
            触发事件:
            <v-menu
              v-if="!disabled"
              v-model="addEventEnum"
              offset-y
              :close-on-content-click="false"
              min-width="200px"
            >
              <template #activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on" text small>
                  添加事件
                </v-btn>
              </template>
              <flow-node-event-vue
                v-if="addEventEnum"
                @add-node-events="handleAddNodeEvents"
              />
            </v-menu>
            <v-list dense>
              <v-list-item v-if="selectedNode.Events.length == 0">
                <v-list-item-title>
                  未定义事件
                  <v-btn
                    v-if="!disabled"
                    text
                    small
                    color="info"
                    @click="addEventEnum = true"
                    >添加</v-btn
                  >
                </v-list-item-title>
              </v-list-item>

              <v-list-item
                v-for="(r, rIndex) in selectedNode.Events"
                :key="rIndex"
              >
                <v-list-item-title>
                  <v-chip label small color="primary">{{
                    EventTriggerObj[r.EventTrigger]
                  }}</v-chip>
                  <v-chip
                    label
                    small
                    style="margin-left:5px;"
                    color="secondary"
                    >{{ EventNotifyObj[r.EventNotify] }}</v-chip
                  >
                  <v-chip label small style="margin-left:5px;" color="accent">{{
                    EventTargetObj[r.EventTarget]
                  }}</v-chip>
                </v-list-item-title>
                <v-list-item-icon v-if="!disabled">
                  <v-icon color="p" @click="removeEvent(rIndex)">close</v-icon>
                </v-list-item-icon>
              </v-list-item></v-list
            >
          </legend>
        </template>
        <template v-if="['cond'].includes(selectedNode.NodeEnum)">
          <legend
            class="v-label theme--light"
            style="left: 0px; right: auto; position: relative;cursor: text;font-size: 14px;height: auto;"
          >
            <br />
            分支条件:
            <v-menu
              v-if="!disabled"
              v-model="addCondEnum"
              offset-y
              :close-on-content-click="false"
              min-width="200px"
            >
              <template #activator="{ on, attrs }">
                <v-btn color="primary" dark v-bind="attrs" v-on="on" text small>
                  添加条件
                </v-btn>
              </template>
              <flow-node-cond-vue
                v-if="addCondEnum"
                :moduleName="moduleName"
                @add-node-conds="handleAddNodeConds"
              />
            </v-menu>
            <v-list dense>
              <v-list-item v-if="selectedNode.Conds.length == 0">
                <v-list-item-title>
                  未定义条件
                  <v-btn
                    v-if="!disabled"
                    text
                    small
                    color="info"
                    @click="addCondEnum = true"
                    >添加</v-btn
                  >
                </v-list-item-title>
              </v-list-item>

              <v-list-item
                v-for="(r, rIndex) in selectedNode.Conds"
                :key="rIndex"
              >
                <v-list-item-title> </v-list-item-title>
                <v-list-item-icon v-if="!disabled">
                  <v-icon color="p" @click="removeCond(rIndex)">close</v-icon>
                </v-list-item-icon>
              </v-list-item></v-list
            >
          </legend>
        </template>
      </v-card-text>
      <!-- <v-divider></v-divider> -->
      <!-- <v-card-actions>
        <v-btn text @click="handleClose">
          取消
        </v-btn>
        <v-spacer></v-spacer>
        <v-btn color="p" text @click="handleClose">
          确定
        </v-btn>
      </v-card-actions> -->
    </v-card>
  </v-navigation-drawer>
</template>

<script>
import { getEnumValues } from "../../../generate";
import { groupBy, createObject } from "../../../utils";
import FlowNodeCheckerVue from "./FlowNodeChecker.vue";
import FlowNodeCondVue from './FlowNodeCond.vue';
import FlowNodeEventVue from "./FlowNodeEvent.vue";


export default {
  components: {
    FlowNodeEventVue,
    FlowNodeCheckerVue,
    FlowNodeCondVue
  },
  props: {
    selectedNode: Object,
    value: Boolean,
    disabled: Boolean,
    isDialog: Boolean,
    moduleName: String
  },
  data() {
    return {
      addEventEnum: false,
      EventTriggerObj: {},
      EventNotifyObj: {},
      EventTargetObj: {},

      addCheckerEnum: false,
      CheckerEnumObj: {},

      addCondEnum: false
    };
  },
  async mounted() {
    this.EventTriggerObj = createObject(
      await getEnumValues("FlowNodeEvent", "EventTrigger")
    );
    this.EventNotifyObj = createObject(
      await getEnumValues("FlowNodeEvent", "EventNotify")
    );
    this.EventTargetObj = createObject(
      await getEnumValues("FlowNodeEvent", "EventTarget")
    );
    this.CheckerEnumObj = createObject(
      await getEnumValues("FlowNodeChecker", "CheckerEnum")
    );
  },
  methods: {
    handleClose() {
      this.$emit("close");
    },
    handleInput() {
      this.$emit("input", ...arguments);
    },
    handleAddNodeEvents(arr) {
      this.addEventEnum = false;
      let brr = [...this.selectedNode.Events, ...arr];
      this.selectedNode.Events = groupBy(
        brr,
        v => ({
          EventTrigger: v.EventTrigger,
          EventNotify: v.EventNotify,
          EventTarget: v.EventTarget
        }),
        (a, b) =>
          a.EventTrigger == b.EventTrigger &&
          a.EventNotify == b.EventNotify &&
          a.EventTarget == b.EventTarget
      ).map(v => v.key);
    },
    removeEvent(rIndex) {
      this.selectedNode.Events.splice(rIndex, 1);
    },
    handleAddNodeCheckers(arr) {
      this.addCheckerEnum = false;
      let brr = [...this.selectedNode.Checkers, ...arr];
      this.selectedNode.Checkers = groupBy(
        brr,
        v => ({
          CheckerEnum: v.CheckerEnum,
          Checker_Id: v.Checker_Id,
          CheckerName: v.CheckerName
        }),
        (a, b) =>
          a.CheckerEnum == b.CheckerEnum &&
          a.Checker_Id == b.Checker_Id &&
          a.CheckerName == b.CheckerName
      ).map(v => v.key);
    },
    removeChecker(rIndex) {
      this.selectedNode.Checkers.splice(rIndex, 1);
    },
    handleAddNodeConds(arr) {
      this.addCondEnum = false;
      let brr = [...this.selectedNode.Conds, ...arr];
      this.selectedNode.Conds = groupBy(
        brr,
        v => ({
          CheckerEnum: v.CheckerEnum
        }),
        (a, b) => a.CheckerEnum == b.CheckerEnum
      ).map(v => v.key);
    },
    removeCond(rIndex) {
      this.selectedNode.Conds.splice(rIndex, 1);
    }
  }
};
</script>

<style>
</style>