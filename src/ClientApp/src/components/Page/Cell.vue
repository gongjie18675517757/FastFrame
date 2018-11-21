<template>
  <span>
    <a-btn
      class="btn-link"
      v-if="info.IsLink"
      flat
      small
      color="primary"
      :moduleName="moduleName"
      name="Get"
      @click="$emit('toEdit')"
    >{{value}}</a-btn>
    <span v-else>{{value}}</span>
  </span>
</template>

<script>
export default {
  props: {
    info: {
      type: Object,
      default: function() {
        return {}
      }
    },
    model: {
      type: Object,
      default: function() {
        return {}
      }
    },
    moduleName: String
  },
  computed: {
    value() {
      let val = this.model[this.info.Name]
      if (this.info.Type == 'Boolean') {
        if (val) return '是'
        else return '否'
      }
      if (this.info.Relate) {
        let tempName = this.info.Name.replace('_Id', '')
        let name = this.info.Relate[0]
        if (this.model[tempName]) {
          return this.model[tempName][name]
        }
      }
      return val
    }
  },
  methods: {}
}
</script>

<style>
.btn-link .v-btn__content{
  font-size: 13px; 
}
</style>
