import React, { Component } from 'react';
import {  Row, Col,Label } from 'reactstrap';
import { Display } from './Display';

export class GetSynonyms extends Component {

    constructor(props) {
        super(props);
        this.state = {
            searchKeyword: "",
            errMsg: this.props.synonymsstore.errMsg,
            isError: this.props.synonymsstore.errMsg == null ? false : true
        };
        this.handleChange = this.handleChange.bind(this);
        this.searchKeyword = this.searchKeyword.bind(this);
    }
    handleChange(event) {
        this.setState({ searchKeyword : event.target.value });
    }
    searchKeyword(event) {
        event.preventDefault();
        if (this.state.searchKeyword == "") {
            this.setState({ isError: true, errMsg: "Search Keyword is empty" })
        }
        else {
            this.props.searchWord(this.state.searchKeyword)
        }        
    }

    render() {
      
            return (
                <div>
                    <h3>Get Synonyms</h3>

                    {this.state.isError && (
                        <div className="alert alert-danger alert-dismissible fade show">
                            {this.state.errMsg}
                        </div>
                    )}

                    <form onSubmit={this.searchKeyword}>
                        <Row className="form-group">
                            <Label htmlFor="keyword" sm={2}>
                                Word:
                            </Label>
                            <Col sm={9}>
                                <input type="text" id="keyword" name="keyword" className="keyword" value={this.state.keyword} onChange={this.handleChange} autoFocus />
                            
                                <input type="submit" className="submit ml-2" value="Search" />
                            </Col>
                        </Row>                       
                    </form>
                    <div className="mt-3">
                        {this.props.synonymsstore.synonyms != "" && (

                            <Display words={this.props.synonymsstore} />

                            
                            
                        )}
                       
                    </div>
                </div>

            );
       
        
    }
}