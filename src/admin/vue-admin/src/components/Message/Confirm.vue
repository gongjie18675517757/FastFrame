<template>
  <v-card>
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-card-text>
      <v-divider class="mb-5"></v-divider>
      <div v-html="content"></div>
      <br />
      <v-divider class="mt-5"></v-divider>
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="cancel">取消</v-btn>
      <v-spacer></v-spacer>
      <v-btn color="primary" text @click="success"
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
    content: String,
    timeoute: Number
  },
  data() {
    return {
      val: this.timeoute
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
      this.$emit("success");
    }
  }
};
</script>

 