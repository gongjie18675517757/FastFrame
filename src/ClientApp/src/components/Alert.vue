<template>
  <div>
    <v-snackbar
      v-for="(item,index) in alerts"
      :key="item.key"
      :value="true"
      :color="item.type"
      :timeout="item.timeout"
      bottom
      multi-line
      right
      vertical
      absolute
    >
      {{ item.msg }}
      <v-btn color="pink" flat @click="alerts.splice(index,1)">
        关闭
      </v-btn>
    </v-snackbar>
  </div>
</template>

<script>
export default {
  data() {
    return {
      alerts: []
    }
  },
  created() {
    function S4() {
      return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
    }
    function guid() {
      return (
        S4() +
        S4() +
        '-' +
        S4() +
        '-' +
        S4() +
        '-' +
        S4() +
        '-' +
        S4() +
        S4() +
        S4()
      )
    }

    let self = this
    this.$eventBus.$on('alert', function({
      type = 'success',
      msg = '',
      timeout = 3000
    }) {
      let item = {
        type,
        msg,
        timeout,
        key: guid()
      }

      self.alerts.push(item)
      setTimeout(
        function() {
          let key = arguments[0]
          let index = self.alerts.findIndex(item => item.key == key)
          self.alerts.splice(index, 1)
        },
        timeout + 100,
        item.key
      )
    })
  }
}
</script>

 
