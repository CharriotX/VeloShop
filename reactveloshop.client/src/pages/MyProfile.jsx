import { useContext } from "react";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import Login from "./Login";
import classes from '../styles/MyProfile.module.css'
import { Link } from "react-router-dom";

function MyProfile() {
    const { store } = useContext(Context);

    if (!store.isAuth) {
        return (<Login></Login>)
    }

    console.log(store.user.email)

    return (
        <div>
            <h1 className={classes.title}>Profile info</h1>
            <div>
                <Link to="/admin">Admin panel</Link>
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
                        {store.user.username}
                    </div>
                    <div>
                        {store.user.email}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default observer(MyProfile);