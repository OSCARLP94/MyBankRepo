
function showInfo(message){
    return{
        type: 'ALERT_INFO',
        message
    }
}

function showDanger(message){
    return{
        type:'ALERT_DANGER',
        message
    }
}

function showSuccess(message){
    return{
        type: 'ALERT_SUCCESS',
        message
    }
}

function showWarning(message){
    return{
        type: 'ALERT_WARNING',
        message
    }
}

function clear(){
    return{type: 'ALERT_CLEAR'}
}

export const AlertActions={
    showInfo,
    showDanger,
    showSuccess,
    showWarning,
    clear
};
