import { useState } from "react";
import { useFetching } from "../hooks/useFetching";
import CategoryService from "../services/CategoryService";
import { useEffect } from "react";
import CategoryItem from "./CategoryItem";
import classes from "../styles/CategoryList.module.css"

function CategoryList() {

    const [categories, setCategories] = useState([])
    const [fetchCategory, isLoading, error] = useFetching(async () => {
        const response = await CategoryService.getAll();
        setCategories(response.data)
    })

    useEffect(() => {
        fetchCategory()
    }, [])

    return (
        <>
            <h2 className={classes.Title}>Каталог магазина</h2>
            <div className={classes.List} >
                {isLoading
                    ? <div>Иддет загрузка</div>
                    : categories.map(category =>
                        <CategoryItem key={category.id} category={category}></CategoryItem>)
                }
            </div>
        </>
    );
}

export default CategoryList;