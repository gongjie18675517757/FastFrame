<template>
  <v-card>
    <v-card-title class="indigo white--text headline">选择{{title}}</v-card-title>
    <v-card-text>
      <v-layout wrap="">
        <v-flex sm3 md2 xs4 v-for="item in items" :key="item.Id">
          <v-checkbox :label="labelFormatter(item)" v-model="selection[item.Id]"></v-checkbox>
        </v-flex>
      </v-layout>
    </v-card-text>
    <v-card-actions>
      <v-btn flat @click="$emit('close')">取消</v-btn>
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
    model: {
      type: Array,
      default: function() {
        return []
      }
    },
    labelFormatter: {
      type: Function,
      default: function(item) {
        return item.Name
      }
    }
  },
  data() {
    return {
      selection: {},
      items: []
    }
  },
  async created() {
    let { Data: items } = await this.$http.post(this.requestUrl, { pagesize: 999 })
    let obj = {}
    for (const item of items) {
      obj[item.Id] = false
    }
    for (const item of this.model) {
      if (obj.hasOwnProperty(item)) obj[item] = true
    }
    this.selection = obj
    this.items = items
  },
  methods: {
    success() {
      var keys = Object.keys(this.selection).filter(k => this.selection[k])
      this.$emit('success', keys)
    }
  }
}
</script>

 