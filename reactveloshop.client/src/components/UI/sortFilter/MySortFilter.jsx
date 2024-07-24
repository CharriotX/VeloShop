/* eslint-disable react/prop-types */
import classes from '../sortFilter/MySortFilter.module.css'
import { options } from "../sortFilter/options"
import MySelect from '../select/MySelect';
import MyInput from '../input/MyInput';

const MySortFilter = ({ changeSorting, searchTerm, changeSearchTerm, changePageSize }) => {

    const sortHandler = (e) => {
        switch (e.target.value) {
            case 'По алфавиту (возрастание)':
                changeSorting(options[0].value);
                break;
            case 'По алфавиту (убывание)':
                changeSorting(options[1].value);
                break;
            case 'По цене (возрастание)':
                changeSorting(options[2].value);
                break;
            case 'По цене (убывание)':
                changeSorting(options[3].value);
                break;
        }
    };

    return (
        <div className={classes.sortContainer}>
            <select value={options.value} onChange={(e) => sortHandler(e)} defaultValue="Сортировка">
                <option disabled>Сортировка</option>
                {options.map((option) => (
                    <option key={option.label}>{option.label}</option>
                ))}
            </select>
            <MySelect
                defaultValue="Кол-во отображаемых элементов"
                options={[
                    { value: "5", name: "5" },
                    { value: "10", name: "10" },
                    { value: "15", name: "15" },
                ]}
                onChange={value => changePageSize(value)}
            ></MySelect>
            <MyInput
                placeholder="Поиск.."
                value={searchTerm}
                onChange={e => changeSearchTerm(e.target.value)}
            ></MyInput>
        </div>
    )
}

export default MySortFilter;