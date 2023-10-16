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
        dispatch(AlertActions.showDanger("Error connecting or timeout service: "+err));
    }
}

async function GetListTransactionsByParams(dispatch, userName, productNumber, maxCount=10, fromDate, untilDate, listTypeTransac){
    try{
        var objSearch = {clientUserName: userName, productNumber: productNumber, maxCount:maxCount, fromDate:fromDate, untilDate:untilDate, typeTransactions:listTypeTransac};

        var response = await ConsumeRoute.CallRoute(ClientSettings.UrlBaseInfoBankApi+"ProductInfo/ListTransactionsClient", objSearch, "POST", null);
        await HandleResponseService(response, dispatch);

        let json = await response.clone().json();
        if (response.status == 200 && json.response === true && json.error === "") {
            return json.data;
        }

        return null;
    }catch(err){
        dispatch(AlertActions.showDanger("Error connecting or timeout service: "+err));
        return null;
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
        dispatch(AlertActions.showDanger("Error connecting or timeout service: "+err));
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
        dispatch(AlertActions.showDanger("Error connecting or timeout service: "+err));
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
        dispatch(AlertActions.showDanger("Error connecting or timeout service: "+err));
        return null;
    }
}

const HandleResponseService=async(response, dispatch)=>{
    if(response.status==401)
    {
        dispatch(AlertActions.showDanger("Service denied - Credentials"))
        return false
    }

    if(response.status==404){
        dispatch(AlertActions.showDanger("Service, URL not found"))
        return false
    }

    let json = await response.clone().json()
    if(response.status==400){
        dispatch(AlertActions.showDanger("Service denied - Bad request: "+JSON.stringify(json)))
        return false
    }

    if(json.successful !== true || json.message!==""){
        dispatch(AlertActions.showDanger(json.message+json.error))
        return false
    } 

    if(json.error!==""){
        dispatch(AlertActions.showDanger("Error response in Service - "+ json.error))
        return false
    }
    return true
}

export const ProductService={
    GetListProductsClient,
    GetListTransactionsByParams,
    CallWithdrawalTransaction,
    CallDepositTransaction,
    CallFundsTransferTransaction
};