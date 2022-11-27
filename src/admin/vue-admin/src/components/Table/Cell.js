import { convertHtmlToText, getValue, makeVueContext } from "../..//utils";
import { getDownLoadPath } from "../../config";
import store from "../../store";
import { existsIsImage } from "../../utils/fileIcons";
export default {
  functional: true,
  props: {
    info: {
      type: Object,
      required: true,
    },
    props: {
      type: Object,
      required: true,
    },
  },

  render(h, context) {
    const { props: attrs } =
      context || makeVueContext.call(this, { inject: [] });
    const { info, props } = attrs;
    const { EnumItemInfo, EnumValues, renderFunc, RelateKeyFieldName } = info;

    /**
     * 当前对象值
     */
    const value = getValue(props.item, info.Name);
    const value_arr = Array.isArray(value) ? value : [value];

    /**
     * 是否文件
     */
    const isFile = info.Type == 'File';

    /**
     * 外键对象
     */
    const fkObject_Key = (function () {
      if (RelateKeyFieldName) {
        return getValue(props.item, RelateKeyFieldName);
      } else {
        return null;
      }
    })();

    /**
     * 文本
     */
    const text = (function () {
      let { getValueFunc, Type, Name } = info;
      let text = value;
      let length = info.Length || 0;

      /**
       * 自定义取值逻辑
       */
      if (typeof getValueFunc == "function") {
        return getValueFunc({
          value: value,
          model: props.item,
          info: info,
        });
      } else if (length >= 4000) {
        /**
         * 富文本
         */
        return convertHtmlToText(text).substring(0, 200);
      } else if (Type == "Boolean") {
        /**
         * 布尔值
         */
        if (text == true) return "是";
        else if (text == false) return "否";
        else return "";
      } else if (Type == "DateTime") {
        /**
         * 日期
         */
        let name = Name.toLowerCase();
        if (name.includes("time") && text) {
          return text.substring(0, 16);
        } else if (text) {
          return text.substring(0, 10);
        } else {
          return null;
        }
      } else if (EnumItemInfo) {
        store.dispatch("loadEnumValues", EnumItemInfo.Name);
        return store.getters
          .getItemValues(EnumItemInfo.Name)
          .filter((v) => value_arr.includes(v.Id))
          .map((v) => v.Value)
          .join(",");
      } else if (EnumValues) {
        const values =
          typeof EnumValues == "function" ? EnumValues(props.item) : EnumValues;
        return values
          .filter((v) => value_arr.includes(v.Key))
          .map((v) => v.Value)
          .join(",");
      } else {
        return text;
      }
    })();

    /**
     * 自定义渲染逻辑
     */
    if (typeof renderFunc == "function") {
      return renderFunc(h, {
        value: value,
        text: text,
        model: props.item,
        info: info,
      });
    }

    if (isFile) {
      let src = getDownLoadPath(fkObject_Key, value);
      if (existsIsImage(value)) {
        /**
        * 图片下载
        */
        return h(
          "img",
          {
            attrs: {
              src,
              title:`${value} 点击打开`
            },
            style: {
              maxWidth: '100%',
              maxHeight: '100%',
              borderRadius: '50%',
              padding: '2px'
            },
            on: {
              click: () => {
                window.open(src);
              },
            },
          }
        );
      }

      else {
        /**
       * 文件下载
       */
        return h(
          "a",
          {
            attrs: {
              href: src,
              target: '_blank'
            }
          },
          text
        );
      }
    } else {
      return h("span", null, text);
    }
  },
};