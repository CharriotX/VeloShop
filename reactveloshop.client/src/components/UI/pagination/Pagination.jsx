/* eslint-disable react/prop-types */
import React from 'react';
import classes from '../pagination/pagination.module.css'

const Pagination = ({ totalPages, page, changePage }) => {

    const getPagesArray = (totalPages) => {
        let result = [];
        for (let i = 0; i < totalPages; i++) {
            result.push(i + 1)
        }
        return result;
    }
    let pagesArray = getPagesArray(totalPages);

    return (
        <div className={classes.pageWrapper}>
            {pagesArray.map(p =>
                <span
                    className={page === p ? classes.currentPage : classes.page}
                    key={p}
                    onClick={() => changePage(p)}
                >
                    {p}
                </span>
            )}
        </div>
    )
}

export default Pagination;