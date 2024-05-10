/* eslint-disable react/prop-types */
import classes from '../button/MyButton.module.css'

const MyButton = ({ children, ...props }) => {
    return (
        <button {...props}  className={classes.myBtn}  >
            {children}
        </button>
    )
}

export default MyButton;