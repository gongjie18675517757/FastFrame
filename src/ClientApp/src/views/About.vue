<template>
  <a @click="handleClick">测试</a>
</template>

<script>
export default {
  methods: {
    handleClick() {
      let key = new Date().getTime()
      let component = () => import('@/views/Basis/User/List.vue')
      let render = {
        render: h => {
          return h(
            'v-dialog',
            {
              props: {
                persistent: true,
                scrollable: true,
                value: true
              }
            },
            [
              h(component, {
                ref: 'list'
              })
            ]
          )
        }
      }

      this.$store.commit({
        type: 'showDialog',
        render,
        key
      })
    }
  }
}
</script>

<style>
</style>
