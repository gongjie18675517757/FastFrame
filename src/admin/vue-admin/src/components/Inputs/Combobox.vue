<template>
  <span>
    <span v-if="this.disabled && !isXs">{{ text }}</span>
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
  </span>
</template>

<script>
import { throttle } from "../../utils";
export default {
  props: {
    model: Object,
    requestUrl: String,
    value: [String, Array],
    disabled: Boolean,
    label: String,
    errorMessages: Array,
    isXs: Boolean,
    description: String,
    multiple: Boolean
  },
  data() {
    return {
      keyword: null,
      items: []
    };
  },
  computed: {
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
          .filter(v => val.includes(v.Key))
          .map(v => v.Value)
          .join(",") || "无"
      );
    }
  },
  watch: {
    keyword: {
      immediate: true,
      handler: "loadList"
    },
    disabled: {
      immediate: true,
      handler: "loadList"
    }
  },
  methods: {
    getText({ item, index }) {
      if (item) {
        let txt = this.values
          .filter(v => v.Key == item)
          .map(v => v.Value)
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
        let items = this.values.filter(v => val.includes(v.Key));
        this.$emit("change", items);
      } else {
        let item = this.values.find(v => v.Key == val);
        this.$emit("change", item);
      }
      this.keyword = null;
    },
    loadList: throttle(async function() {
      if (this.disabled) return;
      let url = this.requestUrl;
      if (!url.includes("?")) url = `${url}?`;
      url = `${url}&kw=${this.keyword || ""}`;
      this.items = await this.$http.get(url);
    }, 500)
  }
};
</script>