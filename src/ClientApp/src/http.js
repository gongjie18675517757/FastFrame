import axios from 'axios'
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
  if (error.response && error.response.data) {
    let data = error.response.data
    for (const key of Object.keys(data)) {
      let item = data[key]
      if (Array.isArray(item)) {
        data[key].forEach(err => {
          alert.error(err)
        })
      } else {
        alert.error(item)
      }
    }
  }

  return Promise.reject(error);
});

export default axios;