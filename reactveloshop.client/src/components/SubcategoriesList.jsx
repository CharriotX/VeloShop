import classes from "../styles/SubcategoriesList.module.css"

function SubcategoriesList(props) {
    console.log(props)
    return (
        <div>
            <div>{ props.name}</div>
            {props.category.subcategories.map(sub =>
                <span key={sub.id}>{sub.name}</span>
            )}
        </div>
    );
}

export default SubcategoriesList;