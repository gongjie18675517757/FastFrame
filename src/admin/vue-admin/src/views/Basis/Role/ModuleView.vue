<template>
  <div
    class="v-data-table v-data-table--dense v-data-table--fixed-header v-data-table--has-bottom theme--light"
  >
    <div class="v-data-table__wrapper" style="height: calc(50vh)">
      <table>
        <thead class="v-data-table-header">
          <tr>
            <th>#</th>
            <th>模块名</th>
            <th>所有权限</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(r, rIndex) in permissionList" :key="r.Name">
            <td>{{ rIndex + 1 }}</td>
            <td>{{ r.Text }}</td>
            <td>
              <v-checkbox
                v-for="v in r.Child"
                :key="v.Name"
                :label="v.Text"
                :input-value="hasCheck(v)"
                :disabled="disabled"
                @change="handleInput(v, $event)"
                hide-details
                dense
                style="display: inline-block; margin-top: 0px; padding-top: 0px"
              ></v-checkbox>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    value: Array,
    disabled: Boolean,
  },
  computed: {
    permissionList() {
      return this.$store.state.permissionList;
    },
  },
  methods: {
    hasCheck(v) {
      let { Name } = v;
      return this.value.includes(Name);
    },
    handleInput(v, val) {
      let { Name } = v;
      if (!val) {
        this.$emit(
          "input",
          this.value.filter((v) => v != Name)
        );
      } else {
        this.$emit("input", [...this.value, Name]);
      }
    },
  },
};
</script>

<style>
</style>