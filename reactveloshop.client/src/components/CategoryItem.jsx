import classes from "../styles/CategoryItem.module.css"
import { Link } from "react-router-dom"

function CategoryItem(props) {
    return (
        <div className={classes.Item}>
            <h4>
                <Link to={`/catalog/${props.category.id}`}>{props.category.name}</Link>
            </h4>
            <div className={classes.SubcategoryList}>
                {props.category.subcategories.map(sub =>
                    <span key={sub.id} className={classes.SubcategoryItem}>{sub.name}</span>
                )}
            </div>
        </div>
    );
}

export default CategoryItem;