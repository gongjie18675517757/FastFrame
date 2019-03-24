<template>
  <v-menu
    v-model="menu2"
    :close-on-content-click="false"
    :nudge-right="0"
    lazy
    transition="scale-transition"
    offset-y
    full-width
    max-width="290px"
    min-width="290px"
  >
    <template v-slot:activator="{ on }">
      <v-text-field v-model="date" readonly v-on="on"></v-text-field>
    </template>
    <v-date-picker
      v-model="date"
      no-title
      @input="menu2 = false"
      :header-date-format="format"
      :month-format="format"
      :weekday-format="weekdayFrm"
    ></v-date-picker>
  </v-menu>
</template>

<script>
export default {
  data() {
    return {
      date: null,
      menu2: false,
      items: []
    };
  },

  methods: {
    weekdayFrm(v) {
      v = new Date(v).getDay();
      return {
        0: "日",
        1: "一",
        2: "二",
        3: "三",
        4: "四",
        5: "五",
        6: "六"
      }[v];
    },
    format(v) {
      return v;
    }
  }
};
</script>

 