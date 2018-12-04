<template>
  <v-flex v-bind="formItemFlex" v-if="evalType">
    <v-text-field
      v-if="evalType=='text' || evalType=='password'"
      v-model="value"
      :rules="rules"
      :label="Description"
      :error-messages="ErrorMessage"
      :type="evalType"
      :disabled="evalDisabled"
      @change="change"
    ></v-text-field>
    <v-checkbox
      v-if="evalType=='Boolean'"
      v-model="value"
      :rules="rules"
      :label="Description"
      :error-messages="ErrorMessage"
      :disabled="evalDisabled"
      @change="change"
    ></v-checkbox>
    <SearchInput
      v-if="evalType=='remoteSelect'"
      v-model="value"
      :Name="Name"
      :model="model"
      :disabled="evalDisabled"
      :label="Description"
      :rules="rules"
      :Relate="Relate"
      :filter="filter"
      @change="change"
    />
    <v-textarea
      v-if="evalType=='textArea'"
      v-model="value"
      :Name="Name"
      :model="model"
      :disabled="evalDisabled"
      :label="Description"
      :rules="rules"
      :Relate="Relate"
      :filter="filter"
      @change="change"
      auto-grow
    ></v-textarea>
    <RichInput
      v-if="evalType=='richText'"
      v-model="value"
      :Name="Name"
      :model="model"
      :disabled="evalDisabled"
      :label="Description"
      :rules="rules"
      :Relate="Relate"
      :filter="filter"
      @change="change"
    />
  </v-flex>
</template>

<script>
import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";

export default {
  components: {
    SearchInput,
    RichInput
  },
  props: {
    model: {
      type: Object,
      default: function() {
        return {};
      }
    },
    callback: {
      type: Function,
      default: function() {}
    },
    rules: {
      type: Array,
      default: function() {
        return [];
      }
    },
    canEdit: Boolean,
    IsTextArea: Boolean,
    IsRichText: Boolean,
    ModuleName: String,
    Relate: String,
    Type: {
      type: String,
      default: "text"
    },
    Description: String,
    Name: String,
    Readonly: String,
    filter: [Array, Function],
    flex: [Object, String]
  },
  data() {
    return {
      ErrorMessage: []
    };
  },
  computed: {
    value: {
      get() {
        let temp = this.model;
        return eval(`temp.${this.Name}`);
      },
      set(val) {
        let temp = this.model;
        eval(`temp.${this.Name}=val`);
      }
    },
    formItemFlex() {
      if (this.IsTextArea || this.IsRichText) {
        return {
          xs12: ""
        };
      }

      if (typeof this.flex == "string") {
        let obj = {};
        obj[this.flex] = "";
        return obj;
      }
      if (typeof this.flex == "object") {
        return this.flex;
      }

      return {
        xs12: "",
        sm4: ""
      };
    },
    evalType() {
      if (this.IsTextArea) {
        return "textArea";
      }
      if (this.IsRichText) {
        return "richText";
      }
      if (this.Name == "Password") {
        return "password";
      }
      if (this.Relate) {
        return "remoteSelect";
      }
      if (this.Name.endsWith("Id")) {
        return;
      }
      if (this.Type == "String") {
        return "text";
      }

      return ""; //this.Type
    },
    evalDisabled() {
      if (this.Readonly == "all") return true;
      if (this.Readonly == "edit") return !!this.model.Id;
      return !this.canEdit;
    }
  },
  methods: {
    change(val) {
      this.$emit("change", val);
      this.callback.call(this, {
        model: this.model,
        value: val
      });
    }
  }
};
</script>

<style>
</style>
