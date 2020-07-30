import React from 'react';
import axios from 'axios';


interface IProps {
    superhero: string;
}

interface IState {
    selectedFile: number;
}

class Fileupload extends React.Component {

 
    readonly state = { selectedFile: '', uploadedBy:'' };

   
    constructor(props : any) {
        super(props);
        this.state = {
            selectedFile: '',
            uploadedBy: ''
     
        };
    }

    async submit(e : any) {
        e.preventDefault();
        const url = `https://localhost:5001/api/Upload/upload`;
        const formData = new FormData();
        formData.append("formFile", this.state.selectedFile);
        formData.append("uploadedBy", this.state.uploadedBy);
        const config = {
            headers: {
                //'content-type': 'multipart/form-data',
                'content-type': 'application/x-www-form-urlencoded',
                
                'token': 'xxxx'                //withCredentials: true
            },
        };
        const HTTP = axios.create({
            withCredentials: true
        });

        //return post(url, formData, config);
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
                <form onSubmit={e => this.submit(e)}>
                    <div className="col-sm-12 btn btn-primary">
                        File Upload
                        </div>
                    <br /> <br />
         
                    <input type="file" onChange={e => this.setFile(e)} />
                    <button className="btn btn-primary" type="submit">Upload</button>
                    <br /> 
                    Uploaded By &nbsp;
                    <input type="text" value={this.state.uploadedBy} onChange={e=> this.setUploadedBy(e)} />
                    <br />
             
                </form>
            </div>
        )
    }
}
export default Fileupload