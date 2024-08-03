/* eslint-disable react/prop-types */
import { forwardRef, useContext, } from "react";
import MyButton from "../components/UI/button/MyButton"
import { Link } from "react-router-dom"
import noPhotoImage from "../accets/defaultProductImage.png"
import classes from "../styles/ProductCard.module.css"
import { Context } from "../main";
function ProductCard({ product }, ref) {

    const { cartStore } = useContext(Context)

    const addProduct = (id) => {
        cartStore.addProductToCart(id)
    }

    return (
        <div className={classes.card} ref={ref}>
            <div className={classes.cardImage}>
                <img src={noPhotoImage}></img>
            </div>
            <div className={classes.cardBody}>
                <div className={classes.cardTitle}>
                    <Link to={`/product/${product.id}`}>{product.id}. {product.name}</Link>
                </div>
                <div className={classes.cardDescription}>
                    {product.description}
                </div>
            </div>
            <div className={classes.cardPrice}>
                <div>{product.price} руб.</div>
                <MyButton onClick={() => addProduct(product.id)}>В корзину</MyButton>
            </div>
        </div>
    );
}

export default forwardRef(ProductCard);