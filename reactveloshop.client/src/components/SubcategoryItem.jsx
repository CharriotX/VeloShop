/* eslint-disable react/prop-types */
function SubcategoryItem(props) {
    return (
        <div>
            <div>{props.sub.id}</div>
            <div>{props.sub.name}</div>
        </div>
    );
}

export default SubcategoryItem;