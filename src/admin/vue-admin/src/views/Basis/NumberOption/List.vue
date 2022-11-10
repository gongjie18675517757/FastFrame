<script>
let pageInfo = { area: "Basis", name: "NumberOption", direction: "编号设置" };
import Page, {
  PageMethodsDefinds,
} from "../../../components/Page/ListPageCore.js";
import { getHasNumberModules } from "../../../generate.js";

/**
 * 合并两个vue定义的methods
 */
function mergeMethods(obj_source, obj_target) {
  const source_arr = Object.entries(obj_source);
  const target_arr = Object.entries(obj_target);

  const result_obj = {};
  const merge_arr = source_arr.filter((v) =>
    target_arr.some((x) => x[0] == v[0])
  );

  /**
   * 先处理有合并的
   */
  for (const [k, func] of merge_arr) {
    const result_func = function () {
      const result = func.call(this, ...arguments);
      const target_func = obj_target[k];

      /**
       * 如果没有合并项,则退出
       */
      if (!target_func) return result;

      /**
       * 异步处理办法
       */
      if (result && typeof result == "object" && result instanceof Promise) {
        return result.then(target_func.bind(this));
      }

      /**
       * 同步时处理办法
       */
      return target_func.bind(this)(result);
    };
    result_obj[k] = result_func;
  }

  for (const [k, func] of source_arr) {
    if (obj_target[k]) continue;

    result_obj[k] = func;
  }

  for (const [k, func] of target_arr) {
    if (obj_source[k]) continue;

    result_obj[k] = func;
  }

  return result_obj;
}

/**
 * 合并两个vue定义
 */
function mergeVueComponentInstance(base_vue_instance, child_vue_instance) {
  return {
    ...base_vue_instance,
    props: {
      ...base_vue_instance.props,
      ...(child_vue_instance.props || {}),
    },
    data() {
      const data = base_vue_instance.data.call(this);
      const child_data_func =
        child_vue_instance.data ||
        function () {
          return {};
        };
      const child_data = child_data_func.call(data);
      return {
        ...data,
        ...child_data,
      };
    },
    computed: {
      ...base_vue_instance.computed,
      ...(child_vue_instance.computed || {}),
    },
    watch: {
      ...base_vue_instance.watch,
      ...(child_vue_instance.watch || {}),
    },
    methods: {
      ...mergeMethods(
        base_vue_instance.methods,
        child_vue_instance.methods || {}
      ),
    },
  };
}


export default mergeVueComponentInstance(Page, {
  data() {
    return {
      ...pageInfo,
    };
  },
  methods: {
    async [PageMethodsDefinds.fmtColumns](arr) {
      const EnumValues = await getHasNumberModules();
      return [
        ...arr,
        {
          Name: "BeModule",
          EnumValues,
        },
      ];
    },
  },
});
</script>
