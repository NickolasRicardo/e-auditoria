import axios, { Axios } from 'axios';

const api: Axios = axios.create({
  baseURL: 'http://localhost:5000/',
});

api.interceptors.response.use(
  response => {
    if (response.status === 401 || response.status === 403) {
      localStorage.clear();
      window.location.href = '/';
    }
    return response;
  },
  error => {
    if (error.response) {
      if (error.response.status === 401 || error.response.status === 403) {
        localStorage.clear();
        window.location.href = '/';
      }
      return Promise.reject(error.response);
    }

    return Promise.reject(error);
  }
);

export default api;
