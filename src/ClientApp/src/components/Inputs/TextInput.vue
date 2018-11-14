<template>
  <v-flex v-bind="formItemFlex" v-if="evalType">
    <v-text-field
      v-if="evalType=='text' || evalType=='password'"
      v-model="model[Name]"
      :rules="rules"
      :label="Description"
      :error-messages="ErrorMessage"
      :type="evalType"
      :disabled="evalDisabled"
      @change="change"
    ></v-text-field>
    <v-checkbox
      v-if="evalType=='Boolean'"
      v-model="model[Name]"
      :rules="rules"
      :label="Description"
      :error-messages="ErrorMessage"
      :disabled="evalDisabled"
      @change="change"
    ></v-checkbox>
    <SearchInput
      v-if="evalType=='remoteSelect'"
      v-model="model[Name]"
      :Name="Name"
      :model="model"
      :disabled="evalDisabled"
      :label="Description"
      :rules="rules"
      :Relate="Relate"
      @change="change"
    />
  </v-flex>
</template>

<script>
import SearchInput from './SearchInput.vue'
import rules from '@/rules'
export default {
  components: {
    SearchInput
  },
  props: {
    model: {
      type: Object,
      default: function() {
        return {}
      }
    },
    callback: {
      type: Function,
      default: function() {}
    },
    rules: {
      type: Array,
      default: function() {
        return []
      }
    },
    ModuleName: String,
    Relate: String,
    Type: {
      type: String,
      default: 'text'
    },
    Description: String,
    Name: String,
    Readonly: String
  },
  data() {
    return {
      ErrorMessage: []
    }
  },
  computed: {
    formItemFlex() {
      return {
        xs12: '',
        sm4: ''
      }
    },
    evalType() {
      if (this.Name == 'Password') {
        return 'password'
      }
      if (this.Relate) {
        return 'remoteSelect'
      }
      if (this.Name.endsWith('Id')) {
        return
      }
      if (this.Type == 'String') {
        return 'text'
      }

      return '' //this.Type
    },
    evalDisabled() {
      return (
        this.Readonly == 'all' || (this.Readonly == 'edit' && this.model.Id)
      )
    }
  },
  methods: {
    change(val) {     
      this.$emit('change', val)
      this.callback.call(this, {
        model: this.model,
        value: val
      })
    }
  }
}
</script>

<style>
</style>
