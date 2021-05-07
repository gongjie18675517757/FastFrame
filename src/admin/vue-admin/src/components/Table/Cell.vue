 <script>
import { getValue } from "@/utils";
import EnumItemInput from "@/components/Inputs/EnumItemInput";
import SelectInput from "@/components/Inputs/SelectInput.vue";
import { getDownLoadPath } from "../../config";
export default {
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
  data() {
    return {};
  },
  computed: {
    /**
     * 当前对象值
     */
    value() {
      return getValue(this.props.item, this.info.Name);
    },

    /**
     * 是否文件
     */
    isFile() {
      return this.info.Relate && this.info.Relate.ModuleName == "Resource";
    },

    /**
     * 外键对象
     */
    fkObject() {
      if (this.info.Relate && this.info.Relate.ModuleName && this.value) {
        let tempName = this.info.Name.replace(/_Id$/, "");
        return getValue(this.props.item, tempName);
      } else {
        return null;
      }
    },

    /**
     * 文本
     */
    text() {
      let { getValueFunc, Type, Relate, Name } = this.info;
      let text = this.value;
      let length = this.info.Length || 0;

      /**
       * 自定义取值逻辑
       */
      if (typeof getValueFunc == "function") {
        return getValueFunc({
          value: this.value,
          model: this.props.item,
          info: this.info,
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
        let obj = this.fkObject;
        if (obj) {
          return this.info.Relate.RelateFields.map((v) => obj[v])
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
      } else {
        return text;
      }
    },
  },

  render(h) {
    let { IsLink, EnumItemInfo, EnumValues, renderFunc } = this.info;

    /**
     * 自定义渲染逻辑
     */
    if (typeof renderFunc == "function") {
      return renderFunc(h, {
        value: this.value,
        model: this.props.item,
        info: this.info,
      });
    } else if (IsLink) {
      return h(
        "a",
        {
          on: {
            click: () => this.$emit("toEdit", this.props.item),
          },
        },
        this.text
      );
    } else if (this.isFile) {
      /**
       * 文件下载
       */
      return h(
        "a",
        {
          on: {
            click: () => {
              let url = getDownLoadPath(
                this.value,
                this.fkObject ? this.fkObject.Name : ""
              );
              window.open(url);
            },
          },
        },
        this.text
      );
    } else if (EnumItemInfo) {
      /**
       * 数据字典
       */
      return h(EnumItemInput, {
        props: {
          value: this.value,
          EnumItemInfo,
          disabled: true,
          multiple: this.info.Type == "Array",
        },
      });
    } else if (
      /**
       * 枚举
       */
      (Array.isArray(EnumValues) && EnumValues.length > 0) ||
      typeof EnumValues == "function"
    ) {
      if (typeof EnumValues == "function") {
        EnumValues = EnumValues.call(this, this.props.item);
      }
      return h(SelectInput, {
        props: {
          value: this.value,
          values: EnumValues,
          disabled: true,
          multiple: this.info.Type == "Array",
        },
      });
    } else {
      return h("span", null, this.text);
    }
  },
};
</script>

 
