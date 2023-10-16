export function AlertReducer(state={}, action){
    switch(action.type){
        case 'ALERT_INFO':
            return{
                alert:{
                    type: 'info',
                    message: action.message
                }              
            };
        case 'ALERT_DANGER':
            return{
                alert:{
                    type: 'error',
                    message: action.message
                }
            };
        case 'ALERT_SUCCESS':
            return{
                alert:{
                    type: 'success',
                    message: action.message
                }
            };
        case 'ALERT_WARNING':
            return{
                alert:{
                    type: 'warning',
                    message: action.message
                }
            };
        case 'ALERT_CLEAR':
            return {};
        default:
            return state;
    }
}
