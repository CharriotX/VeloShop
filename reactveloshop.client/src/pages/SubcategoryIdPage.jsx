import { useRef, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import ProductService from '../services/ProductService';
import { useEffect } from 'react';
import ProductList from '../components/ProductList';
import { useObserver } from '../hooks/useObserver';

function SubcategoryIdPage() {
    const params = useParams();
    const lastElement = useRef();

    const [products, setProducts] = useState([]);
    const [subcategory, setSubcategory] = useState({});
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);

    const [fetchProducts, isLoading, error] = useFetching(async (id) => {
        const response = await ProductService.getAllProductsBySubcategory(id, page);
        console.log(response.data)
        setProducts([...products, ...response.data.data])
        setSubcategory(response.data.data[0].subcategory)
        setTotalPages(response.data.totalPages)
    })

    useObserver(lastElement, page < totalPages, isLoading, () => {
        setPage(page + 1);
    })

    useEffect(() => {
        fetchProducts(params.id, page)
    }, [params.id, page])
        
    return (
        <>
            <div>
                <h2 style={{padding:10} }>{subcategory.name}</h2>
            </div>
            <ProductList products={products} ></ProductList>
            <div ref={lastElement} style={{ height: 20, background: "white", marginTop: 200 }}></div>
        </>

    );
}

export default SubcategoryIdPage;