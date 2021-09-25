import Vue from 'vue'
import axios from 'axios'
import router from '../router'
import store from '../store'
import $message from '../components/Message'

axios.defaults.baseURL = '';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

/**
 * 格式化提交表单
 * @param {*} data 
 */
function frmPostData(data) {
    if (data instanceof FormData) {
        return data;
    }
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
    /**
     * 自动对GET请求增加版本号
     */
    if (config.method == 'get') {
        let pars = `v=${new Date().getTime()}`;
        if (!config.url.includes('?'))
            pars = `?${pars}`
        else
            pars = `&${pars}`
        config.url = `${config.url}${pars}`
    }

    /**
     * 格式化表单
     */
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
                if (store.state.currUser && store.state.currUser.Id && router.history.current.path != '/login') {
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
            case 404:
                $message.toast.error('请求资源丢失,提尝试刷新或者联系您的实施工程师!') 
                break;
            case 400:
            case 403:
                $message.toast.error(error.response.data.Message)
                error = new Error(error.response.data.Message);
                break;
            case 500:
            default:
                break;
        }
    }
    return Promise.reject(error);
});

Vue.prototype.$http = axios;

export default axios;