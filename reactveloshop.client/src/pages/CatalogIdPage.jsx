import { useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import CategoryService from '../services/CategoryService';
import { useEffect } from 'react';
import classes from "../styles/CatalogIdPage.module.css"
import SubcategoriesList from '../components/SubcategoriesList';
import ProductService from '../services/ProductService';
import ProductList from '../components/ProductList';
import { useObserver } from '../hooks/useObserver';

function CatalogIdPage() {
    const params = useParams();
    const [category, setCategory] = useState({});
    const [subcategories, setSubcategories] = useState([]);
    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [catId, setCatId] = useState();
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);
    const lastElement = useRef();

    const [fetchCategory, isCategoryLoading, error] = useFetching(async (id) => {
        const response = await CategoryService.getById(id);
        setSubcategories(response.data.subcategories)
        setCategory(response.data)
        setCatId(response.data.id)
        setProducts([]);
        setPage(1)
    })

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async (categoryId, page) => {
        const response = await ProductService.getAllProductsByCategory(categoryId, page);
        setProducts([...products, ...response.data.data.products])
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })

    useObserver(lastElement, page < totalPages, isProductsLoading, () => {
        setPage(page + 1);
    })

    const changingCategoryId = () => {
        if (params.id != catId) {
            setProducts([]);
            setPage(1)
        }
    }

    useEffect(() => {
        fetchCategory(params.id)
        fetchProducts(params.id, page)
        changingCategoryId()
    }, [params.id, page, catId])


    return (
        <>
            <div>
                {isCategoryLoading
                    ? <div>Loading</div>
                    : <div>
                        <div style={{ display: "flex", justifyContent: "center" }}>
                            <h2 className={classes.CategoryTitle}>{category.name}</h2>
                            <div className={classes.totalRecords}>
                                <span >{totalRecords}</span>
                            </div>
                        </div>
                        <SubcategoriesList subcategories={subcategories}></SubcategoriesList>
                    </div>
                }
            </div>
            <div>
                <ProductList products={products}></ProductList>
                <div ref={lastElement} style={{ height: 20, background: "white", marginTop: 300 }}></div>
                {isProductsLoading ??
                    <div>Loading</div>
                }
            </div>
        </>
    );
}

export default CatalogIdPage;