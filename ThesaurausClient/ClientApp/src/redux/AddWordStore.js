import * as ActionTypes from './ActionTypes';

export const AddWordStore = (state = {
    errMsg: null,
    successMsg:null
}, action) => {
    switch (action.type) {
        case ActionTypes.POST_SYNONYM:
            return {
                ...state,  errMsg: null, successMsg:action.payload
            }

        case ActionTypes.POST_FAILED:
            return {
                ...state,   errMsg: action.payload, successMsg:null
            }


        default:
            return state;
    }

}