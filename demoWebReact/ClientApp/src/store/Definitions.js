const requestDefinitionsType = 'REQUEST_DEFINITIONS';
const receiveDefinitionsType = 'RECEIVE_DEFINITIONS';
const initialState = { definitionPacket: { packetId: 0, definitions: [] }, isLoading: false };

export const actionCreators = {
    requestDefinitions: startDateIndex => async (dispatch, getState) => {
        if (startDateIndex === getState().definitions.startDateIndex) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }
        dispatch({ type: requestDefinitionsType, startDateIndex });
        const url = `api/Definitions/GetAll`;
        const response = await fetch(url);
        const definitions = await response.json();
        dispatch({ type: receiveDefinitionsType, startDateIndex, definitions });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;
    if (action.type === requestDefinitionsType) {
        return {
            ...state,
            startDateIndex: action.startDateIndex,
            isLoading: true
        };
    }
    if (action.type === receiveDefinitionsType) {
        return {
            ...state,
            startDateIndex: action.startDateIndex,
            definitionPacket: {packetId: 1, definitions: action.definitions },
            isLoading: false
        };
    }
    return state;
};
