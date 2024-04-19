import { useContext, useEffect, useState } from 'react';
import CategoryService from './services/CategoryService';
import { useFetching } from './hooks/useFetching';
import './styles/App.css' 
import Navbar from './components/UI/navbar/Navbar';
import { BrowserRouter } from "react-router-dom"
import AppRouter from './components/AppRouter';
import { Context } from './main';

function App() {

    const { store } = useContext(Context)

    useEffect(() => {
    },[])

    return (
        <BrowserRouter>
            <Navbar></Navbar>
            <div className="container">
                <AppRouter></AppRouter>
            </div>
        </BrowserRouter>
    );    
}

export default App;