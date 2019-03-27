<template>
  <v-data-table
    :headers="headers"
    :loading="loading"
    :items="items"
    :total-items="totalItems"
    :pagination.sync="pager"
    :hide-actions="hidePager"
    :value="value"
    @input="$emit('input',$event)"
    @update:pagination="$emit('loadList',pager)"
    :item-key="rowKey"
    class="elevation-1 fixed-header v-table__overflow"
    style="max-height: calc(100vh - 140px);backface-visibility: hidden;"
  >
    <template slot="items" slot-scope="props">
      <tr :active="props.selected" @click="handleRowClick(props)">
        <td>
          <v-icon small size="16" color="primary" v-if="!multiple && currentRow==props.item">check</v-icon>
          <v-checkbox v-if="multiple" primary hide-details :value="props.selected"></v-checkbox>
        </td>
        <td v-for="col in columns" :key="col.Name">
          <Cell :info="col" :model="props.item"/>
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
    height: [String, Number],
    totalItems: Number,
    multiple: Boolean,
    hidePager: {
      type: Boolean,
      default: true
    },
    loading: Boolean
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
        {
          text: "#",
          sortable: false,
          width: "50px"
        },
        ...this.columns.map(c => {
          return {
            text: c.Description,
            value: c.Name,
            sortBy: !!c.sortBy
          };
        })
      ];
    },
    style() {
      if (this.height) {
        let val = this.height;
        if (typeof val == "string")
          return {
            "max-height": val,
            "backface-visibility": "hidden"
          };
        else if (typeof val == "number")
          return {
            "max-height": `${val}px`,
            "backface-visibility": "hidden"
          };
      } else {
        return {
          "max-height": `calc(100vh - 140px)`,
          "backface-visibility": "hidden"
        };
      }
    },
    class() {}
  },
  methods: {
    handleRowClick(props) {
      if (!this.multiple) {
        this.currentRow = props.item;
        this.$emit("input", [props.item]);
      } else {
        props.selected = !props.selected;
        // this.$emit("input", this.selection);
      }
    }
  }
};
</script>