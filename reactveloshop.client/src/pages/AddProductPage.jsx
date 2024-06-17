/* eslint-disable react-refresh/only-export-components */
import { useEffect, useState } from "react";
import { toJS } from 'mobx';
import MyInput from "../components/UI/input/MyInput";
import MySelect from "../components/UI/select/MySelect";
import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { Context } from "../main";
import MyButton from "../components/UI/button/MyButton";
import ProductService from "../services/ProductService";
import classes from '../styles/AddProductPage.module.css'

function AddProductPage() {

    const { productDataStore } = useContext(Context);
    const [specifications, setSpecifications] = useState([...productDataStore.specifications]);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");

    useEffect(() => {
        productDataStore.getAllCategories()
        productDataStore.getCategoryDataForAddProduct(toJS(productDataStore.category))
        setSpecifications(toJS(productDataStore.specifications))
    }, [productDataStore.category])

    const changeSpecifications = (e, id) => {
        const { value } = e.target

        setSpecifications((spec) =>
            spec?.map((list, index) =>
                index === id ? { ...list, value: value } : list
            )
        );
    };

    const createProduct = (e) => {
        e.preventDefault();

        const data = {
            name: name,
            description: description,
            price: price,
            brandId: Number(productDataStore.brand),
            categoryId: productDataStore.category.id,
            subcategoryId: Number(productDataStore.subcategory),
            productSpecifications: specifications
        }

        ProductService.createProduct(data);
    }

    return (
        <>
            <div>
                <h2>Добавление нового товара</h2>
                <form onSubmit={createProduct} className={classes.form}>
                    <div >
                        <div className={classes.selectsField}>
                            <div>Выберите категорию товара</div>
                            <div>
                                <MySelect
                                    defaultValue="Выберите категорию"
                                    options={toJS(productDataStore.categories)}
                                    onChange={(e) => productDataStore.setSelectedCaregory(e)}
                                ></MySelect>
                            </div>
                        </div>
                        <div className={classes.selectsField}>
                            <div>
                                Выберите подкатегорию
                            </div>
                            <div>
                                <MySelect
                                    defaultValue="Выберите подкатегорию"
                                    options={toJS(productDataStore.subcategories)}
                                    onChange={(e) => productDataStore.setSelectedSubcaregory(e)}
                                ></MySelect>
                            </div>
                        </div>
                        <div className={classes.selectsField}>
                            <div>
                                Выберите бренд
                            </div>
                            <div>
                                <MySelect
                                    defaultValue="Выберите бренд"
                                    options={toJS(productDataStore.brands)}
                                    onChange={(e) => productDataStore.setSelectedBrand(e)}
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
                    {productDataStore.specifications.length
                        ? <table>
                            <caption>Введите характеристики</caption>
                            <thead>
                                <tr>
                                    <th>Название</th>
                                    <th>Значение</th>
                                </tr>
                            </thead>
                            <tbody>
                                {productDataStore.specifications.map((spec, i) =>
                                    <tr key={spec.id}>
                                        <th>{spec.name}</th>
                                        <th><MyInput onChange={(e) => changeSpecifications(e, i)} /></th>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                        : <div> </div>
                    }
                    <div className={classes.submitBtn}>
                        <MyButton type="submit">Создать</MyButton>
                    </div>
                </form>
            </div>
        </>
    );
}

export default observer(AddProductPage);