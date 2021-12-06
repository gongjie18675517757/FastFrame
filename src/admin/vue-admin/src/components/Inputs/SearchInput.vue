<template>
  <span>
    <span v-if="this.disabled && !isXs">{{ getField(select) || "无" }}</span>
    <v-autocomplete
      v-else
      :loading="loading"
      :items="items"
      :search-input.sync="search"
      :filter="() => true"
      v-model="select"
      :clearable="!disabled"
      :label="label"
      :readonly="disabled"
      :errorMessages="errorMessages"
      :placeholder="description"
      :item-text="getField"
      @change="change"
      return-object
      dense
      flat
    >
      <template slot="no-data">
        <v-list-item>
          <v-list-item-title>请输入关键字搜索</v-list-item-title>
        </v-list-item>
      </template>
      <template slot="selection" slot-scope="{ item }">{{
        getField(item)
      }}</template>

      <template #default>
        <slot></slot>
      </template>
      <template #prepend>
        <slot name="prepend"></slot>
      </template>
    </v-autocomplete>
  </span>
</template>

<script>
import { getModuleStrut } from "@/generate";
import { getValue, setValue, fmtRequestPars } from "@/utils";
export default {
  props: {
    model: Object,
    value: [String, Array],
    disabled: Boolean,
    label: String,
    filter: [Array, Function],
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
      search: null,
      select: null,
      fields: [],
      canQueryFields: [],
    };
  },
  computed: {
    relateModel() {
      let name = this.Name.replace("_Id", "");
      return getValue(this.model || {}, name);
    },
    url() {
      return typeof this.requestUrl == "function"
        ? this.requestUrl.call(this, this.model)
        : this.requestUrl;
    },
  },
  async mounted() {
    let { RelateFields, FieldInfoStruts } = await getModuleStrut(this.Relate);
    this.fields = RelateFields;
    this.canQueryFields = FieldInfoStruts.filter(
      (v) => v.Type != "EnumName"
    ).map((v) => v.Name);
    if (this.value) {
      this.items = [this.relateModel];
      this.select = this.relateModel;
    }

    if (!this.disabled) this.querySelections("");
  },
  watch: {
    search(v) {
      v && this.querySelections(v);
    },
    url() {
      this.change();
      this.querySelections("");
    },
  },
  methods: {
    getField(item) {
      if (!item) {
        return null;
      } else {
        let values = this.fields.map((r, i) => {
          let val = getValue(item, r);
          if (i == 0) {
            return val;
          } else {
            return `[${val}]`;
          }
        });

        return values.join("");
      }
    },
    async querySelections(v = "") {
      this.loading = true;
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);

      let url = this.url;

      let qs = {
        Filters: [
          {
            Key: "and",
            Value: [
              {
                Name: this.fields
                  .filter((v) => this.canQueryFields.includes(v))
                  .join(";"),
                Compare: "$",
                Value: v,
              },
              {
                Name: "Id",
                Compare: "!=",
                Value: this.value || "",
              },
              ...filter,
            ].filter((v) => v.Value),
          },
        ].filter((v) => v.Value.length > 0),
      };

      try {
        let { Data } = await this.$http.get(
          `${url}?qs=${JSON.stringify(qs, fmtRequestPars)}`
        );

        this.items = [...(this.select ? [this.select] : []), ...Data];
        this.loading = false;
      } catch (error) {
        window.console.error(error);
      }
    },
    change($event) {
      $event=$event || {}
      this.$emit("change", $event);
      this.$emit("input", $event.Id || null);
      if (!$event.Id) {
        this.items = [];
        let name = this.Name.replace("_Id", "");
        setValue(this.model, name, null);
        this.$nextTick(this.querySelections);
      } else {
        let name = this.Name.replace("_Id", "");
        setValue(this.model, name, $event);
      }
    },
  },
};
</script>

 