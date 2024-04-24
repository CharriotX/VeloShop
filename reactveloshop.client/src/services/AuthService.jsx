import $api from "../http/index";

export default class AuthService {
    static async login(email, password) {
        const response = await $api.post("/login", { email, password })
        console.log(response)
        return response;
    }
    static async registration(username, email, password) {
        return await $api.post("/register", { username, email, password });
    }
    static async logout() {
        return await $api.get("/logout");
    }

    static async myProfile() {
        return await $api.get("/profile");
    }

    static async getUsers() {
        const response = await $api.get("/users")
        console.log(response)
        return response;
    }
}