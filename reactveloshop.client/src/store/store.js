import { makeAutoObservable } from 'mobx';
import AuthService from '../services/AuthService';
import axios from 'axios';

export default class Store {
    user = {username:"", email: "" };
    isAuth = false;

    constructor() {
        makeAutoObservable(this)
    }

    setAuth(bool) {
        this.isAuth = bool;
    }

    setUser(user) {
        this.user = user;
    }

    async login(email, password) {
        try {
            const response = await AuthService.login(email, password);
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
        } catch {
        }
    }

    async registration(username, email, password) {
        try {
            const response = await AuthService.registration(username, email, password);
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
        } catch (e) {
            console.log(e.response.data.message)
        }
    }

    async logout() {
        try {
            await AuthService.logout();
            localStorage.removeItem("token");
            this.setAuth(false);
            this.setUser({});
        } catch (e) {
            console.log(e.response.data.message)
        }
    }

    async checkAuth() {
        try {
            const token = localStorage.getItem('token')
            const response = await axios.post(`https://localhost:7245/api/user/refresh`, { accessToken: token }, { withCredentials: true })
            localStorage.setItem('token', response.data.accessToken)
            this.setAuth(true);
            this.setUser(response.data.userData);
        } catch (e) {
            
        }        
    }
}