import ListPage, { PageMethodsDefines as ListMethodsDefines, PageComputedDefines as ListComputedDefines, PageDataDefines as ListDataDefines } from './ListPageCore'
import FormPage, { FormPageDataDefines, FormPageComputedDefines, FormPageMethodsDefines } from './FormPageCore'
/**
 * 合并两个vue定义的methods
 */
export function mergeMethods(obj_source, obj_target) {
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
                return result.then(r=>target_func.bind(this)(r,...arguments));
            }

            /**
             * 同步时处理办法
             */
            return target_func.bind(this)(result,...arguments);
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
export function mergeVueComponentInstance(base_vue_instance, child_vue_instance) {
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

/**
 * 列表页面的定义
 */
export const ListPageDefines = {
    /**
     * 页面所有方法的定义
     */
    MethodsDefines: ListMethodsDefines,

    /**
     * 页面所有计算属性的定义
     */
    ComputedDefines: ListComputedDefines,

    /**
     * 页面所有数据的定义
     */
    DataDefines: ListDataDefines
}

/**
 * 表单页面的定义
 */
export const FormPageDefines = {
    /**
     * data
     */
    DataDefines: FormPageDataDefines,

    /**
     * computed
     */
    ComputedDefines: FormPageComputedDefines,

    /**
     * methods
     */
    MethodsDefines: FormPageMethodsDefines
}

/**
 * 根据父类列表页生成个性化列表页面
 * @param {*} instance 传入重写的内容，如果父类有的方法/属性，会把结果作为入参传过来
 * @returns 
 */
export function makeListPageInheritedFromBaseListPage(instance) {
    return mergeVueComponentInstance(ListPage, instance)
}

/**
 * 根据父类列表页生成个性化表单页面
 * @param {*} instance 传入重写的内容，如果父类有的方法/属性，会把结果作为入参传过来
 * @returns 
 */
 export function makeFormPageInheritedFromBaseFormPage(instance) {
    return mergeVueComponentInstance(FormPage, instance)
}