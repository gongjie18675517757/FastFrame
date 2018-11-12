<template>
  <v-flex v-bind="formItemFlex">
    <v-text-field
      v-if="evalType=='text' || evalType=='password'"
      v-model="model[Name]"
      :rules="evalRules"
      :label="Description"
      :error-messages="ErrorMessage"
      :type="evalType"
      :disabled="evalDisabled"
      @change="change"
    ></v-text-field>
    <v-checkbox
      v-if="evalType=='Boolean'"
      v-model="model[Name]"
      :rules="evalRules"
      :label="Description"
      :error-messages="ErrorMessage"
      :disabled="evalDisabled"
      @change="change"
    ></v-checkbox>
    <SearchInput
      v-if="evalType=='remoteSelect'"
      v-model="model[Name]"
      :model="model"
      :disabled="evalDisabled"
      :label="Description"
      :rules="evalRules"
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
    ModuleName: String,
    Relate: String,
    Type: {
      type: String,
      default: 'text'
    },
    Description: String,
    Name: String,
    Readonly: String,
    Rules: {
      type: Array,
      default: function() {
        return []
      }
    }
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
    },
    evalRules() {
      let evalRules = this.Rules.map(r => {
        if (rules[r.RuleName]) {
          return rules[r.RuleName].call(this, this.Description, ...r.RulePars)
        } else {
          return
        }
      }).filter(r => !!r)
      if (this.Name.includes('Email')) evalRules.push(rules.email.call(this))
      if (this.Name.includes('Phone')) evalRules.push(rules.phone.call(this))
      return evalRules
    }
  },
  methods: {
    change(val) {
      this.evalUnique(val)
      this.$emit('change', val)
      this.callback.call(this, {
        model: this.model,
        value: val
      })
    },
    evalUnique(val) {
      this.ErrorMessage = []
      let postData = {
        Id: this.model.Id ? this.model.Id : '',
        ModuleName: this.ModuleName,
        KeyValues: [
          {
            Key: this.Name,
            Value: val
          }
        ]
      }
      this.Rules.filter(r => r.RuleName == 'unique').forEach(async r => {
        let data = await this.$http.post(`/api/Common/VerififyUnique`, postData)
        if (data) {
          this.ErrorMessage.push(`${this.Description}重复!`)
        }
      })
    }
  }
}
</script>

<style>
</style>
