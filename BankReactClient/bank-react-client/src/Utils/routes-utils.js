
async function CallRoute(route, data, method, bearer = '') {
    const controller = new AbortController();
    const id = setTimeout(() => controller.abort(), 7000);
    let response = null;

    switch (method.toUpperCase()) {
        case "GET":
            let routeComplete = data !== null ?
                route + "?" + new URLSearchParams(data)
                : route;

            response = await fetch(routeComplete,
                {
                    method: method,
                    headers: {
                       Authorization: 'Bearer ' + bearer,
                    },
                    //credentials: "include",
                    signal: controller.signal,
                });
            break;
        case "GET-FILE":
            let routeCompleteFile = data !== null ?
                route + "?" + new URLSearchParams(data)
                : route;
                
            response = await fetch(routeCompleteFile,
                {
                    method: "GET",
                    headers: {
                        Authorization: 'Bearer ' + bearer,
                    },
                    credentials: "include",
                    signal: controller.signal
                });
            break;
        case "POST-FILE":
            response = await fetch(route,
                {
                    method: "POST",
                    headers: {
                        Authorization: 'Bearer ' + bearer,
                    },
                    signal: controller.signal,
                    credentials: "include",
                    body: data
                });
                break;
        case "POST":
        default:
            response = await fetch(route,
                {
                    method: method,
                    headers: {
                        Accept: 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + bearer,
                    },
                    //credentials: "include",
                    signal: controller.signal,
                    body: JSON.stringify(data)
                });
            break;
    }

    clearTimeout(id);
    return response;
}

async function GetTokenUser(msalInstance) {
    var tokenAZ = '...';//obtener de sessionStorage
    return tokenAZ;
}

export const ConsumeRoute = {
    CallRoute,
    GetTokenUser
}