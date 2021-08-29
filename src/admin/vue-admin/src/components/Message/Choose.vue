<template>
  <v-card>
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-card-text>
      <p v-if="text">{{ text }}</p>
      <template v-if="multiple">
        <v-checkbox
          v-model="value"
          v-for="n in values"
          :key="n.Key || n"
          :label="n.Value || n"
          :value="n.Key || n"
          hide-details
        ></v-checkbox>
      </template>
      <template v-else>
        <v-radio-group v-model="value" hide-details="">
          <v-radio
            v-for="n in values"
            :key="n.Key || n"
            :label="n.Value || n"
            :value="n.Key || n"
          ></v-radio>
        </v-radio-group>
      </template>
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="cancel">取消</v-btn>
      <v-spacer></v-spacer>
      <v-btn
        color="primary"
        text
        @click="success"
        :disabled="!value || value.length == 0"
        >确认
        <template v-if="timeoute">({{ val }})</template>
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  props: {
    title: String,
    text: String,
    values: Array,
    timeoute: Number,
    multiple: Boolean
  },
  data() {
    return {
      val: this.timeoute,
      value: this.multiple ? [] : null
    };
  },
  mounted() {
    if (this.timeoute) {
      this.interval = setInterval(() => {
        this.val -= 1;
        if (this.val <= 0) {
          this.cancel();
        }
      }, 1000);
    }
  },
  beforeDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
      this.interval = null;
    }
  },
  methods: {
    cancel() {
      this.$emit("close");
    },
    success() {
      this.$emit("success", this.value);
    }
  }
};
</script>

 