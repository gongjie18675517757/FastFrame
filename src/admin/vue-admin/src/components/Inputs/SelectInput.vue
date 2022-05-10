<template>
  <v-input
    dense
    class="v-input__slot_checkbox_container v-input-no-border"
    style="margin-top: 0px"
    v-if="values.length <= 5"
  >
    <template #default>
      <slot>
        <v-checkbox
          v-for="v in values"
          :key="v.Key"
          :value="v.Key"
          :label="v.Value"
          :input-value="value"
          @change="handleChange"
          :disabled="disabled"
          hide-details
          style="margin-top: 0px"
          dense
          :multiple="multiple"
        ></v-checkbox>
      </slot>
    </template>
    <template #prepend>
      <slot name="prepend"></slot>
    </template>
  </v-input>
  <v-combobox
    v-else
    :value="value"
    :items="items"
    :clearable="!disabled"
    :label="label"
    :readonly="disabled"
    :errorMessages="errorMessages"
    :placeholder="description"
    :multiple="multiple"
    :dense="!isXs"
    :return-object="false"
    @input="handleChange"
  >
    <template v-slot:selection="props">{{ getText(props) }}</template>
    <template #default>
      <slot></slot>
    </template>
    <template #prepend>
      <slot name="prepend"></slot>
    </template>
  </v-combobox>
</template>

<script>
export default {
  props: {
    value: [String, Array],
    disabled: Boolean,
    label: String,
    errorMessages: Array,
    isXs: Boolean,
    description: String,

    multiple: Boolean,
    values: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      keyword: null,
    };
  },
  computed: {
    items() {
      return this.values.map((v) => ({
        text: v.Value,
        value: v.Key,
      }));
    },

    /**
     * 字典文本
     */
    text() {
      let val = this.value;
      if (!this.multiple) {
        val = [val];
      }

      return (
        this.values
          .filter((v) => val.includes(v.Key))
          .map((v) => v.Value)
          .join(",") || "无"
      );
    },
  },
  methods: {
    getText({ item, index }) {
      if (item) {
        let txt = this.values
          .filter((v) => v.Key == item)
          .map((v) => v.Value)
          .join(",");
        if (this.multiple && this.value.length > index) {
          txt = `${txt},`;
        }
        return txt;
      } else {
        return "";
      }
    },
    handleChange(val) {
      val = val || null;
      this.$emit("input", val);

      if (this.multiple) {
        val = val || [];
        let items = this.values.filter((v) => val.includes(v.Key));
        this.$emit("change", items);
      } else {
        let item = this.values.find((v) => v.Key == val);
        this.$emit("change", item);
      }
      this.keyword = null;
    },
  },
};
</script>