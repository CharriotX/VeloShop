import Home from "../pages/Home";
import Catalog from "../pages/Catalog";
import CatalogIdPage from "../pages/CatalogIdPage";
import Login from "../pages/Login";
import MyProfile from "../pages/MyProfile";
import Register from "../pages/Register";
import ProductIdPage from "../pages/ProductIdPage";
import SubcategoryIdPage from "../pages/SubcategoryIdPage";
import AdminPage from "../pages/AdminPage";
import AddProductPage from "../pages/AddProductPage";
import UpdateProductPage from "../pages/UpdateProductPage";
import CartPage from "../pages/CartPage";
import NoAccess from "../components/NoAccess";

export const publicRoutes = [
    { id: 1, path: "/", component: Home },
    { id: 2, path: "/catalog", component: Catalog },
    { id: 3, path: "/catalog/:id", component: CatalogIdPage },
    { id: 4, path: "/login", component: Login },
    { id: 5, path: "/registration", component: Register },
    { id: 6, path: "/product/:id", component: ProductIdPage },
    { id: 7, path: "/subcategory/:id", component: SubcategoryIdPage },
    { id: 8, path: "/noAccess", component: NoAccess },
    { id: 9, path: "/cart", component: CartPage },
]

export const privateRoutes = [
    { id: 9, path: "/profile", component: MyProfile },
]

export const adminRoutes = [
    { id: 10, path: "/admin", component: AdminPage },
    { id: 11, path: "/addProduct", component: AddProductPage },
    { id: 12, path: "/updateProduct/:id", component: UpdateProductPage },
]