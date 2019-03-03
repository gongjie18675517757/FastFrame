<template>
  <Table v-bind="tablePars"/>
</template>

<script>
import Table from "@/components/Table/DataTable.vue";
import { generateArray } from "@/utils";
export default {
  components: {
    Table
  },
  data() {
    return {
      tablePars: {
        columns: generateArray(10).map((item, index) => {
          return {
            Name: `Name${index}`,
            Description: `Description${index}`
          };
        })
      }
    };
  }
};
</script>

<style>
</style>
