import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './Layout';
import { GetSynonyms } from './GetSynonyms';
import { GetAllWords } from './GetAllWords';
import { Post } from './Post';
import { searchWord, getAllWords, postComment} from '../redux/ActionCreators'
import { connect } from 'react-redux';



const mapStateToProps = state => ({
    wordstore: state.wordstore,
    synonymsstore: state.synonymsstore,
    addwordstore: state.addwordstore
});

const mapDispatchToProps = (dispatch) => ({
    searchWord: (keyword) => dispatch(searchWord(keyword)),
    getAllWords: () => dispatch(getAllWords()),
    postComment: (word, synonyms) => dispatch(postComment(word, synonyms))
});


class Main extends Component {
   
    render() {
        return (
            <Layout>
                <Route path='/post' component={() => <Post postComment={this.props.postComment} addwordstore={this.props.addwordstore}/>} />
                
                <Route path='/getsynonyms' component={() => <GetSynonyms
                                                    searchWord={this.props.searchWord}
                                                    synonymsstore={this.props.synonymsstore}
                                                />}/>

                <Route path='/getallwords' component={() => <GetAllWords wordstore={this.props.wordstore} getAllWords={this.props.getAllWords} />} />
                
            </Layout>
        );
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(Main);