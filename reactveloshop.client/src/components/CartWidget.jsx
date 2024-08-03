import { useContext } from 'react';
import shoppingCart from '../accets/shoppingCartIcon.png'
import classes from '../styles/CartWidget.module.css'
import { Link } from "react-router-dom";
import { useEffect } from 'react';
import { observer } from "mobx-react-lite";
import { useState } from 'react';
import { Context } from '../main';


function CartWidget() {
    const { cartStore } = useContext(Context);
    const [count, setCount] = useState(0)

    useEffect(() => {
        cartStore.getProductCount()
        setCount(cartStore.productCount)
    }, [cartStore])

    return (
        <>
            <Link to="/cart" className={classes.container} >
                <span className={classes.productsCount}>{cartStore.productCount}</span>
                <img src={shoppingCart} className={classes.shoppingCart} alt="Go to cart"></img>
            </Link>
        </>
    );
}

export default observer(CartWidget);