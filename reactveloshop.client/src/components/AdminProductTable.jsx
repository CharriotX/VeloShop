/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */
import React, { useMemo, useState } from 'react';
import { useEffect } from 'react';
import { useFetching } from '../hooks/useFetching';
import ProductService from '../services/ProductService';
import TableHeader from './UI/table/TableHeader';
import classes from '../components/UI/table/Table.module.css'
import { Link } from "react-router-dom"
import Pagination from './UI/pagination/Pagination';
import MyInput from '../components/UI/input/MyInput'
import MyButton from './UI/button/MyButton';
import MyModal from './UI/modal/MyModal';
import DeleteProductButton from './UI/button/DeleteProductButton';

const AdminProductTable = () => {
    const [products, setProducts] = useState([]);
    const [page, setPage] = useState(1);
    const [totalRecords, setTotalRecords] = useState(0)
    const [totalPages, setTotalPages] = useState(0);
    const [pageSize, setPageSize] = useState(10);
    const [searchTerm, setSearchTerm] = useState('');
    const [sorting, setSorting] = useState({ column: "Id", order: "asc" })
    const [deleteModal, setDeleteModal] = useState(false)
    const [productId, setProductId] = useState(" ")

    const columns = ["Id", "Name", "Brand", "Subcategory", "Price", "Actions"]

    const [fetchProducts, isLoading, error] = useFetching(async () => {
        var response = await ProductService.getAll(searchTerm, sorting.column, sorting.order, page, pageSize);
        setProducts(response.data.items)
        setPage(response.data.pageNumber)
        setTotalPages(response.data.totalPages)
        setTotalRecords(response.data.totalRecords)
    })
    useEffect(() => {
        fetchProducts()
    }, [page, sorting, searchTerm])

    const sortTable = (newSorting) => {
        setSorting(newSorting)
        setPage(1)
        setPageSize(10)
    }

    const changePage = (page) => {
        setPage(page)
    }

    const handleDelete = (event) => {
        setDeleteModal(true)
        let div = event.target.closest('div');
        setProductId(div.id)
    }

    const handleUpdate = (event) => {
        let div = event.target.closest('div');
        setProductId(div.id)
    }
    console.log(productId)
    return (
        <>
            <MyInput
                placeholder="Поиск по названию"
                value={searchTerm}
                onChange={e => {
                    setSearchTerm(e.target.value)
                }}
            >
            </MyInput>
            <table className={classes.table}>
                <TableHeader columns={columns} sorting={sorting} sortTable={sortTable}></TableHeader>
                <tbody>
                    {products.map(product =>
                        <tr className={classes.tableRow} key={product.id}>
                            <td width="50">{product.id}</td>
                            <td>{product.name}</td>
                            <td>{product.brandName}</td>
                            <td>{product.subcategoryName}</td>
                            <td>{product.price}</td>
                            <td className={classes.actionButtons}>
                                <div id={product.id}>
                                    <Link to={`/updateProduct/${product.id}`}>
                                        <MyButton onClick={(e) => handleUpdate(e)}>
                                            update
                                        </MyButton>
                                    </Link>
                                </div>
                                <div id={product.id}>
                                    <MyButton onClick={e => handleDelete(e)}>delete</MyButton>
                                    <MyModal
                                        setVisible={setDeleteModal}
                                        visible={deleteModal}
                                    >
                                        <DeleteProductButton setVisible={setDeleteModal} id={productId} ></DeleteProductButton>
                                    </MyModal>
                                </div>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
            <Pagination changePage={changePage} page={page} totalPages={totalPages}></Pagination>
        </>

    )
}

export default AdminProductTable;