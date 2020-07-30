import React, { Component } from 'react'
import ReactTable from 'react-table';
import useRowSelect from 'react-table';
import Modal from 'react-modal';
import "react-table/react-table.css";
import axios from 'axios';

const divStyleBlock = {
    display: 'block' as const,

};
const divStyleNone = {
    display: 'none' as const,

};


const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)'
    }
};


interface IState {
    health: number;
}

class ThreatReportTable extends React.Component {

   
    readonly state = {
        reports: [], selectedDetail: { reportDetails: {}}, reportDetails: [], selected: '', showInfo: false, hideInfo: true };



    getDetailedInfo(id:any) {
        axios.get("https://localhost:5001/api/upload/stats").then(response => {
            //console.log(response.data);  
            this.setState({
                reports: response.data
            });
        });
    }

    handleButtonClick = (source, e) => {

        this.setState({ showInfo: true });
        axios.get("https://localhost:5001/api/upload/document?id=" + source.id).then(response => {
            //console.log(response.data);
            this.setState({ selectedDetail: response.data });
            
            console.log(this.state.selectedDetail);
        });
    }; 

  
    componentDidMount() {
        Modal.setAppElement('body');
        axios.get("https://localhost:5001/api/upload/stats").then(response => {
            //console.log(response.data);  
            this.setState({
                reports: response.data,
                showInfo : false
            });
        });
    }  

    constructor(props:any) {    
        super(props);

        this.closeModal = this.closeModal.bind(this);

   
    }

    renderDetailedInfo() {
        return (
            <div>
                <h4 id='title'>Report Statistics</h4>
                <table id='reports'>
                    <thead><tr><th></th><th>id</th><th>File Count</th><th>Total File Size</th><th>Total Amount</th><th>Total Amount Due</th></tr></thead>
                    <tbody>
       
                    </tbody>
                </table>
            </div>
        )
    }
    closeModal() {
        this.setState({ showInfo: false });
     
    }
    showModal() {
        this.state.showInfo = true;

    }

    render() {

        const divStyle = {
            textAlign: 'center' as const,

        };
        const buttonStyle = {
            alignItems: 'center' as const,

        };
       
        const columns = [
            {
                width: 200,
                accessor: "name",
                Cell: ({ original }) => (
                    <button style={divStyle} className="btn btn-primary" onClick={e => this.handleButtonClick(original, e)} type="submit">View</button>
                )
            },
            {
            Header: 'id',
            accessor: 'id'
        }, {
            Header: 'fileCount',
            accessor: 'fileCount'
            },
            {
                Header: 'totalfileSize',
                accessor: 'totalfileSize'
            },
            {
                Header: 'totalAmount',
                accessor: 'totalAmount'
            },
            {
                Header: 'totalAmountDue',
                accessor: 'totalAmountDue'
            }]

        const reportdetailcolumns = [
        
            {
                Header: 'UploadedBy',
                accessor: 'uploadedBy'
            },
            {
                Header: 'UploadedTimeStamp',
                accessor: 'createdDate'
            },
            {
                Header: 'FileSize',
                accessor: 'fileSize'
            },
            {
                Header: 'VendorName',
                accessor: 'reportDetails.vendor'
            },
            {
                Header: 'InvoiceDate',
                accessor: 'reportDetails.invoiceDate'
            },
            {
                Header: 'TotalAmount',
                accessor: 'reportDetails.totalAmount'
            },
            {
                Header: 'TotalAmountDue',
                accessor: 'reportDetails.totalAmountDue'
            },
            {
                Header: 'Currency',
                accessor: 'reportDetails                                                                                               .currency'
            },
            {
                Header: 'TaxAmount',
                accessor: 'reportDetails.tax'
            },
            {
                Header: 'ProcessingStatus',
                accessor: ''
            }
        ]

        return (

            <div style={divStyle}>

                <div className="col-sm-12 btn btn-primary">
                    Report Statistics
                </div>
                <br/>
                <ReactTable
                    data={this.state.reports}
                    columns={columns}
                 
                    defaultPageSize={2}
                    
                    pageSizeOptions={[2, 4, 6]}
                />
                    <Modal isOpen={this.state.showInfo}
                        style={customStyles}
                        contentLabel="Example Modal"
                    onRequestClose={this.closeModal}>
                   
                    <table id="reports">
                        <thead><tr><th>Vendor</th><th>InvoiceDate</th><th>Tax</th><th>TotalAmount</th><th>TotalAmountDue</th></tr></thead>
                            <tbody>
                                <tr>
                                <td>{this.state.selectedDetail.reportDetails["vendor"]}</td>
                                <td>{this.state.selectedDetail.reportDetails["invoiceDate"]}</td>
                                <td>{this.state.selectedDetail.reportDetails["Tax"]}</td>
                                <td>{this.state.selectedDetail.reportDetails["totalAmount"]}</td>
                                <td>{this.state.selectedDetail.reportDetails["totalAmountDue"]}</td>
                                </tr>
                            </tbody>
                    </table>
                   
                   
                    <button style={buttonStyle} onClick={this.closeModal}>close</button>
                </Modal>

              
        
            </div>
            
        )
    } 

}


export default ThreatReportTable;