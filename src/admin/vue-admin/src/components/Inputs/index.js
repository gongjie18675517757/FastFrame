import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextArea from './TextArea'
import Checkbox from './Checkbox.vue'
import SelectInput from "./SelectInput";
import SelectMulitInput from "./SelectMulitInput";
import DateInput from './DateInput.vue'
import FileInput from './FileInput'
import EnumItemInput from './EnumItemInput.js'

export default {
  props: {
    value: [String, Number, Boolean, Array],
    model: Object,
    callback: {
      type: Function,
      default: function () { }
    },
    rules: {
      type: Array,
      default: function () {
        return [];
      }
    },
    errorMessages: Array,
    canEdit: Boolean,
    IsRequired: Boolean,
    Length: Number,
    ModuleName: String,
    Relate: String,
    EnumItemInfo: Object,
    Type: {
      type: String,
      default: "text"
    },
    Description: String,
    Name: String,
    Readonly: String,
    EnumValues: [Array, Function],
    filter: [Array, Function],
    singleLine: Boolean
  },
  data() {
    return {

    }
  },
  computed: {
    // val: {
    //   get() {
    //     if (this.Name && this.model)
    //       return getValue(this.model, this.Name)
    //     else
    //       return this.value
    //   },
    //   set(val) {
    //     if (this.Name && this.model)
    //       setValue(this.model, this.Name, val)
    //     this.$emit('input', val)
    //   }
    // },
    evalDisabled() {
      if (!this.canEdit) return true;
      if (typeof this.Readonly == 'function') return this.Readonly.call(this, this.model)
      if (this.Readonly == "All") return true;
      if (this.Readonly == "Edit") return !!(this.model || {}).Id;
      return false;
    },
  },
  methods: {
    change(val) {
      this.$emit('change', val)
      if (typeof this.callback == 'function') {
        this.callback(this, this.model, val)
      }
    },
  },
  render(h) {
    let errs = this.errorMessages || []
    let isXs = this.$vuetify.breakpoint.smAndDown
    let multiple = this.Type == 'Array'
    let props = {
      value: this.value,
      disabled: this.evalDisabled,
      // label: this.Description,
      description: this.Description,
      placeholder: this.Description,
      errorMessages: this.errorMessages,
      errorCount: errs.length,
      error: !!errs.find(r => r),
      required: this.IsRequired,
      isXs,
      ...this.$attrs
    }

    if (isXs) {
      props.label = this.Description
    }

    let on = {
      ...this.$listeners,
      input: (val) => {
        console.log(val);

        this.$emit('input', val)
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

      }
    }
    /**
     * 数据字典选择
     */
    else if (!component && this.EnumItemInfo) {
      component = h(EnumItemInput, {
        props: {
          ...props,
          multiple,
          EnumItemInfo: this.EnumItemInfo,
          model: this.model || {}
        },
        on
      })
    }
    /**
     * 单选下拉框
     */
    else if (!component && Array.isArray(this.EnumValues) && this.EnumValues.length > 0) {
      if (multiple) {
        flex = {
          xs12: true
        }
      }
      component = h(SelectInput, {
        props: {
          ...props,
          multiple,
          values: this.EnumValues
        },
        on
      })
    }

    /* 单选下拉框 */
    else if (!component && typeof this.EnumValues == 'function') {
      if (multiple) {
        flex = {
          xs12: true
        }
      }
      component = h(SelectInput, {
        props: {
          ...props,
          multiple,
          values: this.EnumValues.call(this, this.model)
        },
        on
      })
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
          readonly: props.disabled,
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
      if (this.Type == 'Array') {
        flex = {
          xs12: true
        }
        component = h(SelectMulitInput, {
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
      }
      else {
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
      }
    }

    if (component) {
      if (this.$vuetify.breakpoint.smAndDown) {
        return h('v-flex', {
          attrs: {
            xs12: true
          }
        }, [component]);
      }
      return h('v-flex', {
        attrs: flex,
        class: ['input-container'],
        style: {
          padding: '10px',
          color: props.error ? 'red' : null
        }
      }, [
        h('v-layout', {
          class: ['much-input']
        },
          [
            h('div', {
              style: {
                width: '150px',
                'padding': '10px',
                'text-align':'center',
                display:'table-cell',
                'line-height':'20px'
              }
            }, [
              h('span', {
                style: {

                }
              }, `${this.Description}:`),
              h('span', {
                style: {
                  color: 'red'
                }
              }, this.IsRequired && this.canEdit ? '*' : '')
            ]),
            h('div', {
              style: {
                width: this.errorMessages ? 'calc(100% - 150px - 150px)' : 'calc(100% - 150px)',
                'padding-top': props.disabled ? '10px' : null,
                'padding-left': '5px'
              }
            }, [component])
          ])
      ])
    } else if (this.Name && !this.Name.endsWith('Id')) {
      window.console.error(this.$props)
    }
  }
}