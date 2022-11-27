<template>
  <v-row align="center" no-gutters>
    <v-col cols="12" >
      <v-menu offset-y v-if="can_append">
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="primary" v-bind="attrs" v-on="on">添加查询项</v-btn>
        </template>
        <v-list dense>
          <v-list-item
            v-for="(append_item, index) in can_append_query_arr"
            :key="index"
            @click="value.push(append_item)"
          >
            <v-list-item-title>
              {{ append_item.Description }}
              <!-- {{ compare_dic[append_item.compare] || append_item.compare }} -->
            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-col>
    <v-col
      v-for="item in value.filter((v) => v.Name && v.compare)"
      :key="item.Description"
      :cols="item.cols || 6"
    >
      <Input v-bind="item" v-model="item.value" :flex="{ xs6: true }" canEdit />
    </v-col>

    <v-col cols="12">
      <br />
      <v-btn color="primary" @click="addChild" block>添加子条件组</v-btn>
    </v-col>
    <v-col
      :cols="12"
      v-for="(item, item_index) in value.filter(
        (v) => v.ComposeMode && v.QueryFilters
      )"
      :key="item.Description"
    >
      <fieldset>
        <legend>
          子条件组({{ item_index + 1 }}):
          <v-radio-group v-model="item.ComposeMode" row label="下级查询项关系">
            <v-radio value="and" label="且"></v-radio>
            <v-radio value="or" label="或"></v-radio>
          </v-radio-group>
          <small> 子条件组与同级条件关系为and </small>
          <v-btn color="primary" small text @click="remove_child(item)">
            <v-icon>delete</v-icon>
            移除此子条件组
          </v-btn>
        </legend>
        <v-card-text>
          <query-filter-collection
            :value="item.QueryFilters"
            :makeOptionsFunc="makeOptionsFunc"
          />
        </v-card-text>
      </fieldset>
    </v-col>
  </v-row>
</template>

<script>
import Input from "@/components/Inputs";
import { take } from "../../utils";
export default {
  name: "query-filter-collection",
  components: {
    Input,
  },
  props: {
    value: Array,
    makeOptionsFunc: Function,
  },
  data() {
    return {
      conds: {
        and: "且",
        or: "或",
      },
      default_arr: this.makeOptionsFunc(),
      compare_dic: {
        $: "包含",
        in: "包含",
      },
    };
  },
  computed: {
    can_append() {
      return this.can_append_query_arr.length > 0;
    },
    can_append_query_arr() {
      return this.default_arr.filter(
        (v) =>
          !this.value.some((x) => x.Name == v.Name && x.compare == v.compare)
      );
    },
  },
  methods: {
    addChild() {
      this.value.push({
        ComposeMode: "and",
        QueryFilters: take(this.makeOptionsFunc(), 1),
      });
    },
    async remove_child(v) {
      await this.$message.confirm({
        title: "提示",
        content: "确认要移除吗?",
      });
      this.value.splice(
        this.value.findIndex((x) => x == v),
        1
      );
    },
  },
};
</script>

<style lang="stylus">
fieldset {
  padding: 10px;
  margin: 10px;
  width: 960px;
  color: #333;
  border: #06c dashed 1px;
}

legend {
  border: 0;
  color: inherit;
  display: flex;
  max-width: 100%;
  white-space: normal;
  max-width: 100%;
  align-items: center;
  padding: 5px 12px;
  justify-content: space-around;
}
</style>