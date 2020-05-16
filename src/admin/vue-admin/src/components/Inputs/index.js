import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextArea from './TextArea'
// import Checkbox from './Checkbox.vue'
import SelectInput from "./SelectInput.vue";
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
    Hide: String,
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
    singleLine: Boolean,
    flex: {
      type: Object,
      default: () => ({})
    }
  },
  data() {
    return {

    }
  },
  computed: {
    evalVisible() {
      if (!this.Hide) return true;
      if (this.Hide == "All") return false;
      if (this.Hide == "Form") return false;
      return true;
    },
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
        this.callback.call(this, { model: this.model, value: val })
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

    if (!this.evalVisible) {
      return null;
    }

    let fieldLabel = h('span', {
      slot: 'prepend',
      style: {
        width: '150px',
        textAlign: 'center'
      }
    }, [
      h('span', `${props.description}:`),
      h('span', {
        style: {
          color: 'red'
        }
      }, this.IsRequired && this.canEdit ? '*' : '')
    ]
    )

    if (isXs) {
      props.label = this.Description;
      fieldLabel = null;
    }

    let on = {
      ...this.$listeners,
      input: (val) => {
        this.$emit('input', val)
      },
      change: val => this.change(val)
    }

    let flex = {
      xs12: 1,
      sm6: 1,

      ...this.flex
    }

    if (this.singleLine) {
      flex = {
        xs12: 1,

      }
    }

    let component = this.component;



    //长文本
    if (!component && this.Length && this.Length >= 200 && this.Length < 4000) {
      component = h(TextArea, {
        props,
        on
      }, [fieldLabel])
      flex = {
        xs12: 1,

        ...this.flex
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
      }, [fieldLabel])
    }
    /**
     * 单选下拉框
     */
    else if (!component && Array.isArray(this.EnumValues) && this.EnumValues.length > 0) {
      if (multiple) {
        flex = {
          xs12: true,

          ...this.flex
        }
      }
      component = h(SelectInput, {
        props: {
          ...props,
          multiple,
          values: this.EnumValues
        },
        on
      }, [fieldLabel])
    }

    /* 单选下拉框 */
    else if (!component && typeof this.EnumValues == 'function') {
      if (multiple) {
        flex = {
          xs12: true,

          ...this.flex
        }
      }
      component = h(SelectInput, {
        props: {
          ...props,
          multiple,
          values: this.EnumValues.call(this, this.model)
        },
        on
      }, [fieldLabel])
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
          dense: true,
          singleLine: !isXs,
          readonly: props.disabled,
        }
        delete textProps.disabled

        component = h('v-text-field', {
          props: textProps,
          on,
          attrs: {
            type: this.Name == 'Password' ? 'password' : ['Int32', 'Decimal'].includes(this.Type) ? 'number' : 'text'
          },
        }, [fieldLabel]);
      } else
        component = h('span', null,
          (this.Name || '').toLowerCase().includes('password') ?
            Array((props.value || '******').length).fill('*').join('') :
            props.value || '无')
    }

    else if (!component && this.Type == 'Boolean') {
      component = h(SelectInput, {
        props: {
          ...props,
          value: `${props.value}`,
          values: [
            {
              Key: 'true',
              Value: '是'
            },
            {
              Key: 'false',
              Value: '否'
            }
          ]
        },
        on
      }, [fieldLabel])
    }

    else if (!component && this.Type == 'DateTime') {
      component = h(DateInput, {
        props: {
          ...props,
          type: this.Name && this.Name.toLowerCase().includes('time') ? 'datetime' : 'date'
        },
        on
      }, [fieldLabel])
    }

    else if (!component && this.Relate == 'Resource') {
      component = h(FileInput, {
        props: {
          ...props,
          model: this.model || {},
          Name: this.Name,
        },
        on
      }, [fieldLabel])
    }

    //远程选择框
    else if (!component && this.Relate) {
      if (this.Type == 'Array') {
        flex = {
          xs12: true,

          ...this.flex
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
        }, [fieldLabel])
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
        }, [fieldLabel])
      }
    }

    if (component) {
      if (props.disabled && !isXs) {
        component = h('v-input', {
          props: {
            ...props,
            dense: true,
            singleLine: !isXs,
            readonly: props.disabled,
            disabled: false
          },
          class: [this.canEdit ? 'v-text-field' : null]
        }, [fieldLabel, component])
      }
      return h('v-flex', {
        attrs: isXs ? { xs12: true } : flex,
        class: ['input-container'],
        style: {
          padding: '5px',
          color: props.error ? 'red' : null
        }
      }, [component])



    } else if (this.Name && !this.Name.endsWith('Id')) {
      window.console.error(this.$props)
    }
  }
}