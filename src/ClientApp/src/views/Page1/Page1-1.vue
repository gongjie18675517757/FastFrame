<template>
    <v-autocomplete
        :loading="loading"
        :items="items"
        :search-input.sync="search"
        :filter="()=>true"
        v-model="select"
        clearable
        hide-details
        hide-selected
        label="管理员"
    >
        <template slot="no-data">
            <v-list-tile>
                <v-list-tile-title>
                    请输入关键字
                    <strong>搜索</strong>
                </v-list-tile-title>
            </v-list-tile>
        </template>
        <template slot="selection" slot-scope="{ item, selected }">{{item.Name}}[{{item.Account}}]</template>
        <template slot="item" slot-scope="{ item, tile }">
            <v-list-tile-content>
                <v-list-tile-title v-text="item.Account"></v-list-tile-title>
                <v-list-tile-sub-title v-text="item.Name"></v-list-tile-sub-title>
            </v-list-tile-content>
            <!-- <v-list-tile-action>
                <v-icon>mdi-coin</v-icon>
            </v-list-tile-action>-->
        </template>
    </v-autocomplete>
</template>
 
<script>
export default {
  data: () => ({
    loading: false,
    items: [],
    search: null,
    select: null
  }),
  watch: {
    search(v) {
      v && this.querySelections(v)
    }
  },
  methods: {
    async querySelections(v) {
      this.loading = true
      let { Data } = await this.$http.post('/api/user/list', {
        Condition: {
          Filters: [
            {
              Name: 'Account;Name',
              Compare: '$',
              Value: v
            }
          ]
        }
      })
      this.items = Data
      this.loading = false
    }
  }
}
</script>

<style>
</style>
