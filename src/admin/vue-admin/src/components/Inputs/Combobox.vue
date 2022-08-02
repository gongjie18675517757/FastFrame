<template>
  <v-combobox
    :value="value"
    :items="items"
    :clearable="!disabled"
    :label="label"
    :disabled="disabled"
    :errorMessages="errorMessages"
    :placeholder="description"
    :multiple="multiple"
    :dense="!isXs"
    :return-object="false"
    @input="handleChange"
    item-text="Value"
    item-value="Key"
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
import { debounce } from "../../utils";
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
    multiple: Boolean,
  },
  data() {
    return {
      keyword: null,
      items: [],
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
        this.items
          .filter((v) => val.includes(v.Key))
          .map((v) => v.Value)
          .join(",") || "无"
      );
    },
  },
  watch: {
    keyword: {
      immediate: true,
      handler: "loadList",
    },
    disabled: {
      immediate: true,
      handler: "loadList",
    },
  },
  methods: {
    getText({ item, index }) {
      if (item) {
        let txt = this.items
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
        let items = this.items.filter((v) => val.includes(v.Key));
        this.$emit("change", items);
      } else {
        let item = this.items.find((v) => v.Key == val);
        this.$emit("change", item);
      }
      this.keyword = null;
    },
    loadList: debounce(async function () {
      if (this.disabled) return;
      let url = this.requestUrl;
      if (!url.includes("?")) url = `${url}?`;
      url = `${url}&kw=${this.keyword || ""}`;
      this.items = await this.$http.get(url);
    }, 500),
  },
};
</script>