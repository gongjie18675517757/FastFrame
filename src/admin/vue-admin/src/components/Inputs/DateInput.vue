<template>
  <span>
    <span v-if="this.disabled && !isXs">{{this.value}}</span>
    <v-text-field
      v-else
      :value="text"
      readonly
      :label="label"
      :errorMessages="errorMessages"
      :placeholder="description"
      dense
      @click="handleInputClick"
    >
      <template #default>
        <slot></slot>
      </template>
      <template #prepend>
        <slot name="prepend"></slot>
      </template>
    </v-text-field>
    <v-menu
      v-model="showDatePicker"
      :close-on-content-click="false"
      :nudge-right="0"
      transition="scale-transition"
      offset-y
      max-width="290px"
      min-width="290px"
      :position-x="x"
      :position-y="y"
      absolute
    >
      <v-date-picker
        :value="dateValue"
        @change="dateChange"
        :header-date-format="format"
        :month-format="format"
        :weekday-format="weekdayFrm"
      ></v-date-picker>
    </v-menu>
    <v-menu
      v-model="showTimePicker"
      :close-on-content-click="false"
      :nudge-right="0"
      transition="scale-transition"
      offset-y
      max-width="290px"
      min-width="290px"
      :position-x="x"
      :position-y="y"
      absolute
    >
      <v-time-picker :value="timeValue" @change="timeChange" format="24hr"></v-time-picker>
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
    isXs: Boolean,
    type: {
      type: String,
      default: "date"
    }
  },
  data() {
    return {
      x: 0,
      y: 0,
      showDatePicker: false,
      showTimePicker: false,
      tempDateValue: null
    };
  },
  computed: {
    dateValue() {
      if (!this.value) {
        return null;
      } else {
        return this.value.substring(0, 10);
      }
    },
    timeValue() {
      if (!this.value || this.type != "datetime") {
        return null;
      } else { 
        return this.value.substring(11, 16);
      }
    },
    text() {
      if (this.value && this.type == "datetime")
        return `${this.dateValue || ""} ${this.timeValue || ""}`;
      else if (this.value) return this.dateValue;
      else return null;
    }
  },
  methods: {
    handleInputClick() {
      if (this.disabled) {
        return;
      }
      let e = event;
      this.x = e.clientX;
      this.y = e.clientY;
      this.showDatePicker = true;
    },
    dateChange(val) {
      this.showDatePicker = false;
      if (this.type == "date") {
        this.$emit("input", val);
        this.$emit("change", val);
      } else {
        this.tempDateValue = val;
        this.showTimePicker = true;
      }
    },
    timeChange(val) {
      val = `${this.tempDateValue} ${val}`;
      this.$emit("input", val);
      this.$emit("change", val);
      this.showTimePicker = false;
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

 
