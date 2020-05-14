<template>
  <v-data-table
    :fixed-header="!!height"
    :height="height"
    :headers="headers"
    :loading="loading"
    :items="rows"
    :server-items-length="totalItems"
    :footer-props="pager"
    :hide-default-footer="hidePager || isXs"
    :value="value"
    v-on="listeners"
    :item-key="rowKey"
    :items-per-page.sync="itemsPerPage"
    :class="[...classArr]"
    :style="styleObj"
    :show-select="!isXs && multiple"
    :single-select="isXs || !multiple"
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
    <template v-slot:body.append="{ pagination,options}" v-if="!hidePager && isXs">
      <v-divider />
      <v-btn
        v-if="pagination.pageCount>pagination.page"
        text
        color="primary"
        block
        @click="$emit('loadMoreList',options)"
      >下一页</v-btn>
      <p v-else style="height:50px;line-height:50px;text-align: center;color: #dababa;">数据已全部加载完成</p>
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
        "items-per-page-options": [10, 15, 20, 30, 50]
      }
    };
  },
  computed: {
    isXs() {
      return this.$vuetify.breakpoint.xs;
    },
    headers() {
      return [
        ...(this.expandComponent
          ? [{ text: "", width: "20px", value: "data-table-expand" }]
          : []),
        ...(this.isXs
          ? []
          : [
              {
                text: "#",
                value: "index",
                sortable: false,
                width: "50px"
              }
            ]),

        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name,
            sortBy: this.isXs ? false : !!c.sortBy,
            width: c.width || "100px",
            align: c.align || "center"
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

.v-data-table-header-mobile {
  display: none;
}

.v-data-table th {
  font-size: 12px;
  padding: 0 5px;
}
.v-data-table td {
  font-size: 12px;
  padding: 0 5px;
}
</style>
