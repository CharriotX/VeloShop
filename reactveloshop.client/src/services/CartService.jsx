import axios from "axios";

const baseUrl = 'https://localhost:7245/api/cart/';

export default class CartService {
    static async getCart() {
        const response = await axios.get(baseUrl, { withCredentials: true });
        return response;
    }

    static async addProductToCart(productId) {
        try {
            const response = await axios.post("https://localhost:7245/api/cart", null, {
                params: {
                    productId
                },
                headers: {
                    "Content-Type": "application/json"
                },
                withCredentials: true
            });

            return response;
        } catch (e) {
            console.log(e)
        }
    }

    static async removeItem(productId) {
        try {
            const response = await axios.patch(`https://localhost:7245/api/cart`, null, {
                params: {
                    productId
                },
                headers: {
                    "Content-Type": "application/json"
                },
                withCredentials: true
            });

            return response;
        } catch (e) {
            console.log(e)
        }
    }

    static async cleanCart() {
        try {
            await axios.delete(`https://localhost:7245/api/cart`, {
                headers: {
                    "Content-Type": "application/json"
                },
                withCredentials: true
            });
        } catch (e) {
            console.log(e)
        }
    }
}