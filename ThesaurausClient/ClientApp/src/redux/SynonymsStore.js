import * as ActionTypes from './ActionTypes';

export const SynonymsStore = (state = { 
    word:"",
    synonyms: "",
    errMsg: null
}, action) => {
    switch (action.type) {
        case ActionTypes.ADD_SYNONYM:
            return {
                ...state, synonyms: action.payload.synonyms, word:action.payload.word, errMsg: null
            }

        case ActionTypes.SEARCH_FAILED:
            return {
                ...state, synonyms: "", word:"", errMsg: action.payload
            }

      
        default:
            return state;
    }

}