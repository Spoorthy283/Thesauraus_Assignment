import * as ActionTypes from './ActionTypes';

export const addSynonyms = (synonyms) => ({
    type: ActionTypes.ADD_SYNONYM,
    payload: synonyms
})
export const searchFailed = (errmsg) => ({   
    type: ActionTypes.SEARCH_FAILED,
    payload: errmsg
})
export const searchWord = (keyword) => (dispatch) => {    
    return fetch('api/get/' + keyword)
        .then(response => {
            if (response.status === 200) {                
                return response;
            }
            else if (response.status === 404) {
                var error = new Error("Synonyms not found");
                throw error;
            }
            else {
                var error = new Error('Error ' + response.status + ': ' + response.statusText );
                throw error;
            }
        }, error => {
            var errmess = new Error(error.message);
            throw errmess;
        })
        .then(response => response.json())
        .then(responseObj => { 
                
            dispatch(addSynonyms({ "synonyms": responseObj.synonyms,"word":keyword }));           
        })
        .catch(error => dispatch(searchFailed(error.message)));

}


export const addWords = (words) => ({
    type: ActionTypes.ADD_WORDS,
    payload: words
})
export const noWords = (errmsg) => ({
    type: ActionTypes.NO_WORDS,
    payload: errmsg
})
export const getAllWords = () => (dispatch) => {
    return fetch('api/getallwords')
        .then(response => {
            if (response.status === 200) {
                return response;
            }
            else if (response.status === 400) {
                var error = new Error("Library is empty");
                throw error;
            }
            else {
                var error = new Error('Error ' + response.status + ': ' + response.statusText);
                throw error;
            }
        }, error => {
            var errmess = new Error(error.message);
            throw errmess;
        })
        .then(response => response.json())
        .then(responseObj => {
                dispatch(addWords(responseObj.words));           
        })
        .catch(error => dispatch(noWords("Library is empty" )));

}
export const postSuccess = (successMsg) => ({
    type: ActionTypes.POST_SYNONYM,
    payload: successMsg
})
export const postFailed = (errMsg) => ({
    type: ActionTypes.POST_FAILED,
    payload: errMsg
})
export const postComment = (word,synonyms) => (dispatch) => {

    const obj = {
        word: word,
        synonyms:"",
        synonymsList: synonyms
    }   

    return fetch('api/postwordsynonyms', {
        method: "POST",
        body: JSON.stringify(obj),
        headers: {
            'Content-Type': 'application/json'
        },
        credentials: 'same-origin'

    })
        .then(response => {
            if (response.status === 201) {
                return response;
            }
            else if (response.status === 400) {
                var error = new Error("Failed to add word and synonyms");
                throw error;
            }
            else {
                var error = new Error('Error ' + response.status + ':' + response.statusText);
                throw error;
            }
        },
        error => {
            var errmess = new Error(error.message);
            throw errmess
        })
        .then(response => response.json())
        .then(response => {
            dispatch(getAllWords())
             dispatch(postSuccess(response.msg))
           
            })
        .catch(error => {
            dispatch(postFailed(error.message))
        });
}