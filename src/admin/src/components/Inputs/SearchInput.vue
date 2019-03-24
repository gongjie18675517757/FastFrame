<template>
  <span>
    <span v-if="this.disabled">{{getField(select) || '无'}}</span>
    <v-autocomplete
      v-else
      :loading="loading"
      :items="items"
      :search-input.sync="search"
      :filter="()=>true"
      v-model="select"
      :clearable="!disabled"
      :label="label"
      :disabled="disabled"
      :errorMessages="errorMessages"
      @change="change"
    >
      <template slot="no-data">
        <v-list-tile>
          <v-list-tile-title>
            请输入关键字,或者
            <strong>
              <a @click="openDialog">搜索</a>
            </strong>
          </v-list-tile-title>
        </v-list-tile>
      </template>
      <template slot="selection" slot-scope="{ item }">{{getField(item)}}</template>
      <template slot="item" slot-scope="{ item }">
        <v-list-tile-content>
          <v-list-tile-title v-text="getField(item)"></v-list-tile-title>
        </v-list-tile-content>
        <!-- <v-list-tile-action>
                <v-icon>mdi-coin</v-icon>
        </v-list-tile-action>-->
      </template>
    </v-autocomplete>
  </span>
</template>

<script>
import { getModuleStrut } from "@/generate";
import { showDialog, getValue, setValue } from "@/utils";
export default {
  props: {
    model: Object,
    value: String,
    disabled: Boolean,
    label: String,
    filter: [Array, Function],
    Name: String,
    errorMessages: Array,
    Relate: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      loading: false,
      items: [],
      search: null,
      select: null,
      fields: []
    };
  },
  computed: {
    relateModel() {
      let name = this.Name.replace("_Id", "");
      return getValue(this.model || {}, name);
    }
  },
  async mounted() {
    let { RelateFields } = await getModuleStrut(this.Relate);
    this.fields = RelateFields;
    if (this.value) {
      this.items = [this.relateModel];
      this.select = this.relateModel;
    }

    this.querySelections("");
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
    async querySelections(v = "") {
      this.loading = true;
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);

      try {
        let { Data } = await this.$http.post(`/api/${this.Relate}/list`, {
          Condition: {
            Filters: [
              {
                Name: this.fields.join(";"),
                Compare: "$",
                Value: v
              },
              {
                Name: "Id",
                Compare: "!=",
                Value: this.value || ""
              },
              ...filter
            ]
          }
        });
        this.items = [...(this.select ? [this.select] : []), ...Data];
        this.loading = false;
      } catch (error) {
        window.console.error(error);
      }
    },
    change($event = {}) {
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
    async openDialog() {
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);
      let rows = await showDialog(`${this.Relate}_List`, {
        single: true,
        queryFilter: [
          {
            Name: "Id",
            Compare: "!=",
            Value: this.value || ""
          },
          ...filter
        ]
      });
      if (rows.length > 0) {
        this.items = rows;
        this.select = rows[0];
        this.change(rows[0]);
      }
    }
  }
};
</script>

 