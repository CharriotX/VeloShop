import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import CategoryService from '../services/CategoryService';
import { useEffect } from 'react';
import CategoryItem from '../components/CategoryItem';
import SubcategoriesList from '../components/SubcategoriesList';

function CatalogIdPage() {
    const {id} = useParams();
    const [category, setCategory] = useState({});
    const [subcategories, setSubcategories] = useState([])

    const [fetchCategory, isLoading, error] = useFetching(async (id) => {
        const response = await CategoryService.getSubcategoriesByCategoryId(id);
        setCategory(response.data)
        setSubcategories(response.data.subcategories)
    })

    useEffect(() => {
        fetchCategory(id)
    }, [id])

    console.log(category)
    console.log(subcategories)

    return (
        <>
            <h2 >{category.name}</h2>
            <SubcategoriesList subcategories={subcategories}></SubcategoriesList>
        </>
    );
}

export default CatalogIdPage;