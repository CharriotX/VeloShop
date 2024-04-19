import axios from "axios";

export const API_URL = `https://localhost:7245/api/user`;

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
})

$api.interceptors.request.use((config) => {
    config.headers.Autorization = `Bearer ${localStorage.getItem('token')}`;
    return config;
})

$api.interceptors.response.use((config) => {   
    return config;
}, async erorr => {
    const originarRequest = erorr.config;
    if (erorr.response.status == 401) {
        try {
            const response = await axios.get(`${API_URL}/refresh`, { withCredentials: true });
            localStorage.setItem("token", response.data.accessToken);
            return $api.request(originarRequest);
        } catch (e) {
            console.log("Пользователь не авторизован")
        }
    }
})

export default $api;