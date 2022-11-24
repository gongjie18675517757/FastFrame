<template>
  <v-autocomplete
    :loading="loading"
    :items="allItems"
    :search-input.sync="search"
    :filter="() => true"
    :value="value"
    :clearable="!disabled"
    :label="label"
    :disabled="disabled"
    :errorMessages="errorMessages"
    :placeholder="description"
    multiple
    dense
    @change="change"
    key="Id"
    item-value="Id"
    item-text="Value"
    return-object
  >
    <template #append-item v-if="!finish">
      <v-lazy>
        <v-list-item @click="moreLoad">
          <v-list-item-title>更多</v-list-item-title>
        </v-list-item>
      </v-lazy>
    </template>
    <template #default>
      <slot></slot>
    </template>
    <template #prepend>
      <slot name="prepend"></slot>
    </template>
  </v-autocomplete>
</template>

<script>
import { distinct, throttle } from "../../utils";
export default {
  props: {
    model: Object,
    value: [Array],
    disabled: Boolean,
    label: String,
    Name: String,
    errorMessages: Array,
    Relate: {
      type: String,
      required: true,
    },
    isXs: Boolean,
    description: String,
    requestUrl: [String, Function],
  },
  data() {
    return {
      loading: false,
      items: [],
      search: "",
      pageIndex: 1,
      finish: false,
    };
  },
  computed: {
    text() {
      return this.value.map(this.Value).join(",");
    },
    allItems() {
      return distinct(
        [...this.value, ...this.items],
        (v) => v.Id,
        (v) => v
      );
    },
  },

  async mounted() {
    if (!this.disabled) this.querySelections("");
  },
  watch: {
    disabled(v) {
      if (!v) {
        this.querySelections(v);
      }
    },
    search(v) {
      this.pageIndex = 1;
      this.finish = false;
      v && this.querySelections(v);
    },
  },
  methods: {
    querySelections: throttle(async function (v = "") {
      this.loading = true;
      let url = this.requestUrl;
      if (typeof this.requestUrl == "function")
        url = this.requestUrl.call(this, this.model);

      try {
        let res = await this.$http.get(
          `${url}?kw=${v || ""}&page_index=${this.pageIndex}`
        );
        if (this.pageIndex == 1) this.items = res;
        else this.items.push(...res);

        if (res.length > 0) {
          this.pageIndex++;
        } else {
          this.finish = true;
        }
        this.loading = false;
      } catch (error) {
        window.console.error(error);
      }
    }, 1000),
    change($event = []) {
      this.$emit("change", $event);
      this.$emit("input", $event);
    },
    moreLoad() {
      this.querySelections(this.search);
    },
  },
};
</script>

 