<template>
  <div class="v-text-field">
    <v-input dense :disabled="disabled" @click="handleInputClick">
      <template #default>
        <input type="text" :value="value" readonly :placeholder="description">
      </template>
      <template #prepend>
        <slot name="prepend"></slot>
      </template>
    </v-input>
    <v-menu
      :close-on-content-click="false"
      :disabled="disabled" 
      v-model="menuVisible"
      :position-x="x"
      :position-y="y" 
      
    >
      <DatePicker :value="value" @input="handleInput"/>
    </v-menu>
  </div>
</template>

<script>
import DatePicker from "v-calendar/lib/components/date-picker.umd";
export default {
  name:'v-date-input',
  components: {
    DatePicker,
  },
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean,
    type: {
      type: String,
      default: "date",
    },
  },
  data() {
    return {
      x: 0,
      y: 0,
      menuVisible:false
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
    },
  },
  methods: {
    handleInputClick() {
      if (this.disabled) {
        return;
      }
      let e = event;
      this.x = e.clientX;
      this.y = e.clientY;
      this.menuVisible = true;
    },
    handleInput(v){
      console.log(v,typeof(v));
    }
  },
};
</script>

 
