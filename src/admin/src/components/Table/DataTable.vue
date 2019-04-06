<template>
  <v-data-table
    :headers="headers"
    :loading="loading"
    :items="items"
    :total-items="totalItems"
    :pagination.sync="pager"
    :hide-actions="hidePager"
    :value="value"
    v-on="listeners"
    :item-key="rowKey"
    :class="classArr"
    :style="styleObj"
    :hide-headers="false"
    :selectAll="multiple"
  >
    <template slot="items" slot-scope="props">
      <tr :active="props.selected" @click="handleRowClick(props)">
        <td>
          <v-icon small size="16" color="primary" v-if="!multiple && currentRow==props.item">check</v-icon>
          <v-checkbox v-if="multiple" primary hide-details :value="props.selected"></v-checkbox>
        </td>
        <td v-for="col in columns" :key="col.Name">
          <component
            v-if="col.component"
            :is="col.component"
            :info="col"
            :model="props.item"
            v-on="listeners"
          />
          <Cell v-else :info="col" :model="props.item" v-on="listeners"/>
        </td>
      </tr>
    </template>
    <template slot="no-data">没有加载数据</template>
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
    totalItems: Number,
    multiple: Boolean,
    hidePager: {
      type: Boolean,
      default: true
    },
    loading: Boolean,
    classArr: Array,
    styleObj: Object
  },
  data() {
    return {
      pager: {},
      currentRow: null
    };
  },
  computed: {
    headers() {
      return [
        ...(this.multiple
          ? []
          : [
              {
                text: "#",
                sortable: false,
                width: "50px"
              }
            ]),
        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name,
            sortBy: !!c.sortBy
          };
        })
      ];
    },
    listeners() {
      return {
        ...this.$listeners,
        toEdit: val => {
          this.$emit("toEdit", val);
        },
        input: val => this.$emit("input", val),
        "update:pagination": val => this.$emit("loadList", val)
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
/* table th {
  border: 1px solid #fff;
} */
</style>
