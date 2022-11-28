<template>
  <v-combobox
    :value="value"
    :items="items"
    :clearable="!disabled && !!value"
    :label="label"
    :disabled="disabled"
    :errorMessages="errorMessages"
    :placeholder="description"
    :multiple="multiple"
    :dense="!isXs"
    :return-object="false"
    :item-text="itemText"
    :item-value="itemValue"
    @input="handleChange"
    @click:clear="handleChange(null)"
    @update:search-input="keyword = $event"
  >
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
  name:'v-combobox-input',
  props: {
    model: Object,
    requestUrl: [String, Function],
    value: [String, Array],
    disabled: Boolean,
    label: String,
    errorMessages: Array,
    isXs: Boolean,
    description: String,
    Name: String,
    multiple: Boolean,
    itemText: {
      type: String,
      default: "Value",
    },
    itemValue: {
      type: String,
      default: "Value",
    },
  },
  data() {
    return {
      keyword: null,
      items: [],
    };
  },
  computed: {
    url() {
      let url =
        typeof this.requestUrl == "function"
          ? this.requestUrl(this.model)
          : this.requestUrl;

      if (!url.includes("?")) url = `${url}?`;
      url = `${url}&kw=${this.keyword || ""}`;
      return url;
    },
  },
  watch: {
    url: {
      immediate: true,
      handler: "loadList",
    },
    disabled: {
      immediate: true,
      handler: "loadList",
    },
  },
  created() {
    this.loadList = debounce(this.loadList, 500).bind(this);
  },
  methods: {
    handleChange(val) {
      val = val || null;
      this.$emit("input", val);

      if (this.multiple) {
        val = val || [];
        let items = this.items.filter((v) => val.includes(v[this.itemValue]));
        this.$emit("change", items);
      } else {
        let item = this.items.find((v) => v[this.itemValue] == val);
        this.$emit("change", item);
      }
      this.keyword = null;
    },
    async loadList() {
      if (this.disabled) return;

      this.items = await this.$http.get(this.url);
    },
  },
};
</script>