import axios from "axios";
import $api from "../http/index";

const baseUrl = 'https://localhost:7245/api/brand/';

export default class BrandService {
    static async getAll() {
        const response = await axios.get(baseUrl);
        return response;
    }

    static async getByName(name) {
        const response = await axios.get(baseUrl + name);
        return response;
    }

    static async getAllBrandsByCategoryId(id) {
        const response = await axios.get(`${baseUrl}category/${id}`);
        return response;
    }

    static async createBrand(data) {
        const response = await axios.post(`${baseUrl}`, JSON.stringify(data), {
            headers: {
                "Content-Type": "application/json"
            }
        });
        return response;
    }
}