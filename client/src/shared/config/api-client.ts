import Axios, { InternalAxiosRequestConfig } from "axios";
import { showErrorNotification } from "../utils/notifications";

export const api = Axios.create({
  baseURL: "http://localhost:8082",
});

function authRequestInterceptor(config: InternalAxiosRequestConfig) {
  if (config.headers) {
    config.headers.Accept = "application/json";
  }

  config.withCredentials = true;
  return config;
}

api.interceptors.request.use(authRequestInterceptor);

api.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    const message = error.response?.data?.errorMessage || error.message;
    showErrorNotification({ message });

    return Promise.reject(error);
  }
);
