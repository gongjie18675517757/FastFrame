<template>
  <v-navigation-drawer
    :value="value"
    @input="handleInput"
    absolute
    temporary
    right
    width="300px"
    style="border-radius: 0px"
  >
    <v-card v-if="selectedNode" style="box-shadow: none">
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
          @change="onChange"
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
            <v-radio
              label="投票(多人时,指定比例的人审核即通过)"
              value="vote"
            ></v-radio>
          </v-radio-group>
          <v-slider
            label="投票通过比例:"
            v-if="selectedNode.CheckEnum == 'vote'"
            v-model="selectedNode.VoteScale"
            thumb-label
            hide-details
            min="1"
            max="100"
            :disabled="disabled"
            @change="onChange"
            ><template v-slot:append>
              <v-text-field
                v-model="selectedNode.VoteScale"
                class="mt-0 pt-0"
                hide-details
                single-line
                type="number"
                style="width:40px"
                suffix="%"
                controls="false"
              ></v-text-field> </template
          ></v-slider>
          <legend
            class="v-label theme--light"
            style="
              left: 0px;
              right: auto;
              position: relative;
              cursor: text;
              font-size: 14px;
              height: auto;
            "
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
                    style="margin-left: 5px"
                    text-color="#fff"
                    color="p"
                    @input="removeChecker(rIndex)"
                    :close="!disabled"
                    >{{ CheckerEnumObj[r.CheckerEnum] }}:{{
                      r.CheckerName
                    }}</v-chip
                  >
                </v-list-item-title>
              </v-list-item></v-list
            >
          </legend>
          <legend
            class="v-label theme--light"
            style="
              left: 0px;
              right: auto;
              position: relative;
              cursor: text;
              font-size: 14px;
              height: auto;
            "
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
                  <v-chip
                    label
                    small
                    color="p"
                    text-color="#fff"
                    :close="!disabled"
                    @input="removeEvent(rIndex)"
                    >{{ EventTriggerObj[r.EventTrigger] }}:{{
                      EventNotifyObj[r.EventNotify]
                    }}=> {{ EventTargetObj[r.EventTarget] }}
                  </v-chip>
                </v-list-item-title>
              </v-list-item></v-list
            >
          </legend>
        </template>
        <template v-if="['cond'].includes(selectedNode.NodeEnum)">
          <v-text-field
            label="权重(越大越优先):"
            v-model="selectedNode.Weight"
            hide-details
            :disabled="disabled"
            @change="onChange"
            type="number"
          ></v-text-field>
          <legend
            class="v-label theme--light"
            style="
              left: 0px;
              right: auto;
              position: relative;
              cursor: text;
              font-size: 14px;
              height: auto;
            "
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
                  添加条件{{
                    selectedNode.Conds.length == 0 ? "" : "组(关系为“或”)"
                  }}
                </v-btn>
              </template>
              <flow-node-cond-vue
                v-if="addCondEnum"
                :moduleName="moduleName"
                @add-node-cond="handleAddNodeCond"
              />
            </v-menu>
            <v-list
              dense
              v-for="(arr, arrIndex) in selectedNode.Conds.length > 0
                ? selectedNode.Conds
                : [[]]"
              :key="arrIndex"
            >
              <v-subheader v-if="selectedNode.Conds.length > 0">
                条件组{{ arrIndex + 1 }}:
                <v-menu
                  v-if="!disabled"
                  :value="currCondIndex == arrIndex"
                  offset-y
                  :close-on-content-click="false"
                  min-width="200px"
                  @input="currCondIndex = $event ? arrIndex : null"
                >
                  <template #activator="{ on, attrs }">
                    <v-btn
                      color="primary"
                      dark
                      v-bind="attrs"
                      v-on="on"
                      text
                      small
                    >
                      添加条件
                    </v-btn>
                  </template>
                  <flow-node-cond-vue
                    v-if="currCondIndex == arrIndex"
                    :moduleName="moduleName"
                    @add-node-cond="handleAddNodeCond($event, arr)"
                  />
                </v-menu>
              </v-subheader>

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

              <v-list-item v-for="(r, rIndex) in arr" :key="rIndex" two-line>
                <v-list-item-title>
                  <v-list-item-content>
                    <v-list-item-title
                      ><code style="color: blue; padding: 15px">
                        {{ FieldNameObj[r.FieldName] }}
                      </code>
                      <v-icon
                        v-if="!disabled"
                        color="p"
                        @click="removeCond(rIndex, arr)"
                        style="float: right"
                        >close</v-icon
                      >
                    </v-list-item-title>
                    <v-list-item-subtitle>
                      <code
                        style="color: blue; padding: 15px"
                        :title="r.ValueText"
                      >
                        {{ CompareEnumObj[r.CompareEnum] }}
                        {{ r.ValueText | substring(10) }}({{
                          ValueEnumObj[r.ValueEnum]
                        }})
                      </code>
                    </v-list-item-subtitle>
                  </v-list-item-content>
                </v-list-item-title>
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
import { getEnumValues, getModuleStrut } from "../../../generate";
import { createObject } from "../../../utils";
import FlowNodeCheckerVue from "./FlowNodeChecker.vue";
import FlowNodeCondVue from "./FlowNodeCond.vue";
import FlowNodeEventVue from "./FlowNodeEvent.vue";

