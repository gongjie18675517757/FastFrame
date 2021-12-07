import Vue from 'vue'
import store from '../../store'
import Alert from './Alert.vue'
import Confirm from './Confirm.vue'
import FormPageCore from '../Page/FormPageCore'
import Choose from './Choose.vue'



/**
 * 生成弹窗表单工厂
 * @param {*} pars 
 * @returns 
 */
export function makePromptPageFacatory(pars) {
    return {
        ...FormPageCore,
        data() {
            const data = FormPageCore.data.call(this);
            return {
                ...data,
                direction: pars.title,
                singleLine: typeof (pars.singleLine) == 'boolean' ? pars.singleLine : true,
                manageOptions: [],
            }
        },
        methods: {
            ...FormPageCore.methods,
            async init() {
                await FormPageCore.methods.init.call(this, ...arguments);
                this.canEdit = true;
            },
            getModuleStrut() {
                return {
                    hasManage: true
                }
            },
            getRules() {
                return pars.rules;
            },
            getModelObject() {
                return pars.model;
            },
            getModelObjectItems() {
                return pars.options;
            },
            getToolItems() {
                return [
                    {
                        title: "确认",
                        color: "primary",
                        key: 'save',
                        iconName: "mdi-content-save-edit-outline",
                        action: this.submit,
                        visible: () => this.canEdit,

                    },
                ]
            },
            getPostMethod() {
                if (typeof (pars.submitFunc) == 'function') {
                    return (_url, model) => pars.submitFunc(model);
                }

                return (_url, model) => model;
            },
            onSaveAfter(res) {
                let model = this.model;
                this.$emit('success', { model, res })
            },

        }
    }
}

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
    prompt(pars) {
        return this.dialog(makePromptPageFacatory(pars), pars)
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