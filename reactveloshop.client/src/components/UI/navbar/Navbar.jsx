﻿import { Link } from "react-router-dom"
import classes from "../../../styles/Navbar.module.css"
import { useContext, useEffect, useState } from "react";
import { useFetching } from "../../../hooks/useFetching";
import CategoryService from "../../../services/CategoryService";
import { observer } from "mobx-react-lite";
import CartWidget from "../../CartWidget";
import { useMemo } from "react";
import { Context } from "../../../main";


function Navbar() {

    const [categories, setCategories] = useState([]);
    const [fetchCategory, isLoading, error] = useFetching(async () => {
        const response = await CategoryService.getAll();
        setCategories(response.data);
    })

    const { authStore } = useContext(Context);

    useEffect(() => {
        
    }, [])

    useMemo(() => {
        fetchCategory()
    }, [])

    return (
        <div className={classes.navbar}>
            <div className={classes.navbarContainer}>
                <div className={classes.navbarHeader}>
                    <h2><Link to="/">VELO SHOP</Link></h2>
                    {authStore.isAuth
                        ? <div className={classes.loginLinks}>
                            <CartWidget></CartWidget>
                            <div>
                                <div><Link to="/profile">Profile</Link></div>
                                <div><Link onClick={() => authStore.logout()}>Logout</Link></div>
                            </div>                            
                        </div>
                        : <div className={classes.loginLinks}>
                            <CartWidget></CartWidget>
                            <Link to="/login">Login</Link>
                        </div>
                    }
                </div>
                <div className={classes.navbarLinks}>
                    <Link to="/catalog">Каталог</Link>
                    {categories.map(category =>
                        <Link key={category.id} to={`/catalog/${category.id}`}>{category.name}</Link>
                    )}
                </div>
            </div>
        </div>
    );
}

export default observer(Navbar);