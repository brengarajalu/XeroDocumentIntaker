import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Fileupload from './components/FileUpload'
import GridView from './components/GridView'
import ThreatReportTable from './components/ReactTableView'


import './custom.css'


function App() {
    return (
        <div className="App">
            <Fileupload></Fileupload>
            <br /><br />
            <div>
                <ThreatReportTable></ThreatReportTable>
            </div>

            
        </div>
       
       
    );
}
export default App;
