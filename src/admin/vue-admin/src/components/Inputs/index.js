
import SearchInput from "./SearchInput.vue";
import RichInput from "./RichInput.vue";
import TextArea from "./TextArea";
import SelectInput from "./SelectInput.vue";
import SelectMulitInput from "./SelectMulitInput";
import DateInput from "./DateInput.vue";
import FileInput from "./FileInput";
import EnumItemInput from "./EnumItemInput.js";
import Checkbox from "./Checkbox.vue";
import { makeVueContext } from "../../utils";

/**
 * 所有的子控件
 */
export const inputs = {
  SearchInput,
  RichInput,
  TextArea,
  SelectInput,
  SelectMulitInput,
  DateInput,
  FileInput,
  EnumItemInput,
  Checkbox,
  TextInput: 'v-text-field'
}

/**
 * 计算使用哪个控件
 */
export function calcInputComponent(props) {
  const { component, Length, EnumItemInfo, multiple, Type, EnumValues, Name } = props;
  if (component) {
    return {
      colspan: 1,
      component
    }
  }
  //长文本
  else if (Length && Length > 200 && Length < 4000) {
    return {
      colspan: 2,
      component: inputs.TextArea
    }
  }

  /**
   * 数据字典选择
   */
  else if (EnumItemInfo) {
    return {
      colspan: multiple ? 2 : 1,
      component: inputs.EnumItemInput
    }
  }
  /**
  * 勾选是否
  */
  else if (Type == 'Boolean') {
    return {
      colspan: 1,
      component: inputs.Checkbox
    }
  }
  /**
    * 单选下拉框
    */
  else if (EnumValues) {
    return {
      colspan: multiple ? 2 : 1,
      component: inputs.SelectInput
    }
  }

  //富文本
  else if (Length && Length >= 4000) {
    return {
      colspan: 12,
      component: inputs.RichInput
    }
  }

  //文本,数字,密码
  else if (
    !Name.endsWith("Id") &&
    ["Password", "String", "Int32", "Decimal"].includes(Type)
  ) {
    return {
      colspan: 1,
      component: inputs.TextInput
    }

  } else if (["DateTime",'DateOnly','TimeOnly'].includes(Type)) {
    return {
      colspan: 1,
      component: inputs.DateInput
    }
  } else if (!component && props.Relate == "Resource") {
    return {
      colspan: 1,
      component: inputs.FileInput
    }
  }

  //远程选择框
  else if (!component && props.Relate) {
    return {
      colspan: multiple ? 2 : 1,
      component: multiple ? inputs.SelectMulitInput : inputs.SearchInput
    }
  }

  return {
    colspan: 0,
    component: null
  };
}

/**
 * 要注入的方法
 */
const inject = ["get_Vuetify"];

/**
 * 计算表单输入框的宽(1:12)
 * @param {Number} pageFormCols 几列式布局(2/3/4/6/12)
 */
export function calcInputItemCols(pageFormCols, props) {
  const width = parseInt(12 / pageFormCols);
  let { colspan = 1 } = props.colspan ? props : calcInputComponent(props);
  return colspan * width;
}

/**
 * 计算是否显示控制
 * @param {*} param0 
 * @returns 
 */
export function calcInputVisible({ Hide, visible }, model) {
  if (typeof visible == 'function')
    return visible(model)
  else if (typeof visible == 'boolean')
    return visible
  else if (!Hide && !visible)
    return true;
  else if (Hide == "All")
    return false;
  else if (Hide == "Form")
    return false;
  else
    return true
}

/**
 * 标签组件
 */
export const labelComponent = {
  functional: true,
  render(h, { props }) {
    return h(
      "span",
      {
        slot: "prepend",
        style: {
          width: props.labelWidth || "150px",
          textAlign: "right",
          // 'margin-top': childProps.disabled ? null : '4px',
          padding: "5px 12px",
          "padding-top": props.disabled ? "6px" : "7px",
        },
      },
      [
        h(
          "span",
          {
            style: {
              color: "red",
            },
          },
          props.IsRequired && props.canEdit ? "*" : ""
        ),
        h("span", `${props.description}:`),
      ]
    )
  }
}

/**
 * 包装组件
 * @param {*} h  
 * @param {*} param1 
 * @returns 
 */
export function packComponent(h, component, { props, on }) {
  /**
   * 输入框的标签
   */
  const fieldLabel = h(
    labelComponent,
    {
      slot: "prepend",
      props: {
        ...props
      }
    },
  );

  return h(
    "div",
    {
      attrs: {
        'input-name': props.Name,
      },
      class: ["input-container", "border-input"],
      style: {
        color: props.error ? "red" : null,
      },
    },
    [
      h(component, {
        props,
        on
      }, [fieldLabel])
    ]
  )
}

