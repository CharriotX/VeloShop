/* eslint-disable react-refresh/only-export-components */
import { useContext, useState } from "react";
import { Navigate, Link } from "react-router-dom";
import { observer } from "mobx-react-lite";
import MyButton from "../components/UI/button/MyButton";
import MyInput from "../components/UI/input/MyInput";
import classes from '../styles/Login.module.css'
import { Context } from "../main";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const { authStore } = useContext(Context);


    if (authStore.isAuth) {
        return (<Navigate to={'/profile'} replace></Navigate>)
    }

    return (
        <div className={classes.login}>
            <div className={classes.loginForm}>
                <h1 className={classes.title}>Log In</h1>
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
                        onChange={e => setPassword(e.target.value)}
                        value={password}
                        type="password"
                        placeholder="Password"
                    ></MyInput>
                </div>
                <div className={classes.submitBtn}>
                    <MyButton className={classes.btn} onClick={() => authStore.login(email, password)}>Login</MyButton>
                </div>
                <div className={classes.registerRedirect}>
                    Еще нет аккаунта? <Link to="/registration">Перейти к регистрации.</Link>
                </div>
            </div>
        </div>
    );
}

export default observer(Login);