import MyButton from "../components/UI/button/MyButton";
import classes from '../styles/Admin.module.css'
import { Link } from "react-router-dom"
import MyModal from "../components/UI/modal/MyModal"
import CreateBrandForm from "../components/CreateBrandForm";
import { useState } from "react";

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
        </>
    );
}

export default AdminPage;