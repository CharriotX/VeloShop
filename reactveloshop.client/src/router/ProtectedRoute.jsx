/* eslint-disable react/prop-types */
import { Fragment, useContext } from "react";
import { Navigate } from 'react-router-dom';
import { Context } from "../main";

const ProtectedRoute = ({ component: Component, roles, ...props }) => {
    const { authStore } = useContext(Context);

    if (!authStore.user) {
        return <Navigate to={'/login'} replace></Navigate>
    }

    if (roles && !roles.includes(authStore.user.role)) {
        return <Navigate to={'/'} replace></Navigate>
    }

    return (
        <Fragment>
            <Component {...props} />
        </Fragment>
    )
};

export default ProtectedRoute;