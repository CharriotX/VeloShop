import { makeAutoObservable } from 'mobx';
import ProductService from '../services/ProductService';


export default class ProductStore {
    product = {};
    constructor() {

        makeAutoObservable(this)
    }

    setProduct(product) {
        this._product = product;
    }

    async getProduct(id) {
        try {
            const response = await ProductService.getProduct(id);
            console.log(response)
            this.setProduct(response.data)
        } catch (e) {
            console.log(e)
        }
    }
}