import axios from 'axios'
import router from '@/router'
import store from '@/store'
import {
  alert
} from './utils';

axios.defaults.baseURL = '';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';



axios.interceptors.request.use(function (config) {
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