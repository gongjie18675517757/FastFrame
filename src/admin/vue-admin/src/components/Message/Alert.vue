<template>
  <v-layout align-center justify-center>
    <v-flex xs12>
      <v-card tile>
        <v-toolbar flat dense>
          <v-toolbar-title>{{title}}</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-divider></v-divider>
        <v-card-text>
          <div v-html="content"></div>
          <v-divider class="mt-5"></v-divider>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="success">确认({{timeValue}})</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  props: {
    title: String,
    content: String,
    color: {
      type: String,
      default: "info"
    },
    timeout: {
      type: Number,
      default: 6 * 1000
    }
  },
  data() {
    return {
      timeValue: parseInt(this.timeout / 1000)
    };
  },
  mounted() {
    this.interval = setInterval(() => {
      this.timeValue -= 1;
      if (this.timeValue <= 0) {
        this.interval = null;
        clearInterval(this.interval);
        this.success();
      }
    }, 1 * 1000);
  },
  beforeDestroy() {
    if (this.interval != null) {
      clearInterval(this.interval);
    }
  },
  methods: {
    success() {
      this.$emit("success");
    }
  }
};
</script>

 