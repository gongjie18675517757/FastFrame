<script>
import { setValue } from "../../utils";
import Combobox from "./Combobox.vue";
export default {
  name:'v-search-input',
  functional: true,
  render(h, context) {
    const { props, listeners, children } = context;
    const { model, RelateKeyFieldName, Name } = props;
    return h(
      Combobox,
      {
        props: {
          ...props,
          itemText: "Value",
          itemValue: "Value",
        },
        on: {
          ...listeners,
          change: (kv) => {
            if (RelateKeyFieldName && model) {
              setValue(model, RelateKeyFieldName, null);
              if (kv && kv.Id) {
                setValue(model, RelateKeyFieldName, kv.Id);
              } else {
                setValue(model, Name, null);
              }
            }

            listeners && listeners.change(kv);
          },
        },
      },
      children
    );
  },
};
</script>

<style>
</style>