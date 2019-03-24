<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense card color="transparent">
            <v-toolbar-title>{{title}}</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text>
              <v-layout wrap>
                <Input
                  v-for="item in options"
                  :key="item.Name"
                  :model="form"
                  :rules="item.rules"
                  v-bind="item"
                  singleLine
                  canEdit
                  :errorMessages="formErrorMessages[item.Name]"
                  @change="changed=true"
                  @update_errorMessages="formErrorMessages[item.Name]=$event"
                  :ref="item.Name"
                />
              </v-layout>
              <v-divider class="mt-5"></v-divider>
            </v-card-text>
          </v-form>
          <v-card-actions>
            <v-btn flat @click="cancel">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" flat @click="success">确认</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Input from "@/components/Inputs";
export default {
  components: {
    Input
  },
  props: {
    options: Array,
    title: String,
    model: Object
  },
  data() {
    return {
      form: this.model,
      formErrorMessages: {}
    };
  },
  created() {
    if (!this.model) {
      let obj = {};
      for (const item of this.options) {
        obj[item.Name] = item.DefaultValue;
      }
      this.form = obj;
    }

    let formErrs = {};
    for (const name of Object.keys(this.form)) {
      formErrs[name] = [];
    }

    this.formErrorMessages = formErrs;
  },
  methods: {
    cancel() {
      this.$emit("close");
    },
    async success() {
      let errs = [];
      let names = Object.keys(this.form);
      for (const name of names) {
        if (
          this.$refs[name] &&
          this.$refs[name].length > 0 &&
          typeof this.$refs[name][0].evalRules == "function"
        ) {
          let err = await this.$refs[name][0].evalRules();
          errs.push(...err);
        }
      }
      if (errs.length > 0) {
        this.$eventBus.$emit("alert", {
          type: "error",
          msg: "表单填写不完整"
        });

        return;
      }
      this.$emit("success", this.form);
    }
  }
};
</script>

 