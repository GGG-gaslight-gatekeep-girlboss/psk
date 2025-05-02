import Axios from "axios";
import { showErrorNotification } from "../utils/notifications";

export const api = Axios.create({
  baseURL: "http://localhost:8082",
});

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
