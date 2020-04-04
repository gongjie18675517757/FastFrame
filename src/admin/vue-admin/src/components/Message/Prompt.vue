<template>
  <v-container grid-list-xl fluid app>
    <v-layout align-center justify-center>
      <v-flex xs12>
        <v-card>
          <v-toolbar flat dense   color="transparent">
            <v-toolbar-title>{{title}}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-btn icon @click="cancel">
              <v-icon>close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-divider></v-divider>
          <v-form ref="form">
            <v-card-text>
              <div class="dialog-form-content">
                <v-layout wrap>
                  <Input
                    v-for="item in options"
                    :key="item.Name"
                    :model="form"
                    v-bind="item"
                    singleLine
                    canEdit
                    :errorMessages="formErrorMessages[item.Name]"
                    @change="handleChange(item)"
                  />
                </v-layout>
              </div>
            </v-card-text>
          </v-form>
          <v-divider></v-divider>
          <v-card-actions>
            <v-btn text @click="cancel">取消</v-btn>
            <v-spacer></v-spacer>
            <v-btn color="primary" text @click="success">确认</v-btn>
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
    model: Object,
    rules: {
      type: Object,
      default: () => ({})
    }
  },
  data() {
    return {
      form: this.model,
      formErrorMessages: {}
    };
  },
  created() {
    this.form = this.model;

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
    handleChange(item) {
      this.changed = true;
      this.evalRule(item);
    },
    evalRule(item) {
      let rules = this.rules[item.Name] || item.rules;
      let val = this.form[item.Name];
      this.formErrorMessages[item.Name] = [];

      let promiseArr = rules.map(v => v.call(this.form, val));
      return Promise.all(promiseArr)
        .then(arr => {
          return arr.filter(v => typeof v == "string");
        })
        .then(errs => {
          this.formErrorMessages[item.Name].push(...errs);
          return errs;
        });
    },
    success() {
      let promiseArr = this.options.map(v => this.evalRule(v));
      Promise.all(promiseArr).then(errs => {
        errs = errs.filter(v => v.length > 0);
        if (errs.length == 0) {
          this.$emit("success", this.form);
        }
      });
    }
  }
};
</script>
<style>
.dialog-form-content {
  max-height: 50vh;
}
</style>


 