import Vue from 'vue'
import Btn from './Btn'
import store from '../../store'
Vue.component('a-btn', Btn)
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