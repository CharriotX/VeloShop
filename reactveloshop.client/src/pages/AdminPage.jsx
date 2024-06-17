/* eslint-disable react-refresh/only-export-components */
import MyButton from "../components/UI/button/MyButton";
import classes from '../styles/Admin.module.css'
import { Link } from "react-router-dom"
import MyModal from "../components/UI/modal/MyModal"
import CreateBrandForm from "../components/CreateBrandForm";
import { useContext, useState } from "react";
import { Context } from "../main";
import { observer } from "mobx-react-lite";
import AdminProductTable from "../components/AdminProductTable";

function AdminPage() {
    const [modal, setModal] = useState(false)

    return (
        <>
            <section className={classes.addSection}>
                <div className={classes.addButtons}>
                    <MyButton onClick={() => setModal(true)}>Добавить бренд</MyButton>
                    <MyModal
                        visible={modal}
                        setVisible={setModal}
                    >
                        <CreateBrandForm setVisible={setModal}></CreateBrandForm>
                    </MyModal>
                    <MyButton><Link to="/addProduct">Добавить продукт</Link></MyButton>
                </div>
            </section>
            <section className={classes.infoSection}>
                <div className={classes.topBar}>
                    <div>
                        <Link>Продукты</Link>
                    </div>
                    <div>
                        <Link>Категории и подкатегории</Link>
                    </div>
                    <div>
                        <Link>Бренды</Link>
                    </div>
                </div>
                <div className={classes.infoField}>
                </div>
            </section>
            <section className={classes.tableSection}>
                <div className={classes.producttable}>
                    <AdminProductTable></AdminProductTable>
                </div>
            </section >
        </>
    );
}

export default observer(AdminPage);