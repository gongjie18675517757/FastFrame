import Vue from 'vue'
import store from '../../store'
import { alert } from '../../utils'

Vue.prototype.$message = {
    alert(pars) {
        return this.dialog(() => import('./Alert'), {
            width: '600px',
            ...pars
        })
    },
    confirm(pars) {
        return this.dialog(() => import('./Confirm.vue'), {
            width: '600px',
            ...pars
        })
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