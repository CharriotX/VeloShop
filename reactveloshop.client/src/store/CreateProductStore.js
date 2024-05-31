import { makeAutoObservable} from 'mobx';
import CategoryService from '../services/CategoryService';

export default class CreateProductStore {

    brand = {};
    brands = [];

    category = {};
    categories = [];

    subcategory = {};
    subcategories = [];

    specifications = [];
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
        this.category = category;
    }

    setCategories(categories) {
        this.categories = categories;
    }

    setSelectedSubcaregory(subcategory) {
        this.subcategory = subcategory;
    }

    setSubcategories(subcategories) {
        this.subcategories = subcategories;
    }

    setSpecifications(specifications) {
        this.specifications = specifications;
    }



    async getCategoryDataForAddProduct(id) {
        try {
            const response = await CategoryService.getCategoryDataForAddProduct(id);
            this.setSelectedCaregory(response.data);
            this.setSubcategories(response.data.subcategories);
            this.setBrands(response.data.brands);
            this.setSpecifications(response.data.specifications);
        } catch (e) {
            console.log(e)
        }
    }

    async getAllCategories() {
        const response = await CategoryService.getAll();
        this.setCategories(response.data)
    }
}