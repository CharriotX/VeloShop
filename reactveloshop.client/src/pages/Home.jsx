import { useEffect, useState } from 'react';
import LoaderProductList from '../components/UI/loaderProductList/LoaderProductList';
import ProductCard from '../components/ProductCard';
import classes from "../styles/ProductList.module.css"
import homeClasses from "../styles/Home.module.css"
import { useFetching } from '../hooks/useFetching';
import ProductService from '../services/ProductService';
import BikeBrandsList from '../components/BikeBrandsList';
import Pagination from '../components/UI/pagination/Pagination';
import MySortFilter from '../components/UI/sortFilter/MySortFilter';

function Home() {

    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);
    const [sorting, setSorting] = useState({ column: "Id", order: "asc" })
    const [searchTerm, setSearchTerm] = useState("")
    const [sortBrand, setSortBrand] = useState("")

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await ProductService.getProductsByBikeCategory(searchTerm, sorting.column, sortBrand, sorting.order, page, pageSize);
        setProducts(response.data.items)
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })

    useEffect(() => {
        fetchProducts(page, pageSize)
    }, [page, pageSize, sorting, searchTerm, sortBrand])

    const brandSortingHandler = (name) => {
        setSortBrand(name)
    }

    const sortingHandler = (value) => {
        setSorting(value)
    }

    const searchTermHandler= (value) => {
        setSearchTerm(value)
    }

    const pageSizeHandler = (value) => {
        setSearchTerm(value)
    }

    const changePage = (page) => {
        setPage(page)
    }

    return (
        <div>
            <div >
                <MySortFilter changeSorting={sortingHandler} searchTerm={searchTerm} changePageSize={pageSizeHandler} changeSearchTerm={searchTermHandler}></MySortFilter>
                <div className={homeClasses.brandsContaner}>
                    <div>Названия брендов</div>
                    <BikeBrandsList sorting={brandSortingHandler}></BikeBrandsList>
                </div>
            </div>
            {isProductsLoading
                ? <LoaderProductList></LoaderProductList>
                : <div className={classes.productList}>
                    <h2>Велосипеды</h2>
                    {products.map((product, index) => {
                        if (index + 1 === products.length) {
                            return <ProductCard key={product.id} product={product}></ProductCard>
                        }
                        return <ProductCard key={product.id} product={product}></ProductCard>
                    })}
                </div>
            }
            <div className={homeClasses.pagination}>
                <Pagination changePage={changePage} page={page} totalPages={totalPages}></Pagination>
            </div>
        </div>
    );
}

export default Home;