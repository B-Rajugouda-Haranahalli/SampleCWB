var logger = {
    log: log,
    logError: logError,
    logWarning: logError,
    logSuccess: logError,
    logIt: logIt
}

function log(title, message, data, source, showToast, positionclass) {
    logIt(title, message, data, source, showToast, 'info', positionclass);
}

function logError(title, message, data, source, showToast, positionclass) {
    logIt(title, message, data, source, showToast, 'error', positionclass);
}

function logWarning(title, message, data, source, showToast, positionclass) {
    logIt(title, message, data, source, showToast, 'warning', positionclass);
}

function logSuccess(title, message, data, source, showToast, positionclass) {
    logIt(title, message, data, source, showToast, 'success', positionclass);
}

function logIt(title, message, data, source, showToast, toastType, positionClass) {
    if (showToast) {
        switch (toastType) {
            case 'error':
                toastr.error(message, title, options = { positionClass: positionClass });
                break;
            case 'success':
                toastr.success(message, title, options = { positionClass: positionClass });
                break;
            case 'warning':
                toastr.warning(message, title, options = { positionClass: positionClass });
                break;
            default:
                toastr.info(message, title, options = { positionClass: positionClass });
        }
    }

}
//logger.log("Hello", null, null, true)
//toastr.info("Enjoy your stay", 'Welcome to Sumo', options = { positionClass: 'toast-bottom-right' })
//toastr.warning("Enjoy your stay", 'Welcome to Sumo', options = { positionClass: 'toast-bottom-right' })
//toastr.success("Enjoy your stay", 'Welcome to Sumo', options = { positionClass: 'toast-bottom-right' })

// Display a info toast, with no title
//toastr.info('Are you the six fingered man?')

//// Display a warning toast, with no title
//toastr.warning('My name is Inigo Montoya. You Killed my father, prepare to die!')

//// Display a success toast, with a title
//toastr.success('Have fun storming the castle!', 'Miracle Max Says')

//// Display an error toast, with a title
//toastr.error('I do not think that word means what you think it means.', 'Inconceivable!')

//options = {
//    tapToDismiss: true,
//    toastClass: 'toast',
//    containerId: 'toast-container',
//    debug: false,
//    fadeIn: 300,
//    fadeOut: 1000,
//    extendedTimeOut: 1000,
//    iconClass: 'toast-info',
//    positionClass: 'toast-top-right',
//    timeOut: 5000, // Set timeOut to 0 to make it sticky
//    titleClass: 'toast-title',
//    messageClass: 'toast-message'
//}
