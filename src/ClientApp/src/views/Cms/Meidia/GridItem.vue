<template>
  <v-flex lg2 sm3 xs4 class="pa-2">
    <v-card flat tile class="grid-item">
      <v-responsive height="150px" width="150px" class="link">
        <v-icon size="135" class="mx-auto" color="indigo" v-if="item.IsFolder">folder</v-icon>
        <img :src="item.path" alt="" v-else-if="isImage">
        <v-icon class="mx-auto" size="135" v-else>insert_drive_file</v-icon>
      </v-responsive>
      <v-divider></v-divider>
      <v-card-title class="grid-item" @dblclick="reName">{{item.Name}}</v-card-title>
    </v-card>
  </v-flex>
</template>

<script>
import { showDialog, alert } from '@/utils'
import Prompt from '@/components/Message/Prompt.vue'
import rules from '@/rules'
export default {
  props: {
    item: {
      type: Object,
      default: function() {
        return {}
      }
    }
  },
  computed: {
    isImage() {
      if (this.item.Resource) return this.item.Resource.ContentType == 'image/jpeg'
    }
  },
  methods: {
    async reName() {
      let { name } = await showDialog(Prompt, {
        title: '文件夹名称',
        maxWidth: '500px',
        model: {
          name: this.item.Name
        },
        options: [
          {
            Name: 'name',
            Type: 'String',
            Description: '文件夹名称',
            rules: [rules.required('文件夹名称')]
          }
        ]
      })
      if (name == this.item.Name) return
      let beforeName = this.item.Name
      this.item.Name = name
      try {
        await this.$http.put('/api/Meidia/put', this.item)
        alert.success('更新成功!')
      } catch (error) {
        this.item.Name = beforeName
      }
    }
  }
}
</script>

<style scoped>
.grid-item {
  border-width: 1px;
  border-style: solid;
  border-color: #eee;
  text-align: center;
}
.link {
  cursor: pointer;
}
</style>
