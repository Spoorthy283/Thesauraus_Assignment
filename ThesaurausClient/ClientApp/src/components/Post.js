import React, { Component } from 'react';
import { Label, Row, Col } from 'reactstrap';
export class Post extends Component {

    constructor(props) {
        super(props);
        this.state = {
            word: "",
            synonyms:[""]
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.AddTextBox = this.AddTextBox.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleWordChange = this.handleWordChange.bind(this);
    }

    handleWordChange(event) {
        this.setState({ word: event.target.value });
    }
    handleChange(index, event) {
        var synonyms = this.state.synonyms.slice(); // Make a copy of the emails first.
        synonyms[index] = event.target.value; // Update it with the modified email.
        this.setState({ synonyms: synonyms }); // Update the state.
    }

    handleSubmit(event) {
      
        event.preventDefault();
        if(this.state.word != "")
            this.props.postComment(this.state.word, this.state.synonyms)
    }
    AddTextBox(event) {
        this.setState({
            synonyms: this.state.synonyms.concat("")
        })
    }
    render() {
      
            return (
                <div>
                    {this.props.addwordstore.successMsg != null && (
                        <div className="alert alert-success alert-dismissible fade show">
                            <strong>Success!</strong> {this.props.addwordstore.successMsg}
                        </div>
                    )}
                    {this.props.addwordstore.errMsg != null && (

                        <div className="alert alert-danger alert-dismissible fade show">
                            {this.props.addwordstore.errMsg}
                        </div>
                    )}
                    <h3>Add New</h3>
                    <form onSubmit={this.handleSubmit}>

                        <Row className="form-group">
                            <Label htmlFor="inputword" sm={3}>
                                Word:
                            </Label>
                            <Col sm={8}>
                                <input id="inputword" type="text" value={this.state.word} onChange={e => this.handleWordChange(e)} />
                            </Col>
                        </Row>

                        {this.state.synonyms.map((x, i) => {
                            return (
                                <Row className="form-group">
                                    {i === 0 ? <Col md={3}>Synonyms: </Col> : <Col sm={3}></Col>}

                                    <Col sm={8}>
                                        <input type="text" value={x} onChange={e => {
                                            this.handleChange(i, e)
                                        }} />
                                   
                                        {i === (this.state.synonyms.length - 1) &&
                                            <input type="button" value="+" className="ml-1" onClick={this.AddTextBox} />
                                   
                                        }
                                    </Col>
                                </Row>
                            );
                        })}
                        <Row>
                            <Col sm={8}>
                                <input type="submit" className="" value="Submit" />
                                </Col>
                        </Row>
                    </form>


                </div>
            );

       
    }
}