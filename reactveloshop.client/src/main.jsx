import { createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import Store from './store/store.js'
import ProductDataStore from './store/ProductDataStore.js'
import CreateBrandStore from './store/CreateBrandStore.jsx'
import AdminStore from './store/AdminStore.jsx'
import ProductStore from './store/ProductStore.js'

const store = new Store();
const productDataStore = new ProductDataStore();
const createBrandStore = new CreateBrandStore();
const adminStore = new AdminStore();
const productStore = new ProductStore();

export const Context = createContext({
    store,
    productStore,
    productDataStore,
    createBrandStore,
    adminStore
})

ReactDOM.createRoot(document.getElementById('root')).render(
    <Context.Provider value={{ store, productDataStore, createBrandStore, adminStore, productStore }}>
        <App />
    </Context.Provider>

)
