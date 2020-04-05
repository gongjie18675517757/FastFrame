import Vue from 'vue'
import store from '../../store'
Vue.prototype.$message = {
    alert() {
        return this.dialog(() => import('./Alert.vue'), ...arguments)
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
    }
}