import { createContext } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import Store from './store/store.js'
import CreateProductStore from './store/CreateProductStore.js'
import CreateBrandStore from './store/CreateBrandStore.jsx'

const store = new Store();
const createProductStore = new CreateProductStore();
const createBrandStore = new CreateBrandStore();

export const Context = createContext({
    store,
    createProductStore,
    createBrandStore
})

ReactDOM.createRoot(document.getElementById('root')).render(
    <Context.Provider value={{ store, createProductStore, createBrandStore }}>
        <App />
    </Context.Provider>

)
