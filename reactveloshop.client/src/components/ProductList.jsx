/* eslint-disable react/prop-types */
import ProductCard from "./ProductCard";
import classes from "../styles/ProductList.module.css"

function ProductList({ products }) {
    return (
        <div className={classes.productList}>
            {products.map(product =>
                <ProductCard key={product.id} product={product}></ProductCard>
            )}
        </div>
    );
}

export default ProductList;