export default {
  components: {
    FlowNodeEventVue,
    FlowNodeCheckerVue,
    FlowNodeCondVue,
  },
  props: {
    selectedNode: Object,
    value: Boolean,
    disabled: Boolean,
    isDialog: Boolean,
    moduleName: String,
    onChange: Function,
  },
  data() {
    return {
      addEventEnum: false,
      EventTriggerObj: {},
      EventNotifyObj: {},
      EventTargetObj: {},

      addCheckerEnum: false,
      CheckerEnumObj: {},

      addCondEnum: false,
      currCondIndex: null,

      FieldNameObj: {},
      CompareEnumObj: {},
      ValueEnumObj: {},
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
    if (this.moduleName)
      this.FieldNameObj = createObject(
        (await getModuleStrut(this.moduleName)).FieldInfoStruts,
        (v) => v.Name,
        (v) => v.Description
      );
    this.CompareEnumObj = createObject(
      await getEnumValues("FlowNodeCond", "CompareEnum")
    );
    this.ValueEnumObj = createObject(
      await getEnumValues("FlowNodeCond", "ValueEnum")
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
      for (const b of arr) {
        if (
          !this.selectedNode.Events.some(
            (a) =>
              a.EventTrigger == b.EventTrigger &&
              a.EventNotify == b.EventNotify &&
              a.EventTarget == b.EventTarget
          )
        ) {
          this.selectedNode.Events.push(b);
        }
      }

      this.onChange && this.onChange();
    },
    removeEvent(rIndex) {
      this.selectedNode.Events.splice(rIndex, 1);

      this.onChange && this.onChange();
    },
    handleAddNodeCheckers(arr) {
      this.addCheckerEnum = false;

      for (const b of arr) {
        if (
          !this.selectedNode.Checkers.some(
            (a) =>
              a.CheckerEnum == b.CheckerEnum &&
              a.Checker_Id == b.Checker_Id &&
              a.CheckerName == b.CheckerName
          )
        ) {
          this.selectedNode.Checkers.push(b);
        }
      }

      this.onChange && this.onChange();
    },
    removeChecker(rIndex) {
      this.selectedNode.Checkers.splice(rIndex, 1);

      this.onChange && this.onChange();
    },
    handleAddNodeCond(item, arr) {
      this.addCondEnum = false;
      this.currCondIndex = null;
      if (!arr) {
        this.selectedNode.Conds.push([item]);
      } else if (
        !arr.some(
          (v) =>
            v.CheckerEnum == item.CheckerEnum &&
            v.CompareEnum == item.CompareEnum &&
            v.ValueEnum == item.ValueEnum &&
            v.Value_Id == item.Value_Id &&
            v.ValueText == item.ValueText
        )
      ) {
        arr.push(item);
      }

      this.onChange && this.onChange();
    },
    removeCond(rIndex, arr) {
      arr.splice(rIndex, 1);
      if (arr.length == 0) {
        let index = this.selectedNode.Conds.findIndex((v) => v == arr);
        this.selectedNode.Conds.splice(index, 1);
      }

      this.onChange && this.onChange();
    },
  },
};
</script>

<style>
input[type="number"] {
  -moz-appearance: textfield;
}
input[type="number"]::-webkit-inner-spin-button,
input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
</style>