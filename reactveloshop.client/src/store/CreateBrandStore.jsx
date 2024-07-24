import { makeAutoObservable } from 'mobx';
import CategoryService from '../services/CategoryService';

export default class CreateBrandStore {
    brand = {};
    brands = [];

    category = 1;
    categories = [];
    constructor() {
        makeAutoObservable(this)
    }

    setSelectedBrand(brand) {
        this.brand = brand;
    }

    setBrands(brands) {
        this.brands = brands;
    }

    setSelectedCaregory(category) {
        this.category = Number(category);
    }

    setCategories(categories) {
        this.categories = categories;
    }

    async getAllBrandsByCategory(id) {
        try {
            const response = await CategoryService.getCategoryDataForAddProduct(id);
            this.setSelectedCaregory(response.data.id);
            this.setBrands(response.data.brands);
        } catch (e) {
            console.log(e)
        }
    }

    async getAllCategories() {
        const response = await CategoryService.getAll();
        this.setCategories(response.data)
    }
}