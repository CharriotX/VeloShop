import { makeAutoObservable } from 'mobx';
import CartService from '../services/CartService';

export default class CartStore {

    cart = 0;
    cartItems = [];
    totalPrice = 0;
    productCount = 0;

    constructor() {
        makeAutoObservable(this)
    }

    setCart(cart) {
        this.cart = cart;
    }
    setTotalPrice(total) {
        this.totalPrice = total;
    }

    setCartItems(cartItems) {
        this.cartItems = cartItems;
    }

    setProductCount(productCount) {
        this.productCount = productCount;
    }

    async getProductCount() {
        return this.productCount;
    }

    async getCart() {
        try {
            var response = await CartService.getCart();
            this.setCart(response.data.id)
            this.setCartItems(response.data.items)
            this.setTotalPrice(response.data.total)
            this.setProductCount(response.data.items.length)
        } catch (e) {
            console.log(e)
        }
    }

    async addProductToCart(id) {
        try {
            var response = await CartService.addProductToCart(id);
            this.setTotalPrice(response.data.total)
            this.setCartItems(response.data.items)
            this.setProductCount(response.data.itemsCount)
        } catch (e) {
            console.log(e)
        }
    }

    async removeItem(id) {
        try {
            var response = await CartService.removeItem(id);
            this.setTotalPrice(response.data.total)
            this.setCartItems(response.data.items)
            this.setProductCount(response.data.itemsCount)
        } catch (e) {
            console.log(e)
        }
    }

    async cleanCart() {
        try {
            await CartService.cleanCart();
            this.setTotalPrice(0)
            this.setCartItems([])
            this.setProductCount(0)
        } catch (e) {
            console.log(e)
        }
    }

}