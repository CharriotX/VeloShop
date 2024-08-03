import axios from "axios";

export const API_URL = `https://localhost:7245/api/user`;

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use((config) => {
    if (config.headers) {
        config.headers.Autorization = `Bearer ${localStorage.getItem('token')}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

axios.interceptors.response.use((response) => {
    return response;
}, async (error) => {
    const originarRequest = error.config;
    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        originarRequest._isRetry = true;
        const response = await axios.post(`${API_URL}/refresh`, { withCredentials: true });
        localStorage.setItem("token", response.data.accessToken);
        console.log("response interceptors")
        return $api.request(originarRequest);
    }

    return Promise.reject(error.message);
});


export default $api;