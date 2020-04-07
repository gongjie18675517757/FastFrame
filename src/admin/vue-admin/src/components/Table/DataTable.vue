<template>
  <v-data-table
    :fixed-header="!!height"
    :height="height"
    :headers="headers"
    :loading="loading"
    :items="rows"
    :server-items-length="totalItems"
    :footer-props="pager"
    :hide-default-footer="hidePager"
    :value="value"
    v-on="listeners"
    :item-key="rowKey"
    :items-per-page.sync="itemsPerPage"
    :class="[...classArr]"
    :style="styleObj"
    :show-select="multiple"
    :single-select="!multiple"
    :show-expand="!!expandComponent"
    dense
  >
    <template slot="item.index" slot-scope="props">{{props.item.index}}</template>
    <template v-for="col in columns" :slot="`item.${col.Name}`" slot-scope="props">
      <component
        :key="col.Name"
        v-if="col.component"
        :is="col.component"
        :info="col"
        :model="props.item"
        :props="props"
        v-on="listeners"
      />
      <Cell :key="col.Name" v-else :info="col" :model="props.item" :props="props" v-on="listeners" />
    </template>
    <template slot="no-data">没有加载数据</template>
    <template v-if="!!expandComponent" v-slot:expanded-item="{ headers, item }">
      <td :colspan="headers.length">
        <component :is="expandComponent" :model="item" />
      </td>
    </template>
  </v-data-table>
</template>

<script>
import Cell from "./Cell.vue";
export default {
  components: {
    Cell
  },
  props: {
    value: {
      type: Array,
      default: function() {
        return [];
      }
    },
    rowKey: {
      type: String,
      default: "Id"
    },
    columns: {
      type: Array,
      default: function() {
        return [];
      }
    },
    items: {
      type: Array,
      default: function() {
        return [];
      }
    },
    height: String,
    totalItems: Number,
    multiple: Boolean,
    hidePager: {
      type: Boolean,
      default: true
    },
    loading: Boolean,
    classArr: {
      type: Array,
      default: () => []
    },
    styleObj: Object,
    expandComponent: [Object, Function]
  },
  data() {
    return {
      itemsPerPage: 20,
      page: 1,
      pager: {
        showFirstLastPage: true,
        "items-per-page-options": [10, 15, 20, 30, 50],
        firstIcon: "mdi-arrow-collapse-left",
        lastIcon: "mdi-arrow-collapse-right"
      }
    };
  },
  computed: {
    headers() {
      return [
        { text: "", width: "20px", value: "data-table-expand" },
        {
          text: "#",
          value: "index",
          sortable: false,
          width: "50px"
        },

        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name,
            sortBy: !!c.sortBy,
            width: c.width || "150px"
          };
        })
      ];
    },
    rows() {
      return this.items.map((v, i) => ({
        ...v,
        index: i + 1 + (this.page - 1) * this.itemsPerPage
      }));
    },
    listeners() {
      return {
        ...this.$listeners,
        toEdit: val => {
          this.$emit("toEdit", val);
        },
        input: val => this.$emit("input", val),
        "update:options": val => {
          this.$emit("loadList", val);
          this.page = val.page;
        },
        "click:row": row => {
          this.$emit("click:row", row);
          let index = this.value.findIndex(v => v == row);
          if (index > -1) {
            this.value.splice(index, 1);
          } else {
            this.value.push(row);
          }
        }
      };
    }
  },
  methods: {
    handleRowClick(props) {
      if (!this.multiple) {
        this.currentRow = props.item;
        this.$emit("input", [props.item]);
      } else {
        props.selected = !props.selected;
      }
    }
  }
};
</script>


<style>
.v-application--is-ltr .v-data-footer__select .v-select {
  margin: 0;
}
.v-data-footer__select
  .v-text-field
  > .v-input__control
  > .v-input__slot:before {
  border-style: none;
}
</style>
