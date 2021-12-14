<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center>
      <v-flex xs12 md8 xl6>
        <v-card>
          <v-toolbar flat dense color="transparent" height="30px">
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-spacer></v-spacer>

            <v-btn icon @click="cancel" title="关闭">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>

          <v-card-text v-if="model" style="min-width: 600px; padding: 0px">
            <VuePerfectScrollbar class="query-dialog-page">
              <div v-for="(arr, arrIndex) in model.Value" :key="arrIndex">
                <v-subheader>
                  查询组{{ arrIndex + 1 }}
                  <v-btn
                    icon
                    v-if="model.Value.length > 1"
                    @click="removeGroup(i)"
                  >
                    <v-icon color="info">clear</v-icon>
                  </v-btn>
                </v-subheader>
                <v-layout wrap style="padding-right: 10px">
                  <Input
                    v-for="item in arr"
                    v-bind="item"
                    :value="item.value"
                    :key="item.Description"
                    :flex="{ xs6: true }"
                    @input="handleInput(item, $event)"
                    canEdit
                  />
                </v-layout>
              </div>
              <br />
              <br />
              <v-btn
                @click="addQueryOption('and')"
                title="添加组"
                block
                tile
                color="primary"
              >
                <v-icon left>add</v-icon>添加查询组
              </v-btn>
              <br />
              <br />
            </VuePerfectScrollbar>
          </v-card-text>

          <v-card-actions>
            <v-btn text @click="cancel">取消</v-btn>
            <v-btn @click="refresh" color="warning" text> 重置 </v-btn>
            <v-spacer></v-spacer>
            <v-radio-group v-if="model && model.Value.length>1" v-model="model.Key" row label="组合关系">
              <v-radio value="and" label="且"></v-radio>
              <v-radio value="or" label="或"></v-radio>
            </v-radio-group>
             <v-spacer></v-spacer>
            <v-btn color="primary" @click="query">查询</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import Input from "@/components/Inputs";
export default {
  components: { VuePerfectScrollbar, Input },
  props: {
    title: String,
    options: Object,
    makeOptionsFunc: Function,
  },
  data() {
    return {
      conds: {
        and: "且",
        or: "或",
      },
      model: null,
      groupValues: this.options.Value.map((_, i) => i),
    };
  },
  mounted() {
 
    this.model = {
      Key: this.options.Key,
      Value: this.options.Value.map((v) =>
        v.map((x) => ({
          ...x,
          value: x.value ? JSON.parse(JSON.stringify(x.value)) : null,
        }))
      ),
    };
  },
  methods: {
    removeGroup(index) {
      this.model.Value.splice(index, 1);
    },
    addQueryOption() {
      this.model.Value.push(this.makeOptionsFunc());
    },
    handleInput(item, val) {
      item.value = val;
    },

    refresh() {
      this.model = {
        Key: "and",
        Value: [this.makeOptionsFunc()],
      };
    },
    query() {
      this.$emit("success", this.model);
    },
    cancel() {
      this.$emit("close");
    },
  },
};
</script>

 

<style lang="stylus">
.query-dialog-page {
  height: calc(100vh - 265px);
  overflow: auto;
  padding: 12px;
  min-width: 500px;

  .v-text-field__details {
    display: none;
  }
}
</style>
