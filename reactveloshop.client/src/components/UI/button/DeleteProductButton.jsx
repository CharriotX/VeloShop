/* eslint-disable react/prop-types */
import ProductService from '../../../services/ProductService';
import MyButton from './MyButton';

const DeleteProductButton = ({ id, setVisible }) => {

    const handleDelete = (id) => {
        ProductService.deleteProduct(id)
        setVisible(false)
    }

    return (
        <>
            <div style={{ marginBottom: 10 }}>
                Удалить продукт id ={id}?
            </div>
            <div style={{ display: "flex"}}>
                <MyButton onClick={() => handleDelete(id)}>Да</MyButton>
                <MyButton onClick={() => setVisible(false)}>Нет</MyButton>
            </div>
        </>
    )
}

export default DeleteProductButton;