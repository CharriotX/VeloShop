import $api from "../http/index";
import axios from "axios";
const baseProductUrl = 'https://localhost:7245/api/product/';

export default class ProductService {

    static async getProduct(id) {
        const response = await axios.get(baseProductUrl + id);
        return response;
    }

    static async getAll(searchTerm, searchColumn, sortOrder, page, pageSize) {
        var params = new URLSearchParams();
        params.append("searchTerm", searchTerm);
        params.append("searchColumn", searchColumn);
        params.append("sortOrder", sortOrder);
        params.append("pageSize", pageSize);
        params.append("page", page);

        var request = {
            params: params
        }

        const response = await axios.get(baseProductUrl, request);
        return response;
    }

    static async getAllProductsByCategory(id, page) {
        const response = await axios.get(baseProductUrl + "category/" + id, {
            params: {
                pageNumber: page
            }
        });

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
        return response;
    }

    static async getProductsByBikeCategory() {
        const response = await axios.get(`${baseProductUrl}bikeCategory`)
        return response;
    }

    static async createProduct(data) {
        try {
            await axios.post('https://localhost:7245/api/product/', JSON.stringify(data), {
                headers: {
                    "Content-Type": "application/json"
                }
            });
        } catch (e) {
            console.log(e)
        }
    }

    static async updateProduct(data) {
        try {
            await axios.patch('https://localhost:7245/api/product/' + data.id, JSON.stringify(data), {
                headers: {
                    "Content-Type": "application/json"
                }
            });
        } catch (e) {
            console.log(e)
        }
    }

    static async deleteProduct(id) {
        try {
            await axios.delete(baseProductUrl + id)
        } catch (e) {
            console.log(e)
        }
    }
}