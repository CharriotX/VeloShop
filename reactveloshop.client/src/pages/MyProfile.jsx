import { useContext, useEffect } from "react";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import Login from "./Login";
import classes from '../styles/MyProfile.module.css'

function MyProfile() {
    const { store } = useContext(Context);

    useEffect(() => {
        //var response = AuthService.myProfile();
        //setUser(response.data)

    }, [])

    if (!store.isAuth) {
        return (<Login></Login>)
    }

    return (
        <div>
            <h1 className={classes.title}>Profile info</h1>
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