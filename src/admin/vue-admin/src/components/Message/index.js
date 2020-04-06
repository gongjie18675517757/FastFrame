import Vue from 'vue'
import store from '../../store'
import { alert } from '../../utils'

Vue.prototype.$message = {
    alert() {
        return this.dialog(() => import('./Alert'), ...arguments)
    },
    confirm() {
        return this.dialog(() => import('./Confirm.vue'), ...arguments)
    },
    prompt() {
        return this.dialog(() => import('./Prompt.vue'), ...arguments)
    },
    dialog(component, pars) {
        return new Promise((resolve, reject) => {
            store.state.dialogs.push({
                component,
                resolve,
                reject,
                pars: {
                    ...pars,
                    isDialog: true,

                }
            })
        })
    },
    toast: alert
}