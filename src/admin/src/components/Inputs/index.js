import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextArea from './TextArea'
import Checkbox from './Checkbox'
import SelectInput from "./SelectInput";
import DateInput from './DateInput.vue'
import FileInput from './FileInput'
import {
  getValue,
  setValue
} from "@/utils";

export default {
  props: {
    value: [String, Number, Boolean, Array],
    model: Object,
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
    Length: Number,
    ModuleName: String,
    Relate: String,
    Type: {
      type: String,
      default: "text"
    },
    Description: String,
    Name: String,
    Readonly: String,
    EnumValues: Array,
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
        this.$emit('input', val)
      }
    },
    evalDisabled() {
      if (typeof this.Readonly == 'function') return this.this.Readonly
      if (this.Readonly == "All") return true;
      if (this.Readonly == "Edit") return !!(this.model || {}).Id;
      return !this.canEdit;
    },
  },
  methods: {
    change(val) {
      this.$emit('change', val)
      if (this.errorMessages)
        this.evalRules()
      if (typeof this.callback == 'function')
        this.callback.call(this.model || {}, val)
    },
    async evalRules() {
      let errs = []
      for (const rule of this.rules) {
        let result = (await rule.call(this.model || {}, this.val))
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
    let errs = this.errorMessages || []
    let isXs = this.$vuetify.breakpoint.smAndDown
    let props = {
      value: this.val,
      disabled: this.evalDisabled,
      // label: this.Description,
      description: this.Description,
      errorMessages: this.errorMessages,
      errorCount: errs.length,
      error: !!errs.find(r => r),
      required: this.IsRequired,
      isXs
    }

    if (isXs) {
      props.label = this.Description
    }

    let on = {
      ...this.$listeners,
      input: (val) => {
        this.val = val
        this.change(val)
      },
      change: val => this.change(val)
    }

    let flex = {
      xs12: 1,
      sm8: 1,
      md6: 1,
      lg6: 1,
      xl4: 1
    }

    if (this.singleLine) {
      flex = {
        xs12: 1,
      }
    }

    let component = null;

    //长文本
    if (!component && this.Length && this.Length >= 200 && this.Length < 4000) {
      component = h(TextArea, {
        props,
        on
      })

      flex = {
        xs12: 1,
        sm8: 1,
        md6: 1,
        lg6: 1,
        xl4: 1
      }
    }

    //富文本
    else if (!component && this.Length && this.Length >= 4000) {
      component = h(RichInput, {
        props,
        on
      })

      return component
    }

    //文本,数字,密码
    else if (!component && !this.Name.endsWith("Id") && (this.Name == "Password" || this.Type == "String" ||
        this.Type == "Int32" || this.Type == "Decimal")) {
      if (!props.disabled || isXs) {
        let textProps = {
          ...props,
          readonly: props.disabled
        }
        delete textProps.disabled

        component = h('v-text-field', {
          props: textProps,
          on,
          attrs: {
            type: this.Name == 'Password' ? 'password' : ['Int32', 'Decimal'].includes(this.Type) ? 'number' : 'text'
          }
        });
      } else
        component = h('span', null,
          (this.Name || '').toLowerCase().includes('password') ?
          Array((props.value || '******').length).fill('*').join('') :
          props.value)
    } else if (!component && this.Type == 'Boolean') {
      component = h(Checkbox, {
        props,
        on
      })
    } else if (!component && this.Type == 'DateTime') {
      component = h(DateInput, {
        props,
        on
      })
    } else if (!component && this.Relate == 'Resource') {
      component = h(FileInput, {
        props: {
          ...props,
          model: this.model || {},
          Name: this.Name,
        },
        on
      })
    }
    //远程选择框
    else if (!component && this.Relate) {
      component = h(SearchInput, {
        props: {
          ...props,
          Name: this.Name,
          Relate: this.Relate,
          filter: this.filter,
          ModuleName: this.ModuleName,
          model: this.model || {}
        },
        on
      })
    } else if (!component && Array.isArray(this.EnumValues) && this.EnumValues.length > 0) {
      component = h(SelectInput, {
        props: {
          ...props,
          values: this.EnumValues
        },
        on
      })
    }

    if (component) {
      if (this.$vuetify.breakpoint.smAndDown) {
        return h('v-flex', {
          attrs: {
            xs12: true
          }
        }, [component]);
      }


      // return h('v-flex', {
      //   attrs: {
      //     ...flex
      //   }
      // }, [component]) 

      return h('v-flex', {
        attrs: flex,
        class: ['input-container']
      }, [
        h('v-layout', {
            class: ['much-input']
          },
          [
            h('v-flex', {
              attrs: {
                xs3: 1,
              },
              style: {
                display: 'table-cell',
                'vertical-align': 'bottom',
              }
            }, [
              h('span', null, `${this.Description}:`),
              h('span', {
                style: {
                  color: 'red'
                }
              }, this.IsRequired && this.canEdit ? '*' : '')
            ]),
            h('v-flex', {
              attrs: {
                xs6: !!this.errorMessages,
                xs8: !this.errorMessages
              },
              style: {
                padding: '12px'
              }
            }, [component]),
            // h('v-flex', {
            //   style: {
            //     display: 'table-cell',
            //     'vertical-align': 'bottom',
            //     color: 'red'
            //   }
            // },null)
          ])
      ])
    } else if (this.Name && !this.Name.endsWith('Id')) {
      window.console.error(this.$props)
    }
  }
}