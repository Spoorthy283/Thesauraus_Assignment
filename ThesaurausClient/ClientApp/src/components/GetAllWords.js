import React, { Component } from 'react';
import { Display } from './Display';

export class GetAllWords extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        if (this.props.wordstore.words == 0 && this.props.wordstore.errMsg == null)
        this.props.getAllWords();
    }

    render() {
       
        if (this.props.wordstore.errMsg != null) {
            return (
                <div class="alert alert-danger alert-dismissible fade show">
                    {this.props.wordstore.errMsg}
                    <button type="button" className="btn-close" data-bs-dismiss="alert"></button>
                </div>
            );
        }


        else if (this.props.wordstore.words != null) {
            return (
                <div>
                    <h3>Library</h3>
                    <div className="m-2">
                        <input type="button" value="Refresh" onClick={this.props.getAllWords} />
                    </div>
                {
                    this.props.wordstore.words.length > 0 && 
                        (<Display words={this.props.wordstore.words}/>)
                }
              </div>
            );
        }
        else {
            return (
                <div></div>
            );
        }

    }
}