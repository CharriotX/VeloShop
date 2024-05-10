import axios from "axios";

const baseUrl = 'https://localhost:7245/api/product/';

export default class ProductService {

    static async getProduct(id) {
        const response = await axios.get(baseUrl + id);
        return response;
    }

    static async getAllProductsByCategory(id, page) {
        const response = await axios.get(baseUrl + "category/" + id, {
            params: {
                pageNumber: page
            }
        });
        return response;
    }

    static async getAllProductsBySubcategory(id, page) {
        const response = await axios.get(baseUrl + "subcategory/" + id, {
            params: {
                pageNumber: page
            }
        });
        return response;
    }
}