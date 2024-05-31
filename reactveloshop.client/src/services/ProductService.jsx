import $api from "../http/index";
import axios from "axios";
const baseProductUrl = 'https://localhost:7245/api/product/';

export default class ProductService {

    static async getProduct(id) {
        const response = await axios.get(baseProductUrl + id);
        return response;
    }

    static async getAllProductsByCategory(id, page) {
        const response = await axios.get(baseProductUrl + "category/" + id, {
            params: {
                pageNumber: page
            }
        });
        console.log(response)
        return response;
    }

    static async getAllProductsBySubcategory(id, page) {
        const response = await axios.get(baseProductUrl + "subcategory/" + id, {
            params: {
                pageNumber: page
            }
        });

        return response;
    }

    static async getProductsByCategory(id) {
        const response = await axios.get(`${baseProductUrl}/category/${id}`)
        console.log(response);
        return response;
    }

    static async createProduct(data) {
        try {
            console.log(data)
            const response = await axios.post('https://localhost:7245/api/product/', JSON.stringify(data), {
                headers: {
                    "Content-Type": "application/json"
                }
            });

            console.log(response)
        } catch (e) {
            console.log(e)
        }

    }
}