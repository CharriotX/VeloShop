/* eslint-disable react/prop-types */
/* eslint-disable no-unused-vars */
import React from 'react';
import classes from '../table/Table.module.css'

const TableContent = ({ entires, columns }) => {
    console.log(entires)
    return (
        <tbody>
            {entires.map(entry =>
                <tr key={entry.id}>
                    {columns.map(column =>
                        <td className={classes.headerTableCell} key={column}>{entry[column]}</td>
                    )}
                </tr>
            )}
        </tbody>
    )
}

export default TableContent;