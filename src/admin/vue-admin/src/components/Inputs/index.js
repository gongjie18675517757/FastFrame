import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextArea from './TextArea'
import SelectInput from "./SelectInput.vue";
import SelectMulitInput from "./SelectMulitInput";
import DateInput from './DateInput.vue'
import FileInput from './FileInput'
import EnumItemInput from './EnumItemInput.js'
import { makeVueContext } from "../../utils";

const inject = ['get_Vuetify'];

export default {
  /**
   * 这里是定义为函数式组件的意思
   * 在生产环境下使用函数式组件,性能会高出不少
   * 在调试时,可以改为false,方便调试
   */
  functional: true,
  inheritAttrs: true,
  inject,
  props: {
    value: [String, Number, Boolean, Array],
    model: Object,
    callback: {
      type: Function,
      default: function () { }
    },
    errorMessages: {
      type: Array,
      default: () => []
    },
    canEdit: Boolean,
    IsRequired: Boolean,
    isXs: Boolean,
    Length: Number,
    Hide: String,
    ModuleName: String,
    Relate: String,
    requestUrl: String,
    EnumItemInfo: Object,
    component: Object,
    Type: {
      type: String,
      default: "text"
    },
    Description: String,
    Name: String,
    Readonly: [String, Function, Boolean],
    EnumValues: [Array, Function],
    filter: [Array, Function],

    flex: {
      type: Object,
      default: () => ({})
    }
  },

  render(h, context) {
    // console.log(context);
    const { listeners, props, data, injections } = context || makeVueContext.call(this, { inject });

    const $vuetify = injections.get_Vuetify()

    /**
     * 计算是否显示
     */
    const evalVisible = (function () {
      {
        if (!props.Hide) return true;
        if (props.Hide == "All") return false;
        if (props.Hide == "Form") return false;
        return true;
      }
    })();

    if (!evalVisible) {
      return null;
    }

    /**
     * 计算是否禁用
     */
    const evalDisabled = (function () {
      if (!props.canEdit) return true;
      if (typeof props.Readonly == 'function') return props.Readonly(props.model)
      if (typeof props.Readonly == 'boolean') return props.Readonly;
      if (props.Readonly == "All") return true;
      if (props.Readonly == "Edit") return props.model && !!props.model.Id;
      return false;
    })();

    /**
     * 传递给下级的参数
     */
    const childProps = (function () {
      return {
        ...data.attrs,
        value: props.value,
        requestUrl: props.requestUrl,
        disabled: evalDisabled,
        label: null,
        description: props.Description,
        placeholder: props.Description,
        errorMessages: props.errorMessages,
        errorCount: props.errorMessages.length,
        error: !!props.errorMessages.find(r => r),
        required: props.IsRequired,
        isXs: props.isXs || $vuetify.breakpoint.smAndDown,
      }
    })()

    /**
     * 是否多选
     */
    let multiple = props.Type == 'Array'

    /**
     * 输入框的标签
     */
    let fieldLabel = h('span', {
      slot: 'prepend',
      style: {
        width: '150px',
        textAlign: 'right',
        // 'margin-top': childProps.disabled ? null : '4px',
        'padding': '5px 12px',
        'padding-top': childProps.disabled ? '6px' : '7px',

      }
    }, [
      h('span', {
        style: {
          color: 'red'
        }
      }, props.IsRequired && props.canEdit ? '*' : ''),
      h('span', `${childProps.description}:`),
    ]
    )

    if (childProps.isXs) {
      childProps.label = props.Description;
      fieldLabel = null;
    }

    let on = {
      ...listeners,
      input: (val) => {
        listeners.input && listeners.input(val)

      },
      change: val => {
        listeners.change && listeners.change(val)

        if (typeof props.callback == 'function') {
          props.callback({ model: props.model, value: val })
        }
      }
    }

    let flex = {
      xs12: 1,
      sm6: 1,

      ...props.flex
    }

    let component = props.component ? h(props.component, {
      props: {
        ...childProps,
      },
      on
    }) : null;



    //长文本
    if (!component && props.Length && props.Length > 200 && props.Length < 4000) {
      component = h(TextArea, {
        props: childProps,
        on
      }, [fieldLabel])
      flex = {
        xs12: 1,
      }
    }
    /**
     * 数据字典选择
     */
    else if (!component && props.EnumItemInfo) {
      component = h(EnumItemInput, {
        props: {
          ...childProps,
          multiple,
          EnumItemInfo: props.EnumItemInfo,
          model: props.model || {}
        },
        on
      }, [fieldLabel])
    }
    /**
     * 单选下拉框
     */
    else if (!component && Array.isArray(props.EnumValues) && props.EnumValues.length > 0) {
      if (multiple) {
        flex = {
          xs12: true,
        }
      }
      component = h(SelectInput, {
        props: {
          ...childProps,
          multiple,
          values: props.EnumValues
        },
        on
      }, [fieldLabel])
    }

    /* 单选下拉框 */
    else if (!component && typeof props.EnumValues == 'function') {
      if (multiple) {
        flex = {
          xs12: true,
        }
      }
      component = h(SelectInput, {
        props: {
          ...childProps,
          multiple,
          values: props.EnumValues(props.model)
        },
        on
      }, [fieldLabel])
    }

    //富文本
    else if (!component && props.Length && props.Length >= 4000) {
      component = h(RichInput, {
        props: childProps,
        on
      })
      return component
    }

    //文本,数字,密码
    else if (!component && !props.Name.endsWith("Id") &&
      ["Password", "String", "Int32", "Decimal"].includes(props.Type)) {

      if (!childProps.disabled || childProps.isXs) {
        let textProps = {
          ...childProps,
          dense: !childProps.isXs,

          readonly: childProps.disabled,
        }
        delete textProps.disabled

        component = h('v-text-field', {
          props: textProps,
          on,
          attrs: {
            type: props.Name == 'Password' ? 'password' : ['Int32', 'Decimal'].includes(props.Type) ? 'number' : 'text'
          },
        }, [fieldLabel]);
      } else {
        component = h('span', null,
          (props.Name || '').toLowerCase().includes('password') ?
            Array((childProps.value || '******').length).fill('*').join('') :
            childProps.value || '无')
      }
    }

    else if (!component && props.Type == 'Boolean') {
      component = h(SelectInput, {
        props: {
          ...childProps,
          value: `${childProps.value}`,
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

    else if (!component && props.Type == 'DateTime') {
      component = h(DateInput, {
        props: {
          ...childProps,
          type: props.Name && props.Name.toLowerCase().includes('time') ? 'datetime' : 'date'
        },
        on
      }, [fieldLabel])
    }

    else if (!component && props.Relate == 'Resource') {
      component = h(FileInput, {
        props: {
          ...childProps,
          model: props.model || {},
          Name: props.Name,
        },
        on
      }, [fieldLabel])
    }

    //远程选择框
    else if (!component && props.Relate) {
      if (props.Type == 'Array') {
        flex = {
          xs12: true,
        }
        component = h(SelectMulitInput, {
          props: {
            ...childProps,
            Name: props.Name,
            Relate: props.Relate,
            filter: props.filter,
            ModuleName: props.ModuleName,
            model: props.model || {}
          },
          on
        }, [fieldLabel])
      }
      else {
        component = h(SearchInput, {
          props: {
            ...childProps,
            Name: props.Name,
            Relate: props.Relate,
            filter: props.filter,
            ModuleName: props.ModuleName,
            model: props.model || {}
          },
          on
        }, [fieldLabel])
      }
    }

    if (component) {
      if (childProps.disabled && !childProps.isXs) {
        component = h('v-input', {
          props: {
            ...childProps,
            dense: true,
            readonly: childProps.disabled,
            disabled: false
          },
          class: [props.canEdit ? 'v-text-field' : null, 'border-input']
        }, [fieldLabel, component])
      }

      if (props.isXs) {
        return component;
      }
      else {
        return h('v-flex', {
          attrs: childProps.isXs ? { xs12: true } : flex,
          class: ['input-container', 'border-input'],
          style: {
            padding: '5px',
            color: childProps.error ? 'red' : null
          }
        }, [component])
      }




    } else if (props.Name && !props.Name.endsWith('Id')) {
      window.console.error(context)
    }
  }
}