/* eslint-disable react/prop-types */
import classes from "../../../styles/ProductList.module.css"
import LoaderProductCard from "./LoaderProductCard";

function LoaderProductList() {

    return (
        <>
            <div className={classes.productList}>
                <LoaderProductCard></LoaderProductCard>
                <LoaderProductCard></LoaderProductCard>
                <LoaderProductCard></LoaderProductCard>
                <LoaderProductCard></LoaderProductCard>
            </div>

        </>

    );
}

export default LoaderProductList;