import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextInput from "./TextInput.js";
import TextArea from './TextArea'
import Checkbox from './Checkbox'

import {
  getValue,
  setValue
} from "@/utils";
export default {
  props: {
    value: [String, Number, Boolean, Array],
    model: {
      type: Object,
      default: function () {
        return {};
      }
    },
    callback: {
      type: Function,
      default: function () {}
    },
    rules: {
      type: Array,
      default: function () {
        return [];
      }
    },
    errorMessages: Array,
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
    singleLine: Boolean
  },
  data() {
    return {

    }
  },
  computed: {
    val: {
      get() {
        if (this.Name && this.model)
          return getValue(this.model, this.Name)
        else
          return this.value
      },
      set(val) {
        if (this.Name && this.model)
          setValue(this.model, this.Name, val)
        else
          this.$emit('input', val)
      }
    },
    evalDisabled() {
      if (this.Readonly == "All") return true;
      if (this.Readonly == "Edit") return !!this.model.Id;
      return !this.canEdit;
    },
  },
  methods: {
    change(val) {
      this.$emit('change', val)
      this.evalRules()
      if (typeof this.callback == 'function')
        this.callback.call(this.model, val)
    },
    async evalRules() {
      let errs = []
      for (const rule of this.rules) {
        let result = await rule.call(this.model, this.val)
        if (typeof (result) == 'string') {
          errs.push(result);
          continue
        }
      }
      // this.errorMessages = errs

      this.$emit('update_errorMessages', errs)
      return errs
    }
  },
  render(h) {
    let props = {
      value: this.val,
      disabled: this.evalDisabled,
      label: this.Description,
      description: this.Description,
      errorMessages: this.errorMessages
    }
    let on = {
      ...this.$listeners,
      input: (val) => {
        this.val = val
      },
      change: val => this.change(val)
    }

    let component = null;

    //长文本
    if (this.IsTextArea) {
      component = h(TextArea, {
        props,
        on
      })
    }

    //富文本
    if (this.IsRichText) {
      component = h(RichInput, {
        props,
        on
      })
    }

    //文本,数字,密码
    if (!this.Name.endsWith("Id") && (this.Name == "Password" || this.Type == "String" ||
        this.Type == "Int32" || this.Type == "Decimal")) {
      component = h(TextInput, {
        props,
        on,
        attrs: {
          type: this.Name == 'Password' ? 'password' : ['Int32', 'Decimal'].includes(this.Type) ? 'number' : 'text'
        }
      })
    }
    if (this.Type == 'Boolean') {
      component = h(Checkbox, {
        props,
        on
      })
    }
    //远程选择框
    if (this.Relate) {
      component = h(SearchInput, {
        props: {
          ...props,
          Name: this.Name,
          Relate: this.Relate,
          filter: this.filter,
          ModuleName: this.ModuleName,
          model: this.model
        },
        on
      })
    }

    if (component) {
      let flex = {
        xs5: 1,
      }
      if (this.singleLine) {
        flex = {
          xs12: 1,
          sm10: 1,
          md8: 1,
          lg6: 1,
          xl6: 1
        }
      }
      return h('v-flex', {
        attrs: {
          ...flex
        }
      }, [component])
    } else if (this.Name && !this.Name.endsWith('Id')) {
      window.console.error(this.$props)
    }

  }
}