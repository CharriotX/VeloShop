/* eslint-disable react/prop-types */
import classes from "../../../styles/ProductCard.module.css"
import Loader from "../loader/Loader";
function LoaderProductCard() {

    return (
        <div className={classes.card}>
            <div className={classes.cardImage}>
                <div style={{ display: "flex", justifyContent: "center" }}>
                    <Loader></Loader>
                </div>
            </div>
            <div className={classes.cardBody}>
                <div className={classes.cardTitle}>
                    
                </div>
                <div style={{ display: "flex", justifyContent: "center" }}>
                    <Loader></Loader>
                </div>
            </div>
            <div className={classes.cardPrice}>
               
            </div>
        </div>
    );
}

export default LoaderProductCard;