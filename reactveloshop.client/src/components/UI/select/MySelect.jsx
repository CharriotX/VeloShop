/* eslint-disable react/prop-types */
import classes from '../select/MySelect.module.css'
import { useState } from "react";

const MySelect = ({ options, defaultValue, onChange }) => {

    const [selectedValue, setSelectedValue] = useState('');


    const handleSelectChange = (event) => {
        const newValue = event.target.value;
        setSelectedValue(newValue);
        if (onChange) {
            onChange(newValue);
        }
    };

    return (
        <select defaultValue={defaultValue} className={classes.mySelect} onChange={handleSelectChange} >
            <option disabled>{defaultValue}</option>
            {options.map((option,index) =>
                <option key={index} value={option.id}>{option.name}</option>
            )}
        </select>
    )
}

export default MySelect;