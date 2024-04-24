import { useContext, useEffect, useState } from "react";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import Login from "./Login";
import AuthService from "../services/AuthService";

function MyProfile() {
    const { store } = useContext(Context);
    const [user, setUser] = useState([]);
    const [users, setUsers] = useState([]);

    useEffect(() => {
        //var response = AuthService.myProfile();
        //setUser(response.data)

    }, [])

    console.log(store.user.email, store.isAuth)

    const fetchUsers = () => {
        const response = AuthService.getUsers();
        console.log(response.data)
        setUsers(response.data);
    }

    if (!store.isAuth) {
        return (<Login></Login>)
    }

    return (
        <div>
            {store.isAuth ? `Пользователь авторизован под ${store.user.email}` : "Авторизуйтесь!"}
            <div>
                <button onClick={() => store.logout()}>Logout</button>
            </div>
            <div>
                <button onClick={() => fetchUsers()}>Users</button>
                <ul>
                    {users && users.map(user =>
                        <li key={user.id }>{user.name}</li>
                    )}
                </ul>
            </div>
        </div>
    );
}

export default observer(MyProfile);