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

    const { createProductStore } = useContext(Context);
    const [specifications, setSpecifications] = useState([...createProductStore.specifications]);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [price, setPrice] = useState("");


    useEffect(() => {
        createProductStore.getAllCategories()
        createProductStore.getCategoryDataForAddProduct(createProductStore.category)
        setSpecifications(toJS(createProductStore.specifications))
    }, [createProductStore.category])

    const changeSpecifications = (e, id) => {
        const { value } = e.target;
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
            brandId: Number(createProductStore.brand),
            categoryId: createProductStore.category.id,
            subcategoryId: Number(createProductStore.subcategory),
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
                                    options={toJS(createProductStore.categories)}
                                    onChange={(e) => createProductStore.setSelectedCaregory(e)}
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
                                    options={toJS(createProductStore.subcategories)}
                                    onChange={(e) => createProductStore.setSelectedSubcaregory(e)}
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
                                    options={toJS(createProductStore.brands)}
                                    onChange={(e) => createProductStore.setSelectedBrand(e)}
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
                    {toJS(createProductStore.specifications.length)
                        && <table>
                            <caption>Введите характеристики</caption>
                            <thead>
                                <tr>
                                    <th>Название</th>
                                    <th>Значение</th>
                                </tr>
                            </thead>
                            <tbody>
                                {createProductStore.specifications.map((spec, i) =>
                                    <tr key={spec.id}>
                                        <th>{spec.name}</th>
                                        <th><MyInput onChange={(e) => changeSpecifications(e, i)} /></th>
                                    </tr>
                                )}
                            </tbody>
                        </table>
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