/* eslint-disable react/prop-types */
import ProductCard from "./ProductCard";
import classes from "../styles/ProductList.module.css"
import { useRef, useState } from "react";
import { useFetching } from "../hooks/useFetching";
import ProductService from "../services/ProductService";
import { useEffect } from "react";
import { useObserver } from "../hooks/useObserver";
import Loader from "./UI/loader/Loader";
import LoaderProductList from "./UI/loaderProductList/LoaderProductList";

function ProductList({ categoryId }) {

    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);
    const lastElement = useRef();
    const observer = useRef();

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await ProductService.getAllProductsByCategory(categoryId, page);
        setProducts([...products, ...response.data.data.products])
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })

    useEffect(() => {
        fetchProducts(categoryId, page);
    }, [page])

    useObserver(lastElement, page < totalPages, isProductsLoading, () => {
        setPage(page + 1)
    })

    return (
        <>
            <div>
                {isProductsLoading
                    ? <LoaderProductList></LoaderProductList>
                    : <div className={classes.productList}>
                        {products.map((product, index) => {
                            if (index + 1 === products.length) {
                                return <ProductCard key={product.id} product={product}></ProductCard>
                            }
                            return <ProductCard key={product.id} product={product}></ProductCard>
                        })}
                        <div style={{ height: 20, background: "red", marginTop: 200 }} ref={lastElement}></div>
                    </div>
                }
            </div>
        </>

    );
}

export default ProductList;