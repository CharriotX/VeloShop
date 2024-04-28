/* eslint-disable react/prop-types */
import classes from '../input/MyInput.module.css'

const MyInput = (props) => {
    return (
        <input className={classes.myInput} {...props}>
        </input>
    )
}

export default MyInput;