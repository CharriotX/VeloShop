/* eslint-disable react/prop-types */
import { useContext, useEffect, useState } from "react";
import classes from "../styles/SubcategoriesList.module.css"
import { useParams } from 'react-router-dom';
import { toJS } from 'mobx';
import { observer } from "mobx-react-lite";
import MySelect from "../components/UI/select/MySelect";
import MyInput from "../components/UI/input/MyInput";
import MyButton from "../components/UI/button/MyButton";
import { useFetching } from "../hooks/useFetching";
import ProductService from "../services/ProductService";
import { Context } from "../main";

function UpdateProductPage() {
    const params = useParams();

    const { productDataStore } = useContext(Context)
    const [specifications, setSpecifications] = useState([]);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");
    const [category, setCategory] = useState({ id: 1, name: "" });
    const [subcategory, setSubcategory] = useState({ id: 1, name: "" });
    const [brand, setBrand] = useState({ id: 1, name: "" });

    const [productFetching, isLoading, error] = useFetching(async () => {
        const response = await ProductService.getProduct(params.id)
        setDescription(response.data.description)
        setName(response.data.name)
        setPrice(response.data.price);
        setSpecifications(response.data.productSpecifications)
        setBrand(brand => ({ ...brand, id: response.data.brand.id, name: response.data.brand.name }))
        setSubcategory(sub => ({ ...sub, id: response.data.subcategory.id, name: response.data.subcategory.name }))
        setCategory(cat => ({ ...cat, id: response.data.category.id, name: response.data.category.name }))
    })

    useEffect(() => {
        productDataStore.getAllCategories()
        productDataStore.getCategoryDataForAddProduct(category.id)
        setSpecifications(toJS(productDataStore.specifications))
    }, [category.id])

    useEffect(() => {
        productFetching()
    }, [])

    const changeSpecifications = (e, id) => {
        const { value } = e.target

        setSpecifications((spec) =>
            spec?.map((list, index) =>
                index === id ? { ...list, value: value } : list
            )
        );
    };

    const updateProduct = (e) => {
        e.preventDefault();

        const data = {
            id: params.id,
            name: name,
            description: description,
            price: price,
            brandId: brand.id,
            categoryId: category.id,
            subcategoryId: subcategory.id,
            productSpecifications: specifications
        }
        console.log(specifications)

        ProductService.updateProduct(data);
    }

    return (
        <div>
            <h2>Изменение существующего товара</h2>
            <form onSubmit={updateProduct} className={classes.form}>
                <div >
                    <div className={classes.selectsField}>
                        <div>Категория {category.name}</div>
                        <div>
                            <MySelect
                                defaultValue="Выберите категорию"
                                options={toJS(productDataStore.categories)}
                                onChange={e => setCategory(cat => ({ ...cat, id: e }))}
                            ></MySelect>
                        </div>
                    </div>
                    <div className={classes.selectsField}>
                        <div>
                            Подкатегория {subcategory.name}
                        </div>
                        <div>
                            <MySelect
                                defaultValue="Изменить подкатегорию"
                                options={toJS(productDataStore.subcategories)}
                                onChange={e => setSubcategory(cat => ({ ...cat, id: e }))}
                            ></MySelect>
                        </div>
                    </div>
                    <div className={classes.selectsField}>
                        <div>
                            Бренд {brand.name}
                        </div>
                        <div>
                            <MySelect
                                defaultValue="Изменить бренд"
                                options={toJS(productDataStore.brands)}
                                onChange={e => setBrand(cat => ({ ...cat, id: e }))}
                            ></MySelect>
                        </div>
                    </div>
                </div>
                <div className={classes.inputsField}>
                    <div>
                        <MyInput
                            placeholder="Введите название"
                            value={name}
                            onChange={e => setName(e.target.value)}
                            type="text"
                        ></MyInput>
                    </div>
                    <div>
                        <MyInput
                            placeholder="Введите цену"
                            value={price}
                            onChange={e => setPrice(e.target.value)}
                            type="number"
                        ></MyInput>
                    </div>
                    <div>
                        <MyInput
                            placeholder="Введите описание"
                            value={description}
                            onChange={e => setDescription(e.target.value)}
                            type="text"
                        ></MyInput>
                    </div>
                </div>
                {/*{productDataStore.specifications.length*/}
                {/*    ? <table>*/}
                {/*        <caption>Введите характеристики</caption>*/}
                {/*        <thead>*/}
                {/*            <tr>*/}
                {/*                <th>Название</th>*/}
                {/*                <th>Значение</th>*/}
                {/*            </tr>*/}
                {/*        </thead>*/}
                {/*        <tbody>*/}
                {/*            {specifications.map((spec, i) =>*/}
                {/*                <tr key={spec.name}>*/}
                {/*                    <th>{spec.name}</th>*/}
                {/*                    <th><MyInput value={spec.value} onChange={(e) => changeSpecifications(e, i)} /></th>*/}
                {/*                </tr>*/}
                {/*            )}*/}
                {/*        </tbody>*/}
                {/*    </table>*/}
                {/*    : <div> </div>*/}
                {/*}*/}
                <div className={classes.submitBtn}>
                    <MyButton type="submit">Update</MyButton>
                </div>
            </form>

        </div>
    );
}

export default observer(UpdateProductPage);