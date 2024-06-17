import { makeAutoObservable, toJS } from 'mobx';
import ProductService from '../services/ProductService';
import axios from 'axios';

export default class AdminStore {

    brands = [];
    categories = [];
    products = {};

    constructor() {
        makeAutoObservable(this)
    }

    setCaregory(categories) {
        this.categories = categories;
    }

    setBrands(brands) {
        this._brands = brands;
    }

    setProducts(products) {
        this.products = products;
    }

    async GetProducts(searchTerm, searchColumn, sortOreder, page) {
        try {
            var response = await ProductService.getAll(searchTerm, searchColumn, sortOreder, page)
            this.setProducts(response.data)
        } catch (e) {
            console.log(e)
        }
    }

}