import classes from "../styles/SubcategoriesList.module.css"

function SubcategoryItem(props) {
    console.log(props)
    return (
        <div>
            <div>{props.sub.id}</div>
            <div>{props.sub.name}</div>
        </div>
    );
}

export default SubcategoryItem;