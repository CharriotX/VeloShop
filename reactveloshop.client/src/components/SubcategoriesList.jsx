/* eslint-disable react/prop-types */
import classes from "../styles/SubcategoriesList.module.css"

function SubcategoriesList({ subcategories }) {
    console.log(subcategories)
    return (
        <div className={classes.SubcategoriesList }>
            {subcategories.map(sub =>
                <div className={classes.SubcategoryItem} key={sub.id}>{sub.name}</div>
            )}
        </div>
    );
}

export default SubcategoriesList;