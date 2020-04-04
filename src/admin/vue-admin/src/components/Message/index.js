import Vue from 'vue'
import {
    showDialog
} from '../../utils';

Vue.prototype.$message = {
    alert() {
        return showDialog(() => import('./Alert.vue'), ...arguments)
    },
    confirm() {
        return showDialog(() => import('./Confirm.vue'), ...arguments)
    },
    prompt() {
        return showDialog(() => import('./Prompt.vue'), ...arguments)
    },
    dialog(component, pars) {
        return showDialog(component, pars)
    }
}