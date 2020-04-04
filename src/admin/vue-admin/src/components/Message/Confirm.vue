<template>
  <v-layout align-center justify-center>
    <v-flex xs6>
      <v-card>
        <v-toolbar flat dense   color="transparent">
          <v-toolbar-title>{{title}}</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-divider></v-divider>
        <v-card-text>
          <div v-html="content"></div>
          <v-divider class="mt-5"></v-divider>
        </v-card-text>
        <v-card-actions>
          <v-btn text @click="cancel">取消</v-btn>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="success">确认</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  props: {
    title: String,
    content: String
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

 