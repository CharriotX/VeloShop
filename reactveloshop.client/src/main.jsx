import { StrictMode, createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import Store from './store/store.js'
import ProductDataStore from './store/ProductDataStore.js'
import CreateBrandStore from './store/CreateBrandStore.jsx'
import CartStore from './store/CartStore.jsx'

const store = new Store();
const productDataStore = new ProductDataStore();
const createBrandStore = new CreateBrandStore();
const cartStore = new CartStore();

export const Context = createContext({
    store,
    productDataStore,
    createBrandStore,
    cartStore
})

ReactDOM.createRoot(document.getElementById('root')).render(
    <StrictMode>
        <Context.Provider value={{ store, productDataStore, createBrandStore, cartStore }}>
            <App />
        </Context.Provider>
    </StrictMode>
)
