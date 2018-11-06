<template>
  <v-card>
    <v-toolbar card dense color="transparent">
      <v-toolbar-title>
        <h4>订单</h4>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>more_vert</v-icon>
      </v-btn>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text class="pa-0">
      <template>
        <v-data-table
          :headers="headers"
          :items="items"
          hide-actions
          class="elevation-0 table-striped"
        >
          <template slot="items" slot-scope="props">
            <td>{{ props.item.id }}</td>
            <td class="text-xs-left">{{ props.item.product }}</td>
            <td class="text-xs-left">{{ props.item.price }}</td>
            <td class="text-xs-left">
              <v-chip
                label=""
                small
                :color="getColorByStatus(props.item.status)"
                text-color="white"
              >{{ props.item.status }}</v-chip>
            </td>
          </template>
        </v-data-table>
      </template>
      <v-divider></v-divider>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      headers: [
        {
          text: '#',
          align: 'left',
          sortable: false,
          value: 'id'
        },
        { text: '产品', value: 'deadline' },
        { text: '价格', value: 'progress' },
        { text: '状态', value: 'status' }
      ],
      items: [
        {
          id: '150',
          product: 'iPhone6',
          price: '$699',
          status: 'processing'
        },
        {
          id: '151',
          product: 'iPad Pro',
          price: '$299',
          status: 'sent'
        },
        {
          id: '300',
          product: 'Microsoft surface',
          price: '$1,699',
          status: 'processing'
        },
        {
          id: '320',
          product: 'Galaxy S7 edge',
          price: '$729',
          status: 'processing'
        },
        {
          id: '501',
          product: '128G SD Card',
          price: '$699',
          status: 'delivered'
        }
      ],
      colors: {
        processing: 'blue',
        sent: 'red',
        delivered: 'green'
      }
    }
  },
  computed: {
    randomColor() {
      let item = Math.floor(Math.random() * this.colors.length)
      return this.colors[item]
    }
  },
  methods: {
    getColorByStatus(status) {
      return this.colors[status]
    }
  }
}
</script>
