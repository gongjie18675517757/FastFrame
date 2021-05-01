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
        if (store.getters.existsPermission(context.props.permission)) {
            return context.children;
        }
        return null;
    }
})