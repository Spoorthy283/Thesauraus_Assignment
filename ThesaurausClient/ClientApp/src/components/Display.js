import React from 'react';
import { Table } from 'reactstrap'

export const Display = (props) => {
   

    if ((Array.isArray(props.words) && props.words.length > 0) || typeof props.words === 'object') {
        return (
            <div className="m-1">
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Word</th>
                            <th>Synonyms</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            Array.isArray(props.words) ?
                            (props.words.map((item, i) => (
                            <tr key={i}>
                                <td>{item.word}</td>
                                <td>{item.synonyms}</td>
                            </tr>
                            )))
                                :
                            (
                                <tr>
                                    <td>{props.words.word}</td>
                                    <td>{props.words.synonyms}</td>
                                </tr>
                            )
                        }
                    </tbody>
                </Table>
            </div>
        );
    }
    else {
        return (
            <div></div>
        );
    }

};


