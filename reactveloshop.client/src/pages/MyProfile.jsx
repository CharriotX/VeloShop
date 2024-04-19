import { useEffect, useState } from "react";
import AuthService from "../services/AuthService";
import { useFetching } from "../hooks/useFetching";
import { useContext } from "react";
import { Context } from "../main";
import { observer } from "mobx-react-lite";

function MyProfile() {
    const { store } = useContext(Context);

    return (
        <div>
        111111
            {/*{store.isAuth ? `Пользователь авторизован под ${store.user.email}` : "Авторизуйтесь!"}*/}
        </div>
    );
}

export default observer(MyProfile);