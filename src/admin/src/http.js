import axios from 'axios'
import router from '@/router'
import store from '@/store'
import {
  alert
} from './utils';

axios.defaults.baseURL = '';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';


function frmPostData(data) {
  if (Array.isArray(data)) {
    for (const item of data) {
      return frmPostData(item)
    }
  } else if (typeof data == 'object') {
    let keys = Object.keys(data)
    keys = keys.filter(v => /_Id$/.test(v))
    for (const key of keys) {
      key = key.replace(/_Id$/, '')
      delete data[key]
    }
  }

  return data;
}

axios.interceptors.request.use(function (config) {
  if (config && config.data) {
    let data = frmPostData(JSON.parse(JSON.stringify(config.data)))
    config.data=data;
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
        router.push({
          path: '/login',
          query: {
            redirect: store.state.lastUrl
          }
        })
        break;
      case 400:
      case 403:
        if (error.response.data.Message)
          alert.error(error.response.data.Message)
        break;
      default:
        break;
    }
  }
  return Promise.reject(error);
});

export default axios;