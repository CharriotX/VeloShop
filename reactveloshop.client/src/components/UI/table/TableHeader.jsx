/* eslint-disable no-undef */
/* eslint-disable react/prop-types */
import TableHeadCell from './TableHeadCell';

const TableHeader = ({ columns, sorting, sortTable }) => {
    return (
        <thead>
            <tr>
                {columns.map(column =>
                    <TableHeadCell column={column} sorting={sorting} key={column} sortTable={sortTable }></TableHeadCell>
                )}
            </tr>
        </thead>
    )
}

export default TableHeader;