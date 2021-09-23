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
      <v-select
        v-if="filedVal"
        :items="compares"
        hide-details=""
        label="比较方式"
        item-text="Value"
        item-value="Key"
        v-model="compareVal"
      ></v-select>
      <v-select
        v-if="filedVal"
        :items="ValueEnumKvs"
        hide-details=""
        label="取值方式"
        item-text="Value"
        item-value="Key"
        v-model="valueEnum"
        @change="handlevalueEnumChange"
      ></v-select>
      <component v-if="valueEnum && valueComponent" :is="valueComponent" />
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        text
        small
        color="p"
        @click="addCond"
        :disabled="!value_Id || !valueEnum || !compareVal"
      >
        确定
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import ComboboxVue from "../../../components/Inputs/Combobox.vue";
import {
  getEnumValues,
  getModelObjectItems,
  getModuleStrut
} from "../../../generate";
const compareDic = {
  /**
   * 大于等于
   */
  gte: [f => ["Int32", "Decimal", "Long", "DateTime"].includes(f.Type)],
  /**
   * 小于等于
   */
  lte: [f => ["Int32", "Decimal", "Long", "DateTime"].includes(f.Type)],
  /**
   * 等于
   */
  eq: [() => true],
  /**
   * 不等于
   */
  not_eq: [() => true],
  /**
   * 包含
   */
  like: [
    f => !f.EnumValues || f.EnumValues.length == 0,
    f => !["Int32", "Decimal", "Long", "DateTime"].includes(f.Type),
    f => !f.Relate
  ],
  /**
   * 不包含
   */
  not_like: [
    f => !f.EnumValues || f.EnumValues.length == 0,
    f => !["Int32", "Decimal", "Long", "DateTime"].includes(f.Type),
    f => !f.Relate
  ]
};
export default {
  props: {
    moduleName: String
  },
  data() {
    return {
      fields: [],
      CompareEnumKvs: [],
      ValueEnumKvs: [],
      compares: [],
      valueComponent: null,

      filedVal: null,
      compareVal: null,
      valueEnum: null,
      value_Id: null,
      valueText: null
    };
  },
  async mounted() {
    let { FieldInfoStruts } = await getModuleStrut(this.moduleName);
    this.fields = FieldInfoStruts.map(v => ({
      ...v,
      Key: v.Name,
      Value: v.Description
    }));
    this.CompareEnumKvs = await getEnumValues("FlowNodeCond", "CompareEnum");
    this.ValueEnumKvs = await getEnumValues("FlowNodeCond", "ValueEnum");
  },
  methods: {
    handleFieldChange(val) {
      this.compareVal = null;
      this.compares = [];
      this.valueEnum = null;
      this.valueComponent = null;
      this.value = null;
      this.valueText = null;

      let field = this.fields.find(v => v.Key == val);
      let keys = Object.entries(compareDic)
        .filter(([, arr]) => !arr.some(func => !func(field)))
        .map(([key]) => key);
      this.compares = this.CompareEnumKvs.filter(v => keys.includes(v.Key));
    },
    async handlevalueEnumChange(val) {
      let arr = await getModelObjectItems(this.moduleName);
      let option = arr.find(v => v.Name == this.filedVal);
      let items = this.fields.filter(v => v.Type == option.Type);
      let self = this;

      switch (val) {
        case "input_value":
          this.valueComponent = {
            components: {
              "v-input": () => import("../../../components/Inputs")
            },
            render(h) {
              return h("v-input", {
                props: {
                  ...option,
                  value: self.value_Id,
                  isXs: true,
                  canEdit: true,
                  Description: "指定内容",
                  Readonly: null,
                  component: option.Relate ? ComboboxVue : null,
                  requestUrl: option.Relate
                    ? `/api/WorkFlow/RelateKvs/${option.Relate}`
                    : null,
                  callback: ({ value }) => {
                    if (value && value.Key && value.Value) {
                      self.value_Id = value.Key;
                      self.valueText = value.Value;
                    } else {
                      self.valueText = value;
                    }
                  }
                },
                on: {
                  input: val => (self.value_Id = val)
                }
              });
            }
          };
          break;
        case "form_field":
          this.valueComponent = {
            render(h) {
              return h("v-select", {
                props: {
                  items,
                  "hide-details": true,
                  label: "取值字段",
                  "item-text": "Value",
                  "item-value": "Key",
                  value: self.value_Id
                },
                on: {
                  input: val => {
                    self.value_Id = val;
                    self.valueText = items
                      .filter(v => v.Key == val)
                      .map(v => v.Value)
                      .find(v => v);
                  }
                }
              });
            }
          };
          break;

        default:
          break;
      }
    },
    addCond() {
      this.$emit("add-node-cond", {
        FieldName: this.filedVal,
        CompareEnum: this.compareVal,
        ValueEnum: this.valueEnum,
        Value_Id: this.value_Id,
        ValueText: this.valueText
      });
    }
  }
};
</script>

<style>
</style>