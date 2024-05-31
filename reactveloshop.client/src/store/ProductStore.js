import { makeAutoObservable } from 'mobx';
import AuthService from '../services/AuthService';
import axios from 'axios';

export default class DeviceStore {
    
    constructor() {
        this._category = {};
        this._brands = [];
        this._products = [];
        makeAutoObservable(this)
    }

    setCaregory(category) {
        this._category = category;
    }

    setBrands(brands) {
        this._brands = brands;
    }

    setProducts(products) {
        this._products = products;
    }
}