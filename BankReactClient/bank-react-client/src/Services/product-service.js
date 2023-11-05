import {ConsumeRoute} from "../Utils/routes-utils";
import {ClientSettings} from "../clientsettings";
import {ProductActions} from "../Redux/Actions/product-actions";
import {AlertActions} from "../Redux/Actions/alert-actions";
import { ConstValues } from "../clientsettings";

async function GetListProductsClient(dispatch, userClient){
    try{
        var response = await ConsumeRoute.CallRoute(ClientSettings.UrlBaseInfoBankApi+"ProductInfo/InfoProductsByCient", {userClient: userClient}, "GET", null);
        await HandleResponseService(response, dispatch);

        let json = await response.clone().json();
        if (response.status == 200 && json.response === true && json.error === "") {
            dispatch(ProductActions.Full(json.data));
        }

        return null;
    }catch(err){
        dispatch(AlertActions.showDanger("An error occurred while fetching the data: "+err));
    }
}

const GetListTransactionsByParams= async(userName, productNumber, maxCount=10, fromDate, untilDate)=>{
    try{
        var objSearch = {clientUserName: userName, productNumber: productNumber, maxCount:maxCount, fromDate:fromDate, untilDate:untilDate, 
            typeTransactions:[ConstValues.CodeDepositTransac, ConstValues.CodeFundsTransfTransac, ConstValues.CodeWithdrawTransac]};
        
        let route = ClientSettings.UrlBaseInfoBankApi+"ProductInfo/ListTransactionsClient";
        let bearer = null;

        var response = await fetch(route,{
            method: "POST",
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + bearer,
            },
            //credentials: "include",
            body: JSON.stringify(objSearch)
        });

        await HandleResponseServiceFetch(response);
        
        const data = await response.clone().json();
        //console.log(data);
        return data.data;
    }catch(err){
        throw new Error("An error occurred while fetching the data: "+err);
    }
}

async function CallWithdrawalTransaction(dispatch, userName, productNumber, effectDate, value, cause, adittional){
    try{
        var objCall = {clientUserName: userName, originProductNumber: productNumber, typeTransaction: ConstValues.CodeWithdrawTransac
            , effectDate:effectDate, userOrClient: "test", value:value, causeTransaction:cause, additional:adittional};

        var response = await ConsumeRoute.CallRoute(ClientSettings.UrlBaseTransacBankApi+"Transaction/GenericTransactionCall", objCall, "POST", null);
        await HandleResponseService(response, dispatch);

        let json = await response.clone().json();
        if (response.status == 200 && json.response === true && json.error === "") {
            return json.data;
        }

        return null;
    }catch(err){
        dispatch(AlertActions.showDanger("An error occurred while fetching the data: "+err));
        return null;
    }
}

async function CallDepositTransaction(dispatch, userName, productNumber, effectDate, value, cause, adittional){
    try{
        var objCall = {clientUserName: userName, destinyProductNumber: productNumber, typeTransaction: ConstValues.CodeDepositTransac
            , effectDate:effectDate, userOrClient: "test", value:value, causeTransaction:cause, additional:adittional};

        var response = await ConsumeRoute.CallRoute(ClientSettings.UrlBaseTransacBankApi+"Transaction/GenericTransactionCall", objCall, "POST", null);
        await HandleResponseService(response, dispatch);

        let json = await response.clone().json();
        if (response.status == 200 && json.response === true && json.error === "") {
            return json.data;
        }

        return null;
    }catch(err){
        dispatch(AlertActions.showDanger("An error occurred while fetching the data: "+err));
        return null;
    }
}

async function CallFundsTransferTransaction(dispatch, userName, originProductNumber, destinyProductNumber,effectDate, value, cause, adittional){
    try{
        var objCall = {clientUserName: userName, originProductNumber:originProductNumber, destinyProductNumber: destinyProductNumber, typeTransaction: ConstValues.CodeFundsTransfTransac
            , effectDate:effectDate, userOrClient: "test", value:value, causeTransaction:cause, additional:adittional};

        var response = await ConsumeRoute.CallRoute(ClientSettings.UrlBaseTransacBankApi+"Transaction/GenericTransactionCall", objCall, "POST", null);
        await HandleResponseService(response, dispatch);

        let json = await response.clone().json();
        if (response.status == 200 && json.response === true && json.error === "") {
            return json.data;
        }

        return null;
    }catch(err){
        dispatch(AlertActions.showDanger("An error occurred while fetching the data: "+err));
        return null;
    }
}

const HandleResponseServiceFetch=async(response)=>{
    if(response.status==401)
        throw new Error("Service denied - Credentials");

    if(response.status==404)
        throw new Error("Service, URL not found");
    
    let json = await response.clone().json()
    if(response.status==400)
        throw new Error("Service denied - Bad request: "+JSON.stringify(json));

    if(json.successful !== true || json.message!=="")
        throw new Error(json.message+json.error);

    if(json.error!=="")
        throw new Error("Error response in Service - "+ json.error);

    return true
}

const HandleResponseService=async(response)=>{
    if(response.status==401)
        throw new Error("Service denied - Credentials");

    if(response.status==404)
        throw new Error("Service, URL not found");
    
    let json = await response.clone().json()
    if(response.status==400)
        throw new Error("Service denied - Bad request: "+JSON.stringify(json));

    if(json.successful !== true || json.message!=="")
        throw new Error(json.message+json.error);

    if(json.error!=="")
        throw new Error("Error response in Service - "+ json.error);

    return true
}

export const ProductService={
    GetListProductsClient,
    GetListTransactionsByParams,
    CallWithdrawalTransaction,
    CallDepositTransaction,
    CallFundsTransferTransaction
};