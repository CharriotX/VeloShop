import React, { useEffect, useState } from 'react';
import LoaderProductList from '../components/UI/loaderProductList/LoaderProductList';
import MySelect from '../components/UI/select/MySelect';
import ProductCard from '../components/ProductCard';
import classes from "../styles/ProductList.module.css"
import homeClasses from "../styles/Home.module.css"
import { useFetching } from '../hooks/useFetching';
import ProductService from '../services/ProductService';
import BikeBrandsList from '../components/BikeBrandsList';
import MyInput from '../components/UI/input/MyInput';
import Pagination from '../components/UI/pagination/Pagination';

function Home() {

    const [products, setProducts] = useState([]);
    const [brands, setBrands] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);
    const [sorting, setSorting] = useState({ column: "Id", order: "asc" })
    const [searchTerm, setSearchTerm] = useState("")
    const [sortBrand, setSortBrand] = useState("")

    const [fetchProducts, isProductsLoading, productsError] = useFetching(async () => {
        const response = await ProductService.getProductsByBikeCategory(searchTerm, sortBrand,);
        console.log(response)
        setProducts([...products, ...response.data.items])
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })

    useEffect(() => {
        fetchProducts(page, pageSize)
    }, [])

    const options = [
        {
            label: 'По алфавиту (возрастание)',
            value: { column: "name", order: "asc" },
        },
        {
            label: 'По алфавиту (убывание)',
            value: { column: "name", order: "desc" },
        },
        {
            label: 'По цене (возрастание)',
            value: { column: "price", order: "asc" },
        },
        {
            label: 'По цене (убывание)',
            value: { column: "price", order: "desc" },
        },
    ];

    const selectHandler = (e) => {
        switch (e.target.value) {
            case 'По алфавиту (возрастание)':
                setSorting(options[0].value);
                break;
            case 'По алфавиту (убывание)':
                setSorting(options[1].value);
                break;
            case 'По цене (возрастание)':
                setSorting(options[2].value);
                break;
            case 'По цене (убывание)':
                setSorting(options[3].value);
                break;
        }
    };


    const brandSorting = (name) => {
        console.log(name)
        setSortBrand(name)
    }

    const changePage = (page) => {
        setPage(page)
    }

    return (
        <div>
            <div >
                <div className={homeClasses.sortContainer}>
                    <select value={options.value} onChange={(e) => selectHandler(e)} defaultValue="Сортировка">
                        <option disabled>Сортировка</option>
                        {options.map((option) => (
                            <option key={option.label}>{option.label}</option>
                        ))}
                    </select>
                    <MySelect
                        defaultValue="Кол-во отображаемых элементов"
                        options={[
                            { value: "5", name: "5" },
                            { value: "10", name: "10" },
                            { value: "20", name: "20" },
                        ]}
                        onChange={value => setPageSize(value)}
                    ></MySelect>
                    <MyInput placeholder="Поиск.."></MyInput>
                </div>
                <div className={homeClasses.brandsContaner}>
                    <div>Названия брендов</div>
                    <BikeBrandsList sorting={brandSorting}></BikeBrandsList>
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