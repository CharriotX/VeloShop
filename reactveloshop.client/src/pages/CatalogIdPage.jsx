import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import CategoryService from '../services/CategoryService';
import { useEffect } from 'react';
import classes from "../styles/CatalogIdPage.module.css"
import SubcategoriesList from '../components/SubcategoriesList';

function CatalogIdPage() {
    const params = useParams();
    const [category, setCategory] = useState({});
    const [subcategories, setSubcategories] = useState([]);

    const [fetchCategory, isLoading, error] = useFetching(async (id) => {
        const response = await CategoryService.getById(id);
        setCategory(response.data)
    })

    const [fetchSubcategory, isSubcategoryLoading, subError] = useFetching(async (id) => {
        const response = await CategoryService.getSubcategoriesByCategoryId(id);
        setSubcategories(response.data)
    })

    useEffect(() => {
        fetchCategory(params.id)
        fetchSubcategory(params.id)
    }, [params])

    return (
        <>
            {isLoading
                ? <div>Loading</div>
                : <h2 className={classes.CategoryTitle}>{category.name}</h2>              
            }

            {isSubcategoryLoading
                ? <div>Loading</div>
                : <SubcategoriesList subcategories={subcategories}></SubcategoriesList>
            }
        </>
    );
}

export default CatalogIdPage;