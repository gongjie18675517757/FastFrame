<template>
  <span>
    <span v-if="this.disabled && !isXs">{{text || '无'}}</span>
    <v-autocomplete
      v-else
      :loading="loading"
      :items="allItems"
      :search-input.sync="search"
      :filter="()=>true"
      :value="value"
      :clearable="!disabled"
      :label="label"
      :readonly="disabled"
      :errorMessages="errorMessages"
      :placeholder="description"
      multiple
      dense
      @change="change"
      key="Id"
      :item-value="v=>v.Id"
      :item-text="getField"
      return-object
    >
      <template slot="append-item">
        <v-lazy>
          <v-list-item link @show="moreLoad">
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
  </span>
</template>

<script>
import { getModuleStrut } from "../../generate";
import { distinct, getValue, throttle, fmtRequestPars } from "../../utils";
export default {
  props: {
    model: Object,
    value: [Array],
    disabled: Boolean,
    label: String,
    filter: [Array, Function],
    Name: String,
    errorMessages: Array,
    Relate: {
      type: String,
      required: true
    },
    isXs: Boolean,
    description: String,
    requestUrl: [String, Function]
  },
  data() {
    return {
      loading: false,
      items: [],
      fields: [],
      canQueryFields: [],
      search: ""
    };
  },
  computed: {
    text() {
      return this.value.map(this.getField).join(",");
    },
    allItems() {
      return distinct(
        [...this.value, ...this.items],
        v => v.Id,
        v => v
      );
    }
  },
  async mounted() {
    let { RelateFields, FieldInfoStruts } = await getModuleStrut(this.Relate);
    this.fields = RelateFields;
    this.canQueryFields = FieldInfoStruts.filter(v => v.Type != "EnumName").map(
      v => v.Name
    );

    if (!this.disabled) this.querySelections("");
  },
  watch: {
    search(v) {
      v && this.querySelections(v);
    }
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
    querySelections: throttle(async function(v = "") {
      this.loading = true;
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);
      let url = this.requestUrl;
      if (typeof this.requestUrl == "function")
        url = this.requestUrl.call(this, this.model);

      let qs = {
        Filters: [
          {
            Key: "and",
            Value: [
              {
                Name: this.fields
                  .filter(v => this.canQueryFields.includes(v))
                  .join(";"),
                Compare: "$",
                Value: v
              },
              {
                Name: "Id",
                Compare: "!=",
                Value: this.value.join(",")
              },
              ...filter
            ].filter(v => v.Value)
          }
        ].filter(v => v.Value.length > 0)
      };
      try {
        let { Data } = await this.$http.get(
          `${url}?qs=${JSON.stringify(qs, fmtRequestPars)}`
        );
        this.items = Data;
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
      console.log(event);
    }
  }
};
</script>

 