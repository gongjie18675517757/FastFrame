<template>
  <v-flex xs12>
    <v-card>
      <v-toolbar flat dense card color="transparent">
        <v-toolbar-title>{{title}}</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-divider></v-divider>
      <v-form ref="form">
        <v-card-text>
          <v-layout wrap="">
            <TextInput
              v-for="item in options"
              :key="item.Name"
              :model="form"
              v-bind="item"
              canEdit
              flex="xs12"
            />
          </v-layout>
          <v-divider class="mt-5"></v-divider>
        </v-card-text>
      </v-form>
      <v-card-actions>
        <v-btn flat @click="cancel">取消</v-btn>
        <v-spacer></v-spacer>
        <v-btn color="primary" flat @click="success">保存</v-btn>
      </v-card-actions>
    </v-card>
  </v-flex>
</template>

<script>
import TextInput from '@/components/Inputs/TextInput.vue'
export default {
  components: {
    TextInput
  },
  props: {
    options: Array,
    title: String,
    model: Object
  },
  data() {
    return {
      form: this.model
    }
  },
  created() {
    if (!this.model) {
      let obj = {}
      for (const item of this.options) {
        obj[item.Name] = item.DefaultValue
      }
      this.form = obj
    }
  },
  methods: {
    cancel() {
      this.$emit('close')
    },
    success() {
      if (this.$refs.form.validate()) {
        this.$emit('success', this.form)
      }
    }
  }
}
</script>

<style>
</style>
