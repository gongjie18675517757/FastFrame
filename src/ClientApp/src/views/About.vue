<template>
  <a @click="handleClick">测试</a>
</template>

<script>
import { showDialog } from '@/utils'
export default {
  methods: {
    async handleClick() {
      let component = () => import('@/views/Basis/User/List.vue')
      let selection = await showDialog(component, {
        single: true
      })
      console.log(selection)
    }
  }
}
</script>

<style>
</style>
