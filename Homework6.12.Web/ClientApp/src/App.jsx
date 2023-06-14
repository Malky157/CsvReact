import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Route, Routes } from 'react-router';
import Layout from './Layout';
import Home from './Home';
import Upload from './Upload';
import Generate from './Generate';


const App = () => {
    return <>
        <Layout>
            <Routes>
                <Route exact path='/' element={<Home />} />
                <Route exact path='/upload' element={<Upload />} />
                <Route exact path='/generate' element={<Generate />} />
            </Routes>
        </Layout>
    </>
}

export default App;