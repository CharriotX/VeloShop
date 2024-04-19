import axios from "axios";

const baseUrl = 'https://localhost:7245/api/category/';

export default class CategoryService {
    static async getAll() {
        const response = await axios.get(baseUrl);
        return response;
    }

    static async getById(id) {
        const response = await axios.get('https://localhost:7245/api/category/' + id);
        return response;
    }

    static async getSubcategoriesByCategoryId(categoryId) {
        const responce = await axios.get('https://localhost:7245/api/subcategory/' + categoryId)
        return responce;
    }
}