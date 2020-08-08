import React from 'react';
import axios from 'axios';

interface IState {
    selectedFile: number;
}

const baseRestAPIPath = "https://localhost:5001/api";
class Fileupload extends React.Component {

    //global state definition
    readonly state = { selectedFile: '', uploadedBy: '', uploadStatus: 'N/A' };
   
    constructor(props : any) {
        super(props);
        this.state = {
            selectedFile: '',
            uploadedBy: '',
            uploadStatus:'N/A'
     
        };
        this.onFormSubmit = this.onFormSubmit.bind(this)
    }

    onFormSubmit(e) {
        e.preventDefault() // Stop form submit
        this.submit(e).then((response) => {
            this.setState({ uploadStatus: response.data });
        })
    }

    async submit(e : any) {
        e.preventDefault();
        const url = baseRestAPIPath+"/Upload/upload";
        const formData = new FormData();
        formData.append("formFile", this.state.selectedFile);
        formData.append("uploadedBy", this.state.uploadedBy);
        const config = {
            headers: {
                'content-type': 'application/x-www-form-urlencoded',              
            },
        };
        const HTTP = axios.create({
            withCredentials: true
        });
        return HTTP.post(url, formData, config);
    }

    setFile(e:any) {
        this.setState({ selectedFile: e.target.files[0] });
    }

    setUploadedBy(e: any) {
        if (this != undefined)

            this.setState({ uploadedBy: e.target.value });
    }

    render() {
        return (
            <div className="container-fluid">
                <form onSubmit={e => this.onFormSubmit(e)}>
                    <div className="col-sm-12 btn btn-primary">
                        File Upload
                        </div>
                    <br /> <br />
                    <input type="file" onChange={e => this.setFile(e)} />
                    <button className="btn btn-primary" type="submit">Upload</button>
                    <br /> 
                    Uploaded By* &nbsp;
                    <input type="text" accept=".pdf" value={this.state.uploadedBy} required onChange={e=> this.setUploadedBy(e)} />
                    <br />
                    <div style={{
                        color: this.state.uploadStatus == "file uploaded successfully" ? 'green' : 'black'
                    }}>Upload Status : {this.state.uploadStatus}</div>
             
                </form>
            </div>
        )
    }
}
export default Fileupload