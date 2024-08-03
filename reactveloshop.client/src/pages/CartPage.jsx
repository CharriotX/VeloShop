/* eslint-disable react-refresh/only-export-components */
import { useContext } from 'react';
import { useEffect } from 'react'
import { observer } from "mobx-react-lite";
import classes from '../styles/CartPage.module.css'
import MyButton from '../components/UI/button/MyButton';
import { Context } from '../main';

function CartPage() {
    const columns = ["Id", "Name", "Price", "Quantity", "Action"]
    const { cartStore } = useContext(Context);

    useEffect(() => {
        cartStore.getCart()
    }, [])

    const handleDelete = (event) => {
        let div = event.target.closest('div');
        cartStore.removeItem(div.id)
    }

    const handleCleanCart = () => {
        cartStore.cleanCart()
    }

    return (
        <>
            <h2>Ваша корзина</h2>
            <div>
                {cartStore.productCount > 0
                    ?
                    <div>
                        <table className={classes.table}>
                            <thead>
                                <tr className={classes.tableHeaderRow}>
                                    {columns.map(column =>
                                        <th key={column}>{column}</th>
                                    )}
                                </tr>
                            </thead>
                            <tbody>
                                {cartStore.cartItems.map((item, index) =>
                                    <tr key={index} className={classes.tableRow}>
                                        <td width="60">{item.productId}</td>
                                        <td>{item.productName}</td>
                                        <td>{item.price}</td>
                                        <td>{item.quantity}</td>
                                        <td width="100">
                                            <div id={item.productId}>
                                                <MyButton onClick={(e) => handleDelete(e)}>
                                                    Удалить
                                                </MyButton>
                                            </div>
                                        </td>
                                    </tr>
                                )}
                            </tbody>

                        </table>
                        <div className={classes.totalPrice}>
                            <div>
                                <MyButton onClick={() => handleCleanCart()}>Очистить корзину</MyButton>
                            </div>
                            <div>
                                Всего: {cartStore.totalPrice} руб.
                            </div>
                        </div>
                    </div>
                    : <div>Ваша корзина пуста</div>
                }

            </div>

        </>
    );
}

export default observer(CartPage);