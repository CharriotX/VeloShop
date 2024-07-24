import axios from "axios";

const baseUrl = 'https://localhost:7245/api/category/';

export default class CategoryService {
    static async getAll() {
        const response = await axios.get(baseUrl);
        return response;
    }

    static async getById(id) {
        const response = await axios.get(baseUrl + id);
        return response;
    }

    static async getSubcategoriesByCategoryId(categoryId) {
        const response = await axios.get('https://localhost:7245/api/subcategory/getSubcategories' + categoryId)
        return response;
    }

    static async getSubcategoryById(subcategoryId) {
        const response = await axios.get('https://localhost:7245/api/subcategory/' + subcategoryId)
        return response;
    }

    static async getCategoryDataForAddProduct(categoryId) {
        const response = await axios.get(`${baseUrl}data/${categoryId}`)
        return response;
    }
}