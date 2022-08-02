<template>
  <v-input dense :disabled="disabled" :errorMessages="errorMessages">
    <template #prepend>
      <slot name="prepend"></slot>
    </template>

    <template #default>
      
        <v-menu v-model="visible" offset-y :close-on-content-click="!multiple">
          <template #activator="{ on }">
            <input
              type="text"
              :value="text"
              :readonly="disabled"
              :placeholder="description"
              v-on="on"
              @blur="handleBlur"
              style="width:100%;"
            />
          </template>
          <v-data-table fixed-header height="30vh" dense>
            <template #header>
              <thead v-if="columns.length > 1">
                <tr>
                  <th style="width: 50px">#</th>
                  <th v-for="c in columns" :key="c.Name">
                    {{ c.Description }}
                  </th>
                </tr>
              </thead>
            </template>
            <template #body>
              <tbody>
                <tr v-for="item in items" :key="item.Id">
                  <td>
                    <v-simple-checkbox
                      :value="selectionKeys.includes(item.Id)"
                      @input="handleCheckChange(item, $event)"
                    ></v-simple-checkbox>
                  </td>
                  <td v-for="c in columns" :key="c.Name">
                    <Cell :info="c" :props="{ item }" />
                  </td>
                </tr>
              </tbody>
            </template>
          </v-data-table>
        </v-menu>
        <div class="v-input__append-inner">
          <div class="v-input__icon v-input__icon--append">
            <v-icon>
              {{ visible ? "mdi-menu-up" : "mdi-menu-down" }}
            </v-icon>
          </div>
        </div>
       
    </template>
  </v-input>
</template>

<script>
import { getModuleStrut } from "@/generate";
import { getValue, fmtRequestPars, setValue } from "../../utils";
import { getColumns } from "../../generate";
import Cell from "../Table/Cell";

export default {
  components: {
    Cell,
  },
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
    multiple: Boolean,
    description: String,
    requestUrl: [String, Function],
  },
  data() {
    return {
      visible: false,
      loading: false,
      items: [],
      search: null,
      select: null,
      fields: [],
      canQueryFields: [],
      columns: [],
    };
  },
  computed: {
    selection() {
      let arr = [];
      const relate_value = getValue(this.model, this.Name.replace("_Id", ""));
      if (Array.isArray(relate_value)) {
        arr = relate_value;
      } else {
        arr = [relate_value];
      }

      return arr.filter((v) => v);
    },
    selectionKeys() {
      return this.selection.map((v) => v.Id);
    },
    text() {
      return this.selection.map(this.getField).join(",");
    },
    url() {
      return typeof this.requestUrl == "function"
        ? this.requestUrl.call(this, this.model)
        : this.requestUrl;
    },
  },
  watch: {
    url() {
      this.items = [];
      this.loadList();
    },
    visible(val) {
      if (val) {
        this.items = [];
        this.loadList();
      }
    },
  },
  async mounted() {
    let { RelateFields, FieldInfoStruts } = await getModuleStrut(this.Relate);
    this.fields = RelateFields;
    this.columns = (await getColumns(this.Relate)).filter((v) =>
      RelateFields.includes(v.Name)
    );
    this.canQueryFields = FieldInfoStruts.filter(
      (v) => v.Type != "EnumName"
    ).map((v) => v.Name);
  },

  methods: {
    handleBlur() {
      event.target.value = this.text;
    },
    handleCheckChange(item, value) {
      if (this.multiple) {
        console.log(1);
      } else {
        if (value) {
          this.$emit("input", item.Id);
          this.$emit("change", item);
          setValue(this.model, this.Name.replace("_Id", ""), item);
        } else {
          this.$emit("input", null);
          this.$emit("change", null);
          setValue(this.model, this.Name.replace("_Id", ""), null);
        }
      }
    },
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
    async loadList() {
      this.loading = true;
      let filter = this.filter || [];
      if (typeof filter == "function")
        filter = await filter.call(this, this.model);

      let url = this.url;

      let qs = {
        KeyWord: null,
      };

      try {
        let { Data, Total } = await this.$http.get(
          `${url}?qs=${JSON.stringify(qs, fmtRequestPars)}`
        );

        this.items = Data;
        this.total = Total;
        this.loading = false;
      } catch (error) {
        window.console.error(error);
      }
    },
  },
};
</script>

 