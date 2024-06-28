import { useMemo, useState } from 'react';
import { useParams } from 'react-router-dom';
import ProductList from '../components/ProductList';
import SubcategoriesList from '../components/SubcategoriesList';
import { useFetching } from '../hooks/useFetching';
import CategoryService from '../services/CategoryService';
import classes from "../styles/CatalogIdPage.module.css";

function CatalogIdPage() {
    const params = useParams();
    const [category, setCategory] = useState({});
    const [subcategories, setSubcategories] = useState([]);
    const [productsCount, setProdcutsCount] = useState(1);

    const [fetchCategory, isCategoryLoading, error] = useFetching(async (id) => {
        const response = await CategoryService.getById(id);
        setProdcutsCount(response.data.totalProducts)
        setSubcategories(response.data.subcategories)
        setCategory(response.data)
    })

    useMemo(() => {
        fetchCategory(params.id)
    }, [params.id])

    return (
        <>
            <div>
                {isCategoryLoading
                    ? <div>...Loading</div>
                    : <div>
                        <div style={{ display: "flex", justifyContent: "center" }}>
                            <h2 className={classes.CategoryTitle}>{category.name}</h2>
                            <div className={classes.totalRecords}>
                                <span >{productsCount}</span>
                            </div>
                        </div>
                        <SubcategoriesList subcategories={subcategories}></SubcategoriesList>
                        <ProductList categoryId={params.id}></ProductList>
                    </div>
                }
            </div>
        </>
    );
}

export default CatalogIdPage;