<template>
  <v-data-table
    :headers="headers"
    :loading="loading"
    :items="items"
    :total-items="total"
    :pagination.sync="pager"
    :hide-actions="hidePager"
    v-model="selection"
    @update:pagination="pagerChangeHandle"
    :item-key="rowKey"
  >
    <template slot="items" slot-scope="props">
      <tr :active="props.selected" @click="handleRowClick(props)">
        <td>
          <v-icon small size="16" color="primary" v-if="!multiple && currentRow==props.item">check</v-icon>
          <v-checkbox v-if="multiple" primary hide-details v-model="props.selected"></v-checkbox>
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
    multiple: Boolean,
    hidePager: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      loading: false,
      total: 0,
      pager: {},
      selection: []
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
    }
  },
  methods: {
    handleRowClick() {},
    pagerChangeHandle(){
      
    }
  }
};
</script>