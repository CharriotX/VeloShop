import Home from "../pages/Home";
import Catalog from "../pages/Catalog";
import CatalogIdPage from "../pages/CatalogIdPage";
import Login from "../pages/Login";
import MyProfile from "../pages/MyProfile";
import Register from "../pages/Register";
import ProductIdPage from "../pages/ProductIdPage";
import SubcategoryIdPage from "../pages/SubcategoryIdPage";

export const routes = [
    { id: 1, path: "/", component: Home },
    { id: 2, path: "/catalog", component: Catalog },
    { id: 3, path: "/catalog/:id", component: CatalogIdPage },
    { id: 4, path: "/login", component: Login },
    { id: 5, path: "/registration", component: Register },
    { id: 6, path: "/profile", component: MyProfile },
    { id: 7, path: "/product/:id", component: ProductIdPage },
    { id: 8, path: "/subcategory/:id", component: SubcategoryIdPage }
]