/**
 * 包装组件工厂
 * @param {*} h 
 * @param {*} param1 
 * @returns 
 */
export function packComponentFacatory(h, component, { props, on }) {
  return function (inheritAttrs = {}) {
    const merge_props = { ...props, ...inheritAttrs };
    return packComponent(h, component, { props: merge_props, on })
  }
}

/**
 * 计算是否禁用
 * @param {*} param0 
 * @param {*} model 
 * @returns 
 */
export function calcInputDisabled({ canEdit, Readonly }, model) {
  if (!canEdit) return true;
  if (typeof Readonly == "function")
    return Readonly(model);
  if (typeof Readonly == "boolean") return Readonly;
  if (Readonly == "All") return true;
  if (Readonly == "Edit") return model && !!model.Id;
  return false;
}

/**
 * 计算数据字典的值
 * @param {*} param0 
 * @param {*} model 
 * @returns 
 */
export function calcInputEnumValues({ EnumValues }, model) {
  if (!EnumValues)
    return null;
  else if (Array.isArray(EnumValues))
    return EnumValues;
  else if (typeof (EnumValues) == 'function')
    return EnumValues(model);
  else
    return null;
}

/**
 * 组件工厂
 */
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
    value: [String, Number, Boolean, Array, Object],
    model: Object,
    callback: {
      type: Function,
      default: function () { },
    },
    errorMessages: {
      type: Array,
      default: () => [],
    },
    canEdit: Boolean,
    IsRequired: Boolean,
    isXs: Boolean,
    Length: Number,
    Hide: String,
    labelWidth: String,
    ModuleName: String,
    Relate: String,
    requestUrl: [String,Function],
    EnumItemInfo: Object,
    component: Object,
    Type: {
      type: String,
      default: "text",
    },
    Description: String,
    Name: String,
    Readonly: [String, Function, Boolean],
    EnumValues: [Array, Function],
    filter: [Array, Function],
  },
  render(h, context) {
    const { listeners, props, data, injections } =
      context || makeVueContext.call(this, { inject });

    const $vuetify = injections.get_Vuetify();

    /**
     * 计算是否显示
     */
    const evalVisible = calcInputVisible(props, props.model);

    if (!evalVisible) {
      return null;
    }

    /**
     * 计算是否禁用
     */
    const evalDisabled = calcInputDisabled(props, props.model);

    /**
     * 计算数据字典的值
     */
    const enum_values = calcInputEnumValues(props, props.model)

    /**
     * 传递给下级的参数
     */
    const childProps = (function () {
      return {
        ...props,
        ...data.attrs,
        disabled: evalDisabled,
        label: null,
        description: props.Description,
        placeholder: props.Description,
        errorCount: props.errorMessages.length,
        error: props.errorMessages.some((r) => r),
        required: props.IsRequired,
        isXs: props.isXs || $vuetify.breakpoint.smAndDown,
        /**
         * 是否多选
         */
        multiple: props.Type == "Array"
      };
    })();

    if (childProps.isXs) {
      childProps.label = props.Description;
    }

    /**
     * 事件监听
     */
    let on = {
      ...listeners,
      input: (val) => {
        listeners.input && listeners.input(val);
      },
      change: (val) => {
        listeners.change && listeners.change(val);

        if (typeof props.callback == "function") {
          props.callback({ model: props.model, value: val });
        }
      },
    };

    /**
     * 计算组件
     */
    const { component } = calcInputComponent(childProps);
    const _h = packComponentFacatory(h, component, { props: childProps, on });

    switch (component) {
      case null:
        break;
      case inputs.SelectInput:
        return _h({
          values: enum_values,
        })
      case inputs.TextInput:
        return _h({
          type:
            (props.Name || '').toLowerCase().includes('password')
              ? "password"
              : ["Int32", "Decimal"].includes(props.Type)
                ? "number"
                : "text",
        });

      case inputs.DateInput:
        return _h({
          type:
            props.Name && props.Name.toLowerCase().includes("time")
              ? "datetime"
              : "date",
        })
      case inputs.FileInput:
      case inputs.SearchInput:
      case inputs.EnumItemInput:
      case inputs.SelectMulitInput:
      case inputs.Checkbox:
      case inputs.RichInput:
      case inputs.TextArea:
      default:
        return _h({})
    }


    if (props.Name && !props.Name.endsWith("Id")) {
      window.console.error(context);
    }
  },
};
