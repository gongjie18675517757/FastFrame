<template>
  <v-card>
    <v-toolbar card dense color="transparent">
      <v-toolbar-title>
        <h4>项目</h4>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>more_vert</v-icon>
      </v-btn>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text class="pa-0">
      <template>
        <v-data-table :headers="headers" :items="projects" hide-actions class="elevation-0">
          <template slot="items" slot-scope="props">
            <td>
              <v-avatar size="36px">
                <img :src="props.item.avatar" :alt="props.item.username">
              </v-avatar>
            </td>
            <td>{{ props.item.name }}</td>
            <td class="text-xs-left">{{ props.item.deadline }}</td>
            <td class="text-xs-left">
              <v-progress-linear :value="props.item.progress" height="5" :color="props.item.color"></v-progress-linear>
            </td>
            <td class="text-xs-right">
              <v-btn flat icon color="grey">
                <v-icon>edit</v-icon>
              </v-btn>
              <v-btn flat icon color="grey">
                <v-icon>delete</v-icon>
              </v-btn>
            </td>
          </template>
        </v-data-table>
      </template>
      <v-divider></v-divider>
    </v-card-text>
  </v-card>
</template>

<script>
import timg from '@/assets/timg.jpg'
export default {
  data() {
    return {
      headers: [
        {
          text: '',
          align: 'center',
          sortable: false,
          value: 'avatar'
        },
        {
          text: '名称',
          align: 'left',
          value: 'name'
        },
        { text: '时间', value: 'deadline' },
        { text: '进度', value: 'progress' },
        { text: '操作', value: 'action', align: 'right' }
      ]
    }
  },
  computed: {
    projects() {
      return [
        {
          username: 'Dessie',
          avatar:timg,
          name: 'Template PSD',
          deadline: '2 days later',
          progress: 90,
          color: 'pink'
        },
        {
          username: 'Jakayla',
          avatar:timg,
          name: 'Logo Design',
          deadline: '1 weeks later',
          progress: 70,
          color: 'success'
        },
        {
          username: 'Ludwiczakpawel',
          avatar:timg,
          name: 'REST API',
          deadline: '1 Month later',
          progress: 50,
          color: 'info'
        },
        {
          username: 'Damenleeturks',
          avatar:timg,
          name: 'API Unit Test',
          deadline: '2 Month later',
          progress: 30,
          color: 'teal'
        },
        {
          username: 'Caspergrl',
          avatar:timg,
          name: 'Project Deploy',
          deadline: 'half year later',
          progress: 15,
          color: 'grey'
        }
      ]
    }
  }
}
</script>
