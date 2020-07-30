
import axios from 'axios';
import React, { Component } from 'react'

interface IProps {
    superhero: string;
}

interface IState {
    selectedFile: number;
}


class GridView extends React.Component {
    readonly state = { reports: []};

    constructor(props:any) {
        super(props)
        
    }

    componentDidMount() {
        axios.get("https://localhost:5001/api/upload/stats").then(response => {
            //console.log(response.data);  
            this.setState({
                reports: response.data
            });
        });
    }  

    renderTableHeader() {
        let header = Object.keys(this.state.reports[0])
        return header.map((key, index) => {
            return <th key={index}>{key.toUpperCase()}</th>
        })
    }

    renderTableData() {
        return this.state.reports.map((r, index) => {
            const {id, fileCount, totalfileSize, totalAmount, totalAmountDue } = r //destructuring
            return (
                <tr key={id}>
   
                    <td>{id}</td>
                    <td>{fileCount}</td>
                    <td>{totalfileSize}</td>
                    <td>{totalAmount}</td>
                    <td>{totalAmountDue}</td>
                </tr>
            )
        })
    }

    render() {
        return (
            <div>
                <h4 id='title'>Report Statistics</h4>
                <table id='reports'>
                    <thead><tr><th>view</th><th>ID</th><th>File Count</th><th>Total File Size</th><th>Total Amount</th><th>Total Amount Due</th></tr></thead>
                    <tbody>
                        {this.renderTableData()}
                    </tbody>
                </table>
            </div>
        )
    }
}
export default GridView
