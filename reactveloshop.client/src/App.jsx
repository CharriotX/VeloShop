import { useContext, useEffect, useState } from 'react';
import './styles/App.css'
import Navbar from './components/UI/navbar/Navbar';
import { BrowserRouter } from "react-router-dom"
import AppRouter from './components/AppRouter';
import { Context } from './main';
import { observer } from "mobx-react-lite"
import UserContext from './context/userContext';

function App() {

    const { authStore } = useContext(Context)
    const [loading, setIsLoading] = useState(true);
    useEffect(() => {
        if (localStorage.getItem("token")) {
            authStore.checkAuth()
                .then(setIsLoading(false))
        }
        setIsLoading(false)
    }, [])


    return (
        <UserContext.Provider value={{ loading }}>
            <BrowserRouter>
                <Navbar></Navbar>
                <div className="container">
                    <AppRouter></AppRouter>
                </div>
            </BrowserRouter>
        </UserContext.Provider>
    );
}

export default observer(App);