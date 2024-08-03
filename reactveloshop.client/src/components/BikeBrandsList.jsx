/* eslint-disable react/prop-types */
import { useState } from "react";
import { useFetching } from "../hooks/useFetching";
import BrandService from "../services/BrandService";
import { useEffect } from "react";
import classes from "../styles/BikeBrandsList.module.css"

function BikeBrandsList({sorting }) {
    const [brands, setBrands] = useState([]);

    const [fetchBrands, isLoadig, error] = useFetching(async () => {
        var response = await BrandService.getBrandsByBikeCategory();
        setBrands(response.data);
    })

    useEffect(() => {
        fetchBrands()
    }, [])

    return (
        <div className={classes.brandContainer}>
            {brands.map(brand =>
                <div key={brand.id} className={classes.brandItem} onClick={() => sorting(brand.name)}>
                    {brand.name}
                </div>
            )}
        </div>
    );


}

export default BikeBrandsList;