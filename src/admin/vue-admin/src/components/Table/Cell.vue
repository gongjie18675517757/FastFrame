 <script>
import { getValue } from "@/utils";
import EnumItemInput from "@/components/Inputs/EnumItemInput";
import SelectInput from "@/components/Inputs/SelectInput.vue";
export default {
  props: {
    info: {
      type: Object,
      default: function() {
        return {};
      }
    },
    model: {
      type: Object,
      default: function() {
        return {};
      }
    },
    moduleName: String
  },
  data() {
    return {};
  },
  computed: {
    isRelate() {
      return !!this.info.Relate;
    },
    isFile() {
      return this.isRelate && this.info.Relate.ModuleName == "Resource";
    },
    val() {
      return getValue(this.model, this.info.Name);
    },
    Foreignkey() {
      return this.isRelate ? this.val : "";
    },
    value() {
      let val = this.val;
      let length = this.info.Length || 0;
      if (length >= 4000) return val.replace(/<[^>]+>/g, "").substring(0, 200);
      if (this.info.Type == "Boolean") {
        if (val) return "是";
        else return "否";
      }
      if (this.info.EnumValues && this.info.EnumValues.length > 0) {
        let kv = this.info.EnumValues.find(r => r.Key == val) || {};
        return kv.Value || "";
      }
      if (this.info.Relate) {
        let tempName = this.info.Name.replace("_Id", "");
        let obj = this.model[tempName];
        if (obj) {
          return this.info.Relate.RelateFields.map(v => obj[v])
            .map((v, i) => (i > 0 ? `[${v}]` : v))
            .join("");
        }
      }
      return val;
    }
  },
  methods: {
    toRelate() {
      if (this.isFile && this.val) {
        let url = `/api/resource/get/${this.val}`;
        window.open(url);
        return;
      }
    }
  },
  render(h) {
    let { IsLink, EnumItemInfo, EnumValues, getValueFunc, render } = this.info;
    if (typeof render == "function") {
      return render(h, { value: this.val, model: this.model, info: this.info });
    }
    else if (typeof getValueFunc == "function") {
      return h(
        "span",
        null,
        getValueFunc({ value: this.val, model: this.model, info: this.info })
      );
    }
    else if (IsLink) {
      return h(
        "a",
        {
          on: {
            click: () => this.$emit("toEdit", this.model)
          }
        },
        this.value
      );
    } else if (this.isFile) {
      return h(
        "a",
        {
          on: {
            click: this.toRelate
          }
        },
        this.value
      );
    } else if (EnumItemInfo) {
      return h(EnumItemInput, {
        props: {
          value: this.val,
          EnumItemInfo,
          disabled: true,
          multiple: this.info.Type == "Array"
        }
      });
    } else if (
      (Array.isArray(EnumValues) && EnumValues.length > 0) ||
      typeof EnumValues == "function"
    ) {
      if (typeof EnumValues == "function") {
        EnumValues = EnumValues.call(this, this.model);
      }
      return h(SelectInput, {
        props: {
          value: this.val,
          values: EnumValues,
          disabled: true,
          multiple: this.info.Type == "Array"
        }
      });
    } else {
      return h("span", null, this.value);
    }
  }
};
</script>

 
