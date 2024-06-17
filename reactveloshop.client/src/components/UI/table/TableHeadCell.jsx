/* eslint-disable react/prop-types */
import React from 'react';
import classes from '../table/Table.module.css'

const TableHeadCell = ({ column, sorting, sortTable }) => {

    const isDescSorting = sorting.column === column && sorting.order === "desc";
    const isAscSorting = sorting.column === column && sorting.order === "asc";

    const sortOrder = isDescSorting ? "asc" : "desc";
    

    return (
        <th className={classes.headerTableCell} key={column} onClick={() => sortTable({column, order: sortOrder })}>
            {column }
            {isDescSorting && <span>▼</span>}
            {isAscSorting && <span>▲</span>}
        </th>

    )
}

export default TableHeadCell;