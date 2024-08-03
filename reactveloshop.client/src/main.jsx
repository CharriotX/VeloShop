import { StrictMode, createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import AuthStore from './store/AuthStore.js'
import ProductDataStore from './store/ProductDataStore.js'
import CreateBrandStore from './store/CreateBrandStore.jsx'
import CartStore from './store/CartStore.jsx'


const authStore = new AuthStore();
const productDataStore = new ProductDataStore();
const createBrandStore = new CreateBrandStore();
const cartStore = new CartStore();

export const Context = createContext({
    authStore,
    productDataStore,
    createBrandStore,
    cartStore
})

ReactDOM.createRoot(document.getElementById('root')).render(
    <Context.Provider value={{ authStore, productDataStore, createBrandStore, cartStore }}>
        <App />
    </Context.Provider>
)
