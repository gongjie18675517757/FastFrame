import { getValue,makeVueContext } from "../..//utils";
import { getDownLoadPath } from "../../config";
import store from "../../store";
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
    const { EnumItemInfo, EnumValues, renderFunc } = info;

    /**
     * 当前对象值
     */
    const value = getValue(props.item, info.Name);
    const value_arr = Array.isArray(value) ? value : [value];

    /**
     * 是否文件
     */
    const isFile = info.Relate && info.Relate.ModuleName == "Resource";

    /**
     * 外键对象
     */
    const fkObject = (function () {
      if (info.Relate && info.Relate.ModuleName && value) {
        let tempName = info.Name.replace(/_Id$/, "");
        return getValue(props.item, tempName);
      } else {
        return null;
      }
    })();

    /**
     * 文本
     */
    const text = (function () {
      let { getValueFunc, Type, Relate, Name } = info;
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
        return text.replace(/<[^>]+>/g, "").substring(0, 200);
      } else if (Type == "Boolean") {
        /**
         * 布尔值
         */
        if (text == true) return "是";
        else if (text == false) return "否";
        else return "";
      } else if (Relate) {
        /**
         * 外键
         */
        let obj = fkObject;
        if (obj) {
          return info.Relate.RelateFields.map((v) => obj[v])
            .map((v, i) => (i > 0 ? `[${v}]` : v))
            .join("");
        } else {
          return null;
        }
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
      /**
       * 文件下载
       */
      return h(
        "a",
        {
          on: {
            click: () => {
              let url = getDownLoadPath(value, fkObject ? fkObject.Name : "");
              window.open(url);
            },
          },
        },
        text
      );
    } else {
      return h("span", null, text);
    }
  },
};