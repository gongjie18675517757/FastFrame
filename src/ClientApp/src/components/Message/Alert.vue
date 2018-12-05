<template>
  <v-layout align-center justify-center>
    <v-flex xs6>
      <v-card>
        <v-toolbar flat dense card color="transparent">
          <v-toolbar-title>{{title}}</v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>
        <v-divider></v-divider>
        <v-card-text>
          {{content}}
          <v-divider class="mt-5"></v-divider>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" flat @click="success">确认</v-btn>
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
    success() {
      this.$emit("success");
    }
  }
};
</script>

<style>
</style>
