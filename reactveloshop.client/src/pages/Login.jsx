import { useContext, useState } from "react";
import AuthService from "../services/AuthService";
import { Context } from "../main";
import { observer } from "mobx-react-lite";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");

    const { store } = useContext(Context);


    return (
        <div>
            <input
                onChange={e => setUsername(e.target.value)}
                value={username}
                type="text"
                placeholder="Username"
            ></input>
            <input
                onChange={e => setEmail(e.target.value)}
                value={email}
                type="text"
                placeholder="Email"
            ></input>
            <input
                onChange={e => setPassword(e.target.value)}
                value={password}
                type="password"
                placeholder="Password"
            ></input>
            <button onClick={() => store.login(email, password)}>Login</button>
            <button onClick={() => store.registration(username, email, password)}>Registration</button>
        </div>
    );
}

export default observer(Login);