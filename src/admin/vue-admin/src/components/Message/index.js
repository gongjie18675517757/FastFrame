import Vue from 'vue'
import store from '../../store'
import { alert } from '../../utils'
import Alert from './Alert.vue'
import Confirm from './Confirm.vue'
import Prompt from './Prompt.vue'

window.alert=alert;

Vue.prototype.$message = {
    alert(pars) {
        return this.dialog(Alert, {
            width: '600px',
            ...pars
        })
    },
    confirm(pars) {
        return this.dialog(Confirm, {
            width: '600px',
            ...pars
        })
    },
    prompt() {
        return this.dialog(Prompt, ...arguments)
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