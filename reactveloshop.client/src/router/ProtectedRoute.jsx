/* eslint-disable react/prop-types */
import { Fragment, useContext } from "react";
import { Navigate } from 'react-router-dom';
import { Context } from "../main";
import { useEffect } from "react";
import { toJS } from 'mobx';
import { observer } from "mobx-react-lite";

const ProtectedRoute = ({ component: Component, roles, ...props }) => {
    const { authStore } = useContext(Context);
    console.log(!roles.includes(authStore.user.role.length))

    if (!authStore.isAuth) {
        return <Navigate to={'/login'} replace></Navigate>
    }

    if (authStore.user.role.length && !roles.includes(authStore.user.role)) {
        return <Navigate to={'/'} replace></Navigate>
    }

    return (
        <Fragment>
            <Component {...props} />
        </Fragment>
    )
};

export default observer(ProtectedRoute);