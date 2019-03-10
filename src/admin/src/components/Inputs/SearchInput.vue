<template>
  <v-autocomplete
    :loading="loading"
    :items="items"
    :search-input.sync="search"
    :filter="()=>true"
    v-model="select"
    clearable
    :label="label"
    :readonly="disabled"
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
    <template slot="selection" slot-scope="{ item }">
      <strong>{{item[fields[0]]}}</strong>
      <span v-for="(f,index) in fields.filter((a,b)=>b>0)" :key="index">[{{item[f]}}]</span>
    </template>
    <template slot="item" slot-scope="{ item }">
      <v-list-tile-content>
        <v-list-tile-title v-text="item[fields[0]]"></v-list-tile-title>
        <v-list-tile-sub-title
          v-for="(f,index) in fields.filter((a,b)=>b>0)"
          :key="index"
          v-text="item[f]"
        ></v-list-tile-sub-title>
      </v-list-tile-content>
      <!-- <v-list-tile-action>
                <v-icon>mdi-coin</v-icon>
      </v-list-tile-action>-->
    </template>
  </v-autocomplete>
</template>

<script>
import { getModuleStrut } from "@/generate";
import { showDialog } from "@/utils";
export default {
  props: {
    model: Object,
    value: String,
    disabled: Boolean,
    label: String,
    filter: [Array, Function],
    Name: String,
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
      return this.model[name] || {};
    }
  },
  async mounted() {
    let { RelateFields } = await getModuleStrut(this.Relate);
    this.fields = RelateFields;
    if (this.value) {
      this.items = [this.relateModel];
      this.select = this.relateModel;
    }
  },
  watch: {
    search(v) {
      v && this.querySelections(v);
    }
  },
  methods: {
    async querySelections(v) {
      this.loading = true;
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);
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
              Value: this.relateModel.Id
            },
            ...filter
          ]
        }
      });
      this.items = Data;
      this.loading = false;
    },
    change($event = {}) {
      this.$emit("change", $event);
      this.$emit("input", $event.Id || null);
      if (!$event.Id) {
        this.items = [];
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
            Value: this.relateModel.Id
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

<style>
</style>