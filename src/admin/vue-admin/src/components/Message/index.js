import Vue from 'vue'
import store from '../../store'
import AlertComponent from './Alert.vue'
import ConfirmComponent from './Confirm.vue'
import { FormPageDefines, FormPageCore } from '../Page'
import Choose from './Choose.vue'
import { guid } from '../../utils'



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
                [FormPageDefines.DataDefines.direction]: pars.title,
                [FormPageDefines.DataDefines.singleLine]: typeof (pars.singleLine) == 'boolean' ? pars.singleLine : true,
                [FormPageDefines.DataDefines.manageOptions]: [],
                [FormPageDefines.DataDefines.pageFormCols]: 1,
            }
        },
        methods: {
            ...FormPageCore.methods,
            async [FormPageDefines.DataDefines.init]() {
                await FormPageCore.methods.init.call(this, ...arguments);
                this.canEdit = true;
            },
            [FormPageDefines.DataDefines.getModuleStrut]() {
                return {
                    hasManage: true
                }
            },
            [FormPageDefines.DataDefines.getRules]() {
                return pars.rules;
            },
            [FormPageDefines.DataDefines.getModelObject]() {
                return pars.model;
            },
            [FormPageDefines.DataDefines.getModelObjectItems]() {
                return pars.options;
            },
            [FormPageDefines.DataDefines.getToolItems]() {
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
            [FormPageDefines.DataDefines.getPostMethod]() {
                if (typeof (pars.submitFunc) == 'function') {
                    return (_url, model) => pars.submitFunc(model);
                }

                return (_url, model) => model;
            },
            [FormPageDefines.DataDefines.onSaveAfter](res) {
                let model = this.model;
                this.$emit('success', { model, res })
            },

        }
    }
}

const message = {
    alert(pars) {
        return this.dialog(AlertComponent, {
            width: '600px',
            ...pars
        })
    },
    confirm(pars) {
        return this.dialog(ConfirmComponent, {
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
            const dialogItem = {
                component,
                ...pars,
                resolve,
                reject,
                _visible: true,
                key: guid(),
                pars: {
                    ...pars,
                    isDialog: true,
                }
            }
            store.state.dialogs.push(dialogItem)

            // setTimeout(() => {
            //     dialogItem._visible = true;
            // }, 100);
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
            timeout = 2000,
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