import Vue from 'vue'
import store from '../../store'

/**
 * 处理权限，有权限时才会渲染
 */
Vue.component('permission-facatory', {
    functional: true,

    render(_, context) {
        const { props } = context;
        let { permission } = props;
        if (!permission)
            return context.children;


        if (permission && !Array.isArray(permission))
            permission = [permission]


        const has_permission = store.getters.existsPermission(permission) 
        if (has_permission) {
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

