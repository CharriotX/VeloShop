/* eslint-disable react/prop-types */
import ProductService from '../../../services/ProductService';
import classes from '../button/MyButton.module.css'
import MyButton from './MyButton';

const DeleteProductButton = ({ id, setVisible }) => {

    const handleDelete = (id) => {
        ProductService.deleteProduct(id)
        setVisible(false)
    }

    return (
        <>
            <div>
                Удалить продукт id ={id}?
            </div>
            <div>
                <MyButton onClick={() => handleDelete(id)}>Да</MyButton>
                <MyButton onClick={() => setVisible(false)}>Нет</MyButton>
            </div>
        </>
    )
}

export default DeleteProductButton;