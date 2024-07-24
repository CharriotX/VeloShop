/* eslint-disable react-refresh/only-export-components */
import { useContext } from "react";
import { observer } from "mobx-react-lite";
import Login from "./Login";
import classes from '../styles/MyProfile.module.css'
import { Link } from "react-router-dom";
import { Context } from "../main";

function MyProfile() {
    const { authStore } = useContext(Context);

    if (!authStore.isAuth) {
        return (<Login></Login>)
    }
    return (
        <div>
            <h1 className={classes.title}>Profile info</h1>
            <div >
                {authStore.isAdmin &&
                    <Link style={{ color: "red" }} to="/admin">Admin panel</Link>
                }
            </div>
            <div className={classes.profileInfo}>
                <div className={classes.infoCol}>
                    <div>
                        Username
                    </div>
                    <div>
                        Email
                    </div>
                </div>
                <div className={classes.infoCol}>
                    <div>
                        {authStore.user.username}
                    </div>
                    <div>
                        {authStore.user.email}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default observer(MyProfile);