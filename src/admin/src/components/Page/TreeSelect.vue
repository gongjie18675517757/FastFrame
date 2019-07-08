<template>
  <v-card>
    <v-card-title class="indigo white--text headline">选择{{title}}</v-card-title>
    <v-divider></v-divider>
    <v-card-text>
      <v-treeview
        :items="items"
        class="grey lighten-5"
        selectable
        transition
        :item-key="treeKey"
        :item-text="treeText"
        v-model="selection"
      ></v-treeview>
    </v-card-text>
    <v-card-actions>
      <v-btn flat @click="cancel">取消</v-btn>
      <v-spacer></v-spacer>
      <v-btn color="primary" flat @click="success">保存</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    title: String,
    requestUrl: String,
    requestPars: {
      type: Object,
      default: function() {
        return {
          pageSize: 9999
        };
      }
    },
    parentKey: {
      default: "Super_Id",
      type: String
    },
    treeKey: {
      type: String,
      default: "Id"
    },
    treeText: {
      type: String,
      default: "Name"
    },
    model: {
      type: Array,
      default: function() {
        return [];
      }
    },
    formatter: {
      type: Function,
      default: function(items) {
        return items;
      }
    }
  },
  data() {
    return {
      selection: [],
      items: [],
      allItems: []
    };
  },
  async created() {
    let { Data: items } = await this.$http.post(
      this.requestUrl,
      this.requestPars
    );
    this.allItems = [...items];
    items = this.formatter(items);
    let loadTree = parentId => {
      return items
        .filter(r => r[this.parentKey] == parentId)
        .map(r => {
          return {
            ...r,
            children: loadTree(r.Id)
          };
        });
    };

    this.items = loadTree(null);
    this.selection = [...this.model];
  },
  methods: {
    cancel() {
      this.$emit("close");
    },
    success() {
      this.$emit("success",this.allItems.filter(v=>this.selection.includes(v[this.treeKey])));
    }
  }
};
</script>

 