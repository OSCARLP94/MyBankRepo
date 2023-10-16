import { combineReducers } from "redux";
import { ProductReducer} from "./product-reducer";
import {AlertReducer} from "./alert-reducer";

const rootReducer = combineReducers({
    clientProducts : ProductReducer,
    alert: AlertReducer
});

export default rootReducer;