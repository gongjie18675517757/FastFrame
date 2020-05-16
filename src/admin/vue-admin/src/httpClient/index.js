import Vue from 'vue'
import axios from 'axios'
import router from '../router'
import store from '../store'
// import {
//     alert
// } from '../utils';

axios.defaults.baseURL = '';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

/**
 * 格式化提交表单
 * @param {*} data 
 */
function frmPostData(data) {
    if (Array.isArray(data)) {
        return data.map(frmPostData)
    } else if (typeof data == 'object') {
        let keys = Object.keys(data)
        keys = keys.filter(v => /_Id$/.test(v))
        for (const keyName of keys) {
            let key = keyName.replace(/_Id$/, '')
            delete data[key]
        }
    } 
    return data;
}
 

axios.interceptors.request.use(function (config) {
    if (config && config.data && !(config.data instanceof FormData)) {
        let data = frmPostData(JSON.parse(JSON.stringify(config.data)))
        config.data = data;
    }
    return config;
}, function (error) {
    return Promise.reject(error);
});

axios.interceptors.response.use(function (response) { 
    return response.data;
}, function (error) {
    if (error.response && error.response.status) {
        switch (error.response.status) {
            case 401:
                if (store.state.currUser && store.state.currUser.Id) {
                    store.dispatch('logout')
                    store.dispatch('existsIdentity').catch(() => {
                        router.push({
                            path: '/login',
                            query: {
                                redirect: store.state.lastUrl
                            }
                        })
                    })
                }

                break;
            case 400:
            case 403: 
                error=new Error(error.response.data.Message);
                break;
            default:
                break;
        }
    }
    return Promise.reject(error);
});

Vue.prototype.$http = axios;

export default axios;