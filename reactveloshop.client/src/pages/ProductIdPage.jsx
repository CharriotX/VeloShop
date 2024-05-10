import { useState } from 'react';
import { useParams } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching';
import ProductService from '../services/ProductService';
import { useEffect } from 'react';
import classes from "../styles/ProductIdPage.module.css"
import noPhotoImage from "../accets/defaultProductImage.png"
import MyButton from "../components/UI/button/MyButton";

function ProductIdPage() {
    const params = useParams();

    const [product, setProduct] = useState({});

    const [fetchProduct, isLoading, error] = useFetching(async (id) => {
        const response = await ProductService.getProduct(id);
        console.log(response.data)
        setProduct(response.data)
    })

    useEffect(() => {
        fetchProduct(params.id)
    }, [params])

    return (
        <div className={classes.productContainer}>
            <div className={classes.productTitle}>
                <h2>{product.name} {product.brandName}</h2>
            </div>
            <div className={classes.productBody}>
                <div className={classes.productImage}>
                    <img src={noPhotoImage}></img>
                </div>
                <div className={classes.productPriceBlock}>
                    <div className={classes.productPrice} >
                        {product.price} руб.
                    </div>
                    <div>
                        <MyButton>В корзину</MyButton>
                    </div>
                </div>
            </div>
            <div className={classes.productDesc}>
                <div className={classes.productDescTitle}>
                    Описание
                </div>
                <div className={classes.productDescText}>
                    {product.description}
                </div>
                {product.productSpecifications && (
                    < table >
                        <tbody>
                            {product.productSpecifications.map(spec =>
                                <tr className={classes.productSpecRow} key={spec.name}>
                                    <td className={classes.productSpecCol}>{spec.name}</td>
                                    <td className={classes.productSpecCol}>{spec.value}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                )}
            </div>
        </div>


    );
}

export default ProductIdPage;