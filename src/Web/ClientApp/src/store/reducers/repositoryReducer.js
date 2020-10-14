import * as actionTypes from '../actions/actionTypes';

// The data property stores data from the server, and the showSuccessModal property
// serves to show or hide the success modal when a POST/PUT/DELETE action is successful.

const initialState = {
    data: null,
    showSuccessModal: false
}


// Whenever any action is dispatched from the repositoryAction.js file, the reducer function
// is going to trigger and to accept the sent object inside the action parameter.

// These functions below update the state. First, they deeply clone the state object with
// the (...) spread operator. Then, they override the property that we want to update in the state object.
// Since objects and arrays are reference types, we need to execute a deep clone on them prior to making changes.
// This allows the state to be updated immutably.

const executeGetDataSuccess = (state, action) => {
    return {
        ...state,
        data: action.data
    }
}
const executePostDataSuccess = (state, action) => {
    return {
        ...state,
        showSuccessModal: true
    }
}
const executePutDataSuccess = (state, action) => {
    return {
        ...state,
        showSuccessModal: true
    }
}
const executeDeleteDataSuccess = (state, action) => {
    return {
        ...state,
        showSuccessModal: true
    }
}


// The reducer function is going to switch through the action types and
// execute the corresponding function above.

// The state parameter is used to update our initialState, and the action parameter
// is used to store the object sent from the repositoryAction.js file.

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actionTypes.GET_DATA_SUCCESS:
            return executeGetDataSuccess(state, action);
        case actionTypes.POST_DATA_SUCCESS:
            return executePostDataSuccess(state, action);
        case actionTypes.PUT_DATA_SUCCESS:
            return executePutDataSuccess(state, action);
        case actionTypes.DELETE_DATA_SUCCESS:
            return executeDeleteDataSuccess(state, action);
        default:
            return state;
    }
}
export default reducer;