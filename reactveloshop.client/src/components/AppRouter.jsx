import { Routes, Route, Navigate } from "react-router-dom";
import { publicRoutes } from "../router";
import { observer } from "mobx-react-lite"
import { useContext } from "react";
import Loader from "./UI/loader/Loader";
import UserContext from "../context/userContext";
import AdminPage from "../pages/AdminPage";
import AddProductPage from "../pages/AddProductPage";
import MyProfile from "../pages/MyProfile";
import ProtectedRoute from "../router/ProtectedRoute";


function AppRouter() {
    const { loading } = useContext(UserContext);

    if (loading) {
        return <Loader></Loader>
    }

    return (
        <Routes>
            {publicRoutes.map(route =>
                <Route
                    key={route.id}
                    path={route.path}
                    Component={route.component}
                ></Route>
            )}
            <Route exact path='/profile' element={<ProtectedRoute component={MyProfile} roles="User, Admin" />} />
            <Route exact path='/addProduct' element={<ProtectedRoute component={AddProductPage} roles="Admin" />} />
            <Route exact path='/updateProduct' element={<ProtectedRoute component={MyProfile} roles="Admin" />} />
            <Route exact path='/admin' element={<ProtectedRoute component={AdminPage} roles="Admin" />} />
            <Route
                path="*"
                element={<Navigate to={'/'} replace></Navigate>}
            ></Route>
        </Routes>
    );
}

export default observer(AppRouter);