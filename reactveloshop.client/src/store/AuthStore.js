import { makeAutoObservable } from 'mobx';
import AuthService from '../services/AuthService';
import $api from '../http/index';
import axios from "axios";

export default class AuthStore {
    user = { username: "", email: "", role: "" };
    isLoading = true;
    isAuth = false;
    isAdmin = false;

    constructor() {
        makeAutoObservable(this)
    }

    setAuth(bool) {
        this.isAuth = bool;
    }

    setUser(user) {
        this.user = user;
    }
    setIsAdmin(bool) {
        this.isAdmin = bool;
    }

    setIsLoading(bool) {
        this.isLoading = bool;
    }

    async login(email, password) {
        try {
            const response = await AuthService.login(email, password);
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
            if (response.data.userData.role === "Admin") {
                this.setIsAdmin(true)
            }
        } catch (e) {
            console.log(e)
        }
    }

    async registration(username, email, password) {
        try {
            const response = await AuthService.registration(username, email, password);
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
            if (response.data.userData.role === "Admin") {
                this.setIsAdmin(true)
            }
        } catch (e) {
            console.log(e)
        }
    }

    async logout() {
        try {
            await AuthService.logout();
            localStorage.removeItem("token");
            this.setAuth(false);
            this.setUser({});
            this.setIsAdmin(false)
        } catch (e) {
            console.log(e)
        }
    }

    async checkAuth() {
        try {
            const token = localStorage.getItem('token')
            const response = await axios.post(`https://localhost:7245/api/user/refresh`, { accessToken: token }, { withCredentials: true })
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
            if (response.data.userData.role === "Admin") {
                this.setIsAdmin(true)
            }
        } catch (e) {
            console.log(e)
        }
    }

    async myProfile() {
        try {
            const token = localStorage.getItem('token')
            const response = await $api.get(`https://localhost:7245/api/user/profile`, { accessToken: token }, { withCredentials: true })
            this.setAuth(true);
            this.setUser(response.data.userData);
        } catch (e) {
            console.log(e)
        }
    }
}