/* eslint-disable react/prop-types */
import ProductCard from "./ProductCard";
import classes from "../styles/ProductList.module.css"
import { useRef, useState } from "react";
import { useFetching } from "../hooks/useFetching";
import ProductService from "../services/ProductService";
import { useEffect } from "react";
import { useObserver } from "../hooks/useObserver";
import LoaderProductList from "./UI/loaderProductList/LoaderProductList";
import MySortFilter from "./UI/sortFilter/MySortFilter";

function ProductList({ categoryId, subcategoryId }) {
    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);
    const [sorting, setSorting] = useState({ column: "Id", order: "asc" })
    const [searchTerm, setSearchTerm] = useState("")
    const lastElement = useRef();

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        if (subcategoryId === undefined) {
            const response = await ProductService.getProductsByCategory(categoryId, searchTerm, sorting.column, sorting.order, page, pageSize);
            setProducts([...products, ...response.data.data.products])
            setTotalPages(response.data.totalPages)
            setTotalRecords(response.data.totalRecords)
        } else {
            const response = await ProductService.getProductsBySubcategory(subcategoryId, searchTerm, sorting.column, sorting.order, page, pageSize);
            setProducts([...products, ...response.data.data.products])
            setTotalPages(response.data.totalPages)
            setTotalRecords(response.data.totalRecords)
        }
    })

    useEffect(() => {
        if (subcategoryId === undefined) {
            fetchProducts(categoryId, searchTerm, sorting.column, sorting.order, page, pageSize)
        } else {
            fetchProducts(subcategoryId, searchTerm, sorting.column, sorting.order, page, pageSize)
        }
    }, [page, pageSize, searchTerm, sorting])

    useObserver(lastElement, page < totalPages, isProductsLoading, () => {
        setPage(page + 1)
    })

    const sortingHandler = (value) => {
        setPage(1)
        setSorting(value)
        setProducts([])
    }

    const changePageSize = (pageSize) => {
        setPage(1)
        setPageSize(pageSize)
        setProducts([])
    }

    const searchTermHandler = (value) => {
        setProducts([])
        setSearchTerm(value)
        setPage(1)
    }

    return (
        <>
            <div>
                <div>
                    <MySortFilter changeSorting={sortingHandler} searchTerm={searchTerm} setPage={setPage} changeSearchTerm={searchTermHandler} changePageSize={changePageSize}></MySortFilter>
                </div>
                {products.length === 0 &&
                    <LoaderProductList></LoaderProductList>
                }
                {isProductsLoading
                    ? <LoaderProductList></LoaderProductList>
                    : <div className={classes.productList}>
                        {products.map(product =>
                            <ProductCard key={product.id} product={product}></ProductCard>
                        )}
                        <div style={{ height: 20, background: "red", marginTop: 200 }} ref={lastElement}></div>
                    </div>
                }
            </div>
        </>

    );
}

export default ProductList;