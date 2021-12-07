import Vue from 'vue'
import store from '../../store'

/**
 * 处理权限，有权限时才会渲染
 */
Vue.component('permission-facatory', {
    functional: true,
    props: {
        permission: [Array, String]
    },
    render(_, context) {
        if (!context.props.permission ||
            (Array.isArray(context.props.permission) && context.props.permission.length == 0) ||
            store.getters.existsPermission(context.props.permission)) {
               
            return context.children;
        }

         
        return null;
    }
})

/**
 * 用于创建一个虚拟组件，绕开VUE只能创建一个根元素
 */
Vue.component('fragments-facatory', {
    functional: true,
    render(_, context) {
        return context.children;
    }
})


Vue.filter('substring', function (v, len) {
    if (!v) {
        return null;
    }

    if (v.length > len) {
        return `${v.substring(0, len)}...`
    }

    return v;
})

