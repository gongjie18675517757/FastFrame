import Vue from 'vue'
import store from '../../store'
import Alert from './Alert.vue'
import Confirm from './Confirm.vue'
import Prompt from './Prompt.vue'
import Choose from './Choose.vue'


const message = {
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
    choose(pars) {
        return this.dialog(Choose, {
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
                ...pars,
                resolve,
                reject,
                pars: {
                    ...pars,
                    isDialog: true,

                }
            })
        })
    },
    toast: {
        /**
         * 显示
         * @param {*} param0 
         * @returns 
         */
        show({
            type = "success",
            msg = "",
            timeout = 5000,
            color
        }) {
            let id = new Date().getTime().toString();
            store.state.toasts.push({
                type,
                msg,
                timeout,
                color,
                id,
                onClose: this.close
            });
            setTimeout(this.close, timeout, id);
            return id;
        },
        /**
         * 关闭
         * @param {*} id 
         */
        close(id) {
            let index = store.state.toasts.findIndex(v => v.id == id);
            if (index > -1) {
                store.state.toasts.splice(index, 1)
            }
        },
        /**
         * 错误
         * @param {*} msg 
         */
        error(msg) {
            this.show({
                type: 'error',
                msg
            })
        },
        /**
         * 成功
         * @param {*} msg 
         */
        success(msg) {
            this.show({
                type: 'success',
                msg
            })
        },
        /**
         * 警告
         * @param {*} msg 
         */
        warning(msg) {
            this.show({
                type: 'warning',
                msg
            })
        },
        /**
         * 信息
         * @param {*} msg 
         */
        info(msg) {
            this.show({
                type: 'alert',
                msg
            })
        }
    },
    notify: {
        /**
         * 显示
         * @param {*} param0 
         * @returns 
         */
        show(pars) {
            store.state.newNotifys.splice(0, 0, {
                ...pars,
                onClose: this.close
            });
        },
        /**
         * 关闭
         * @param {*} id 
         */
        close(id) {
            let index = store.state.newNotifys.findIndex(v => v.Id == id);
            if (index > -1) {
                store.state.newNotifys.splice(index, 1)
            }
        }
    }
}

Vue.prototype.$message = message;

export default message;