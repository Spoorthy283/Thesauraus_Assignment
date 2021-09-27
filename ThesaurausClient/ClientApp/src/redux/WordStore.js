import * as ActionTypes from './ActionTypes';


export const WordStore = (state = {  
        words: [],
    errMsg: null    
   
}, action) => {
    switch (action.type) {        
        case ActionTypes.ADD_WORDS:
            return {
                ...state, words: action.payload, errMsg: null
            }

        case ActionTypes.NO_WORDS:
            return {
                ...state,  words: [], errMsg: action.payload
            }
        default:
            return state;
    }

}