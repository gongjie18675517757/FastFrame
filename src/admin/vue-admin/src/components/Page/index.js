import ListPage, { PageMethodsDefines as ListMethodsDefines, PageComputedDefines as ListComputedDefines, PageDataDefines as ListDataDefines } from './ListPageCore'
import FormPage, { FormPageDataDefines, FormPageComputedDefines, FormPageMethodsDefines } from './FormPageCore'


export const FormPageCore = FormPage
export const ListPageCore = ListPage

/**
 * 合并两个vue定义的methods
 * @param {*} obj_source 父类的methods
 * @param {*} obj_target 子类的methods
 * @returns 
 */
export function mergeMethods(obj_source, obj_target) {
    const source_arr = Object.entries(obj_source);
    const target_arr = Object.entries(obj_target);

    const result_obj = {};
    /**
     * 存放父类的被子类重写过的方法
     */
    const merge_arr = source_arr.filter((v) =>
        target_arr.some((x) => x[0] == v[0])
    );

    /**
     * 先处理有重写的
     */
    for (const [k, func] of merge_arr) {
        /**
         * 定义一个新的工厂函数包装调用
         * @returns 
         */
        result_obj[k] = function () {

            /**
             * 子类的具体方法
             */
            const target_func = obj_target[k].bind(this);

            /**
             * 包装父类的具体方法
             * @returns 返回父类的执行结果
             */
            const super_func = () => func.bind(this)(...arguments)

            /**
             * 返回子类的方法，并把父类的方法做为第一个参数传入
             */
            return target_func(super_func, ...arguments);
        };
    } 
    
    /**
     * 父类的方法未被重写过的
     */
    for (const [k, func] of source_arr) {
        if (obj_target[k]) continue;

        result_obj[k] = func;
    }

    /**
     * 子类自己定义的方法
     */
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