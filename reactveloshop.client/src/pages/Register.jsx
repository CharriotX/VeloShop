import { useContext, useState } from "react";
import { Context } from "../main";
import { Link } from "react-router-dom";
import { observer } from "mobx-react-lite";
import MyInput from "../components/UI/input/MyInput";
import classes from '../styles/Login.module.css'
import MyButton from "../components/UI/button/MyButton";
function Register() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");

    const { store } = useContext(Context);

    return (
        <div className={classes.login}>
            <div className={classes.loginForm}>
                <h1 className={classes.title}>Registration</h1>
                <div>
                    <MyInput
                        onChange={e => setEmail(e.target.value)}
                        value={email}
                        type="text"
                        placeholder="Email"
                    ></MyInput>
                </div>
                <div>
                    <MyInput
                        onChange={e => setUsername(e.target.value)}
                        value={username}
                        type="text"
                        placeholder="Username"
                    ></MyInput>
                </div>
                <div>
                    <MyInput
                        onChange={e => setPassword(e.target.value)}
                        value={password}
                        type="password"
                        placeholder="Password"
                    ></MyInput>
                </div>
                <div className={classes.submitBtn}>
                    <MyButton className={classes.btn} onClick={() => store.registration(username, email, password)}>Registration</MyButton>
                </div>
                <div className={classes.registerRedirect}>
                    Уже зарегистрированы? <Link to="/login">Войти.</Link>
                </div>
            </div>
        </div>
    );
}

export default observer(Register);