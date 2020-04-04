<template>
  <span>
    <span v-if="this.disabled && !isXs">{{this.value}}</span>
    <v-menu
      v-else
      v-model="menu2"
      :close-on-content-click="false"
      :nudge-right="0"
      lazy
      transition="scale-transition"
      offset-y
      full-width
      max-width="290px"
      min-width="290px"
      :readonly="disabled"
    >
      <template v-slot:activator="{ on }">
        <v-text-field
          :value="value"
          readonly
          v-on="on"
          :label="label"
          :errorMessages="errorMessages"
          :placeholder="description"
        ></v-text-field>
      </template>
      <v-date-picker
        :value="value"
        no-title
        @input="change"
        :header-date-format="format"
        :month-format="format"
        :weekday-format="weekdayFrm"
      ></v-date-picker>
    </v-menu>
  </span>
</template>

<script>
export default {
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
     isXs: Boolean
  },
  data() {
    return {
      menu2: false
    };
  },
  methods: {
    change(val) {
      this.$emit("input", val);
      this.$emit("change", val);
      this.menu2 = false;
    },
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

 
