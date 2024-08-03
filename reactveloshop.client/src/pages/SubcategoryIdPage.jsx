import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import { useEffect } from 'react';
import ProductList from '../components/ProductList';
import CategoryService from '../services/CategoryService';

function SubcategoryIdPage() {
    const params = useParams();

    const [subcategory, setSubcategory] = useState({});

    const [fetchSubcategory, isLoading, error] = useFetching(async () => {
        const response = await CategoryService.getSubcategoryById(params.id);
        setSubcategory(response.data)
    })

    useEffect(() => {
        fetchSubcategory(params.id)
    }, [params.id])

    return (
        <>
            <div>
                <h2 style={{ padding: 10 }}>{subcategory.name}</h2>
            </div>
            <ProductList subcategoryId={params.id} ></ProductList>
        </>

    );
}

export default SubcategoryIdPage;