<template>
  <v-card>
    <v-card-title class="indigo white--text headline">选择</v-card-title> 
    <v-card-text>
      <v-treeview 
        :items="items" 
        item-key="Id"
        item-text="Name"
        selectable
        v-model="model"
      > 
      </v-treeview>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      model: [],
      items: []
    }
  },
  async created() {
    let { Data: items } = await this.$http.post('/api/Permission/list', { pagesize: 999 })
    items.forEach(r => {
      r.selected = false
    })
    let loadTree = parentId => {
      return items.filter(r => r.Parent_Id == parentId).map(r => {
        return {
          ...r,
          children: loadTree(r.Id)
        }
      })
    }

    this.items = loadTree(null)
  }
}
</script>

<style>
</style>
