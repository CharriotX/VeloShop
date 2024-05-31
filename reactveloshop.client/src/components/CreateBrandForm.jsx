/* eslint-disable react/prop-types */
/* eslint-disable react-refresh/only-export-components */
import { useContext, useEffect, useState } from "react";
import MyInput from "./UI/input/MyInput";
import MySelect from "./UI/select/MySelect";
import { observer } from "mobx-react-lite";
import { Context } from "../main";
import MyButton from "./UI/button/MyButton";
import BrandService from "../services/BrandService";
import { toJS } from 'mobx';
import classes from '../styles/CreateBrandForm.module.css'

function CreateBrandForm({ setVisible }) {

    const { createBrandStore } = useContext(Context);
    const [name, setName] = useState('');
    const [loading, setLoading] = useState(false);
     
    useEffect(() => {
        createBrandStore.getAllCategories()
        setLoading(true)
        createBrandStore.getAllBrandsByCategory(toJS(createBrandStore.category));
        setLoading(false)
    }, [createBrandStore.category])

    const createBrand = async (e) => {
        e.preventDefault();
        const data = {
            name: name,
            categoryId: toJS(createBrandStore.category.id)
        }
        await BrandService.createBrand(data);
        createBrandStore.setCategories([]);
        createBrandStore.setSelectedCaregory({});
        setName('');
        setVisible(false)
    }

    return (
        <>
            <form onSubmit={createBrand} className={classes.form}>
                <div className={classes.brandInput}>
                    <div>
                        Выберите категорию
                    </div>
                    <MySelect
                        defaultValue="Выберите категорию"
                        options={toJS(createBrandStore.categories)}
                        onChange={(e) => createBrandStore.setSelectedCaregory(e)}
                    ></MySelect>
                </div>
                <div className={classes.brandInput}>
                    <div>
                        Введите название
                    </div>
                    <MyInput onChange={(e) => setName(e.target.value)}></MyInput>
                </div>
                <MyButton type="submit">Создать</MyButton>
            </form>
            <div className={classes.brandList}>
                {loading
                    ? <div>Для этой категории бренды отсутсвуют</div>
                    : <div>
                        <h4>Уже добавленные бренды</h4>
                        {toJS(createBrandStore.brands).map(brand =>
                            <div key={brand.id}>
                                {brand.name}
                            </div>
                        )}
                    </div>
                }
            </div>            
        </>
    )
}

export default observer(CreateBrandForm);