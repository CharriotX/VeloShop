import React from 'react';
import LoaderProductList from '../components/UI/loaderProductList/LoaderProductList';
import MySelect from '../components/UI/select/MySelect';
import ProductCard from '../components/ProductCard';
import classes from "../styles/ProductList.module.css"

function Home() {

    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await ProductService.getAllProductsByCategory(categoryId, page);
        setProducts([...products, ...response.data.data.products])
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })

    return (
        <div>
            <div>
                <MySelect
                    defaultValue="Сортировка"
                    options={[
                        {value: "name", name: "По названию"},
                        {value: "price", name: "По цене"},
                    ] }
                ></MySelect>
            </div>
            {isProductsLoading
                ? <LoaderProductList></LoaderProductList>
                : <div className={classes.productList}>
                    {products.map((product, index) => {
                        if (index + 1 === products.length) {
                            return <ProductCard key={product.id} product={product}></ProductCard>
                        }
                        return <ProductCard key={product.id} product={product}></ProductCard>
                    })}
                </div>
            }
        </div>
    );
}

export default Home;