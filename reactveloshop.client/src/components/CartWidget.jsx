import { useContext } from 'react';
import shoppingCart from '../accets/shoppingCartIcon.png'
import classes from '../styles/CartWidget.module.css'
import { Link } from "react-router-dom";
import { Context } from '../main';
import { useEffect } from 'react';
import { observer } from "mobx-react-lite";
import { toJS } from 'mobx';
import { useState } from 'react';


function CartWidget() {
    const { cartStore } = useContext(Context);
    const [count, setCount] = useState(0)

    useEffect(() => {
        cartStore.getCart()
        setCount(cartStore.productCount)
    }, [cartStore.productCount])

    return (
        <>
            <Link to="/cart" className={classes.container} >
                <span className={classes.productsCount}>{count}</span>
                <img src={shoppingCart} className={classes.shoppingCart} alt="Go to cart"></img>
            </Link>
        </>
    );
}

export default observer(CartWidget);