import axios from "axios";
import $api from "../http/index";

const baseUrl = 'https://localhost:7245/api/category/';

export default class CategoryService {
    static async getAll() {
        const response = await axios.get(baseUrl);
        return response;
    }

    static async getById(id) {
        const response = await axios.get(baseUrl + id);
        console.log(id)
        return response;
    }

    static async getSubcategoriesByCategoryId(categoryId) {
        const response = await axios.get('https://localhost:7245/api/subcategory/' + categoryId)
        return response;
    }

    static async getCategoryDataForAddProduct(categoryId) {
        const response = await axios.get(`${baseUrl}data/${categoryId}`)
        return response;
    }
}