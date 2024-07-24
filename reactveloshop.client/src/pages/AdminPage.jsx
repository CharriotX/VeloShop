/* eslint-disable react-refresh/only-export-components */
import MyButton from "../components/UI/button/MyButton";
import classes from '../styles/Admin.module.css'
import { Link } from "react-router-dom"
import MyModal from "../components/UI/modal/MyModal"
import CreateBrandForm from "../components/CreateBrandForm";
import { useState } from "react";
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
                    <Link to="/addProduct"><MyButton>Добавить продукт</MyButton></Link>
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