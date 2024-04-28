import { useContext, useEffect } from 'react';
import './styles/App.css' 
import Navbar from './components/UI/navbar/Navbar';
import { BrowserRouter } from "react-router-dom"
import AppRouter from './components/AppRouter';
import { Context } from './main';
import {observer } from "mobx-react-lite"

function App() {

    const { store } = useContext(Context)

    useEffect(() => {
        if (localStorage.getItem("token")) {
            store.checkAuth()
        }
    }, [])

    return (
        <BrowserRouter>
            <Navbar></Navbar>
            <div className="container">
                <AppRouter></AppRouter>
            </div>
        </BrowserRouter>
    );    
}

export default observer(App);