<template>
  <div id="pageDashboard">
    <v-container grid-list-xl fluid>
      <v-layout row wrap="">
        <v-flex lg3 sm6 xs12 v-for="(item,index) in miniStatistics" :key="index">
          <MiniStatistic v-bind="item"/>
        </v-flex>
        <v-flex lg4 sm12 xs12 v-for="(item,index) in trending" :key="'c-trending'+index">
          <CircleStatistic
            :title="item.subheading"
            :sub-title="item.headline"
            :caption="item.caption"
            :icon="item.icon.label"
            :color="item.linear.color"
            :value="item.linear.value"
          />
        </v-flex>
        <v-flex lg7 sm12 xs12>
          <plain-table></plain-table>
        </v-flex>
        <v-flex lg5 sm12 xs12>
          <plain-table-order></plain-table-order>
        </v-flex>     
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import MiniStatistic from './MiniStatistic.vue'
import CircleStatistic from './CircleStatistic.vue'
import PlainTable from './PlainTable.vue';
import PlainTableOrder from './PlainTableOrder.vue';

export default {
  components: {
    MiniStatistic,
    CircleStatistic,
    PlainTable,
    PlainTableOrder
  },
  data() {
    return {
      miniStatistics: [
        {
          icon: 'fa fa-facebook',
          title: '100+',
          subTitle: 'Likes',
          color: 'indigo'
        },
        {
          icon: 'fa fa-google',
          title: '150+',
          subTitle: 'Connections',
          color: 'red'
        },
        {
          icon: 'fa fa-twitter',
          title: '200+',
          subTitle: 'Followers',
          color: 'light-blue'
        },
        {
          icon: 'fa fa-instagram',
          title: '20+',
          subTitle: 'Shots',
          color: 'purple'
        }
      ],
      trending: [
        {
          subheading: 'Email',
          headline: '15+',
          caption: 'email opens',
          percent: 15,
          icon: {
            label: 'email',
            color: 'info'
          },
          linear: {
            value: 15,
            color: 'info'
          }
        },
        {
          subheading: 'Tasks',
          headline: '90%',
          caption: 'tasks completed.',
          percent: 90,
          icon: {
            label: 'list',
            color: 'primary'
          },
          linear: {
            value: 90,
            color: 'success'
          }
        },
        {
          subheading: 'Issues',
          headline: '100%',
          caption: 'issues fixed.',
          percent: 100,
          icon: {
            label: 'bug_report',
            color: 'primary'
          },
          linear: {
            value: 100,
            color: 'error'
          }
        }
      ]
    }
  }
}
</script>

<style>
</style>
