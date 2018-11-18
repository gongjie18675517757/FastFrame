<template>
  <v-autocomplete
    :loading="loading"
    :items="items"
    :search-input.sync="search"
    :filter="()=>true"
    v-model="select"
    clearable
    :label="label"
    :disabled="disabled"
    @change="change"
  >
    <template slot="no-data">
      <v-list-tile>
        <v-list-tile-title>
          请输入关键字,或者
          <strong>
            <a @click="openDialog">搜索</a>
          </strong>
        </v-list-tile-title>
      </v-list-tile>
    </template>
    <template slot="selection" slot-scope="{ item, selected }">
      <strong>{{item[fields[0]]}}</strong>
      <span v-for="(f,index) in fields" :key="index" v-if="index>0">[{{item[f]}}]</span>
    </template>
    <template slot="item" slot-scope="{ item, tile }">
      <v-list-tile-content>
        <v-list-tile-title v-text="item[fields[0]]"></v-list-tile-title>
        <v-list-tile-sub-title
          v-for="(f,index) in fields"
          :key="index"
          v-if="index>0"
          v-text="item[f]"
        ></v-list-tile-sub-title>
      </v-list-tile-content>
      <!-- <v-list-tile-action>
                <v-icon>mdi-coin</v-icon>
      </v-list-tile-action>-->
    </template>
  </v-autocomplete>
</template>

<script>
import { getModuleStrut } from '@/generate'
import { showDialog } from '@/utils'
export default {
  props: {
    model: Object,
    callback: {
      type: Function,
      default: function() {}
    },
    value: String,
    rules: {
      type: Array,
      default: function() {
        return []
      }
    },
    disabled: Boolean,
    label: String,

    Name: String,
    ModuleName: String,
    Relate: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      loading: false,
      items: [],
      search: null,
      select: null,
      fields: []
    }
  },
  async created() {
    let { RelateFields } = await getModuleStrut(this.Relate)
    this.fields = RelateFields
    if (this.value) {
      let nameTemp = this.Name.replace('_Id', '')
      let obj = this.model[nameTemp]
      this.items = [obj]
      this.select = obj
    }
  },
  watch: {
    search(v) {
      v && this.querySelections(v)
    }
  },
  methods: {
    async querySelections(v) {
      this.loading = true
      let { Data } = await this.$http.post(`/api/${this.Relate}/list`, {
        Condition: {
          Filters: [
            {
              Name: this.fields.join(';'),
              Compare: '$',
              Value: v
            }
          ]
        }
      })
      this.items = Data
      this.loading = false
    },
    change($event) {
      this.$emit('change', $event)
      this.$emit('input', $event.Id)
    },
    async openDialog() {
      let rows = await showDialog(`${this.Relate}_List`, { single: true })
      if (rows.length > 0) {
        this.items = rows
        this.select = rows[0]
        this.change(rows[0])
      }
    }
  }
}
</script>

<style>
</style>
