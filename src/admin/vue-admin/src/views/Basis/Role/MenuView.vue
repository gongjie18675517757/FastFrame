<template>
  <v-row class="pa-4" justify="space-between">
    <v-col cols="5" v-if="menuList">

      <!-- activatable -->
      <v-treeview
        :items="menuList"         
        open-on-click
        open-all
        dense
        item-text="title"
        item-key="permission"
        item-children="items"
        style="height: 50vh; overflow: auto"
      >
        <template v-slot:prepend="{ item }">
          <v-checkbox
            v-if="item.permission"
            :input-value="hasCheck(item.permission)"
            :readonly="disabled"
            @change="handleTreeInput(item, $event)"
            hide-details
            dense
            style="display: inline-block; margin-top: 0px; padding-top: 0px"
          ></v-checkbox>
        </template>
      </v-treeview>
    </v-col>

    <v-divider vertical></v-divider>

    <v-col class="d-flex text-center">
      <div
        class="v-data-table v-data-table--dense v-data-table--fixed-header v-data-table--has-bottom theme--light"
        style="width: 100%"
      >
        <div class="v-data-table__wrapper" style="height: calc(50vh)">
          <table>
            <thead class="v-data-table-header">
              <tr>
                <th>#</th>
                <th>菜单名</th>
                <th>所有权限</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(r, rIndex) in selection" :key="r.permission">
                <td>{{ rIndex + 1 }}</td>
                <td>{{ r.title }}</td>
                <td>
                  <v-checkbox
                    v-for="v in r.childPermission"
                    :key="v"
                    :label="getPermissionText(v)"
                    :input-value="hasCheck(v)"
                    :readonly="disabled"
                    @change="handleInput(v, $event)"
                    hide-details
                    dense
                    style="
                      display: inline-block;
                      margin-top: 0px;
                      padding-top: 0px;
                    "
                  ></v-checkbox>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </v-col>
  </v-row>
</template>

<script>
import menu from "../../../store/menu";
export default {
  props: {
    value: Array,
    disabled: Boolean,
  },
  data() {
    return {
      menuList: menu,
      active: null,
    };
  },
  computed: {
    permissionList() {
      return this.$store.state.permissionList;
    },
    selection() {
      function findChild(arr = [], resultArr = [], func) {
        for (const item of arr) {
          let val = func(item);

          if (val) {
            resultArr.push(item);
          }
          if (item.items && item.items.length > 0) {
            findChild(item.items, resultArr, func);
          }
        }
      }

      try {
        let valArr = [];
        findChild(menu, valArr, (v) => this.value.includes(v.permission));
        return valArr;
      } catch (error) {
        console.log(error);
        return [];
      }
    },
  },
  methods: {
    getPermissionText(name) {
      for (const v of this.$store.state.permissionList) {
        for (const r of v.Child) {
          if (r.Name == name) return r.Text;
        }
      }
      return null;
    },
    hasCheck(Name) {
      return this.value.includes(Name);
    },
    handleTreeInput(item, val) {
      let { permission, childPermission = [] } = item;
      let arr = [...childPermission, permission];
      if (!val) {
        this.$emit(
          "input",
          this.value.filter((v) => !arr.includes(v))
        );
      } else {
        arr = arr.filter((v) => !this.value.includes(v));
        this.$emit("input", [...this.value, ...arr]);
      }
    },
    handleInput(Name, val) {
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