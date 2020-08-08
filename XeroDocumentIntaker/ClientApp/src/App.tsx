import * as React from 'react';
import { Route } from 'react-router';
import Fileupload from './components/FileUpload'
import ReactTableView from './components/ReactTableView'


import './custom.css'


function App() {
    return (
        <div className="App">
            <Fileupload></Fileupload>
            <br /><br />
            <div>
                <ReactTableView></ReactTableView>
            </div>

            
        </div>
       
       
    );
}
export default App;
