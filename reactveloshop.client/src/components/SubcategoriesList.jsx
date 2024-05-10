/* eslint-disable react/prop-types */
import classes from "../styles/SubcategoriesList.module.css"
import { Link } from "react-router-dom"

function SubcategoriesList({ subcategories }) {
    return (
        <div className={classes.SubcategoriesList }>
            {subcategories.map(sub =>
                <div className={classes.SubcategoryItem} key={sub.id}>
                    <Link to={`/subcategory/${sub.id}`}>{sub.name}</Link>
                </div>
            )}
        </div>
    );
}

export default SubcategoriesList;