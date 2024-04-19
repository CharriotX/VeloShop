import { Routes, Route, Navigate } from "react-router-dom";
import { routes } from "../router";


function AppRouter() {
    return (
        <Routes>
            {routes.map(route =>
                <Route
                    key={route.id}
                    path={route.path}
                    Component={route.component}
                ></Route>
            )}
            <Route
                path="*"
                element={<Navigate to={'/'} replace></Navigate>}
            ></Route>
        </Routes>
    );
}

export default AppRouter;