import { Provider } from "react-redux";
import { createStore } from "redux";
import rootReducer from "./Redux/Reducers/reducers";
import { Routes, Route } from "react-router-dom";
import Home from "./Pages/Home/Home";
import NotFound from "./Pages/NotFound";
import BalanceFunds from "./Pages/BalanceFunds/BalanceFunds";
import Deposit from "./Pages/Deposit/Deposit";
import Withdrawal from "./Pages/Withdrawal/Withdrawal";
import FundsTransfer from "./Pages/FundsTransfer/FundsTransfer";

const store = createStore(rootReducer);

function App() {
  return (
    <>
      <Provider store={store}>
        <Routes>
          <Route exact path={"/"} element={<Home />} />
          <Route exact path={"/deposit"} element={<Deposit />} />
          <Route exact path={"/balancefunds"} element={<BalanceFunds />} />
          <Route exact path={"/withdrawal"} element={<Withdrawal />} />
          <Route exact path={"/fundstransfer"} element={<FundsTransfer />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Provider>
    </>
  );
}

export default App;
