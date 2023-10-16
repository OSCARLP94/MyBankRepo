import React, { useEffect, useState, lazy, Suspense } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ProductService } from "../../Services/product-service";
import { ConstValues } from "../../clientsettings";
import { AlertActions } from "../../Redux/Actions/alert-actions";
import AlertComp from "../../Components/AlertComp";
import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import TextField from "@mui/material/TextField";
import { Divider, Button } from "@mui/material";
import CircularProgress from "@mui/material/CircularProgress";
//lazyload
const ResumTransaction = lazy(() =>
  import("../../Components/ResumTransaction")
);

export default function Deposit() {
  const dispatch = useDispatch();
  const products = useSelector((state) => state.clientProducts.products);

  const [itemSelected, setItemSelected] = useState("");
  const [productSelected, setProductSelected] = useState(null);
  const [value, setValue] = useState(0);
  const [additional, setAdditional] = useState("");
  const [respTransac, setRespTransac] = useState(null);
  const [successTransac, setSuccessTransac] = useState(false);
  const [callingTransac, setCallingTransac] = useState(false);

  useEffect(() => {
    (async function () {
      dispatch(AlertActions.clear());
      if (products === undefined || products.length <= 0)
        await ProductService.GetListProductsClient(
          dispatch,
          ConstValues.DefaultUserTest
        );
    })();
  }, [dispatch]);

  useEffect(() => {
    // set first product whenever products change
    if (products !== undefined && products.length > 0) {
      setItemSelected(products[0].productNumber);
      setProductSelected(products[0]);
    }
  }, [products]);

  const handleChangeSelectedProd = (event) => {
    setItemSelected(event.target.value);
    if (products !== undefined && products.length > 0)
      setProductSelected(
        products.find((value, index) => {
          return value.productNumber == event.target.value;
        })
      );
    //setShowTransacSection(false);
  };

  const handleCallDeposit = async () => {
    setCallingTransac(true);
    var resp = await ProductService.CallDepositTransaction(
      dispatch,
      ConstValues.DefaultUserTest,
      productSelected.productNumber,
      new Date(),
      value,
      "",
      additional
    );
    setCallingTransac(false);
    if (resp !== null) {
      setSuccessTransac(true);
      setRespTransac(resp);
    }
  };

  return (
    <Box
    sx={{
      display: "flex",
      flexDirection: "column",
      alignItems: "center",
      justifyContent: "center",
      gap: "2%",
    }}
  >
    {callingTransac ===false ? (
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "space-around",
          width: "50%",
          marginTop: "5%",
        }}
      >
        <FormControl sx={{ margin: 1 }} fullWidth>
          <InputLabel id="demo-simple-select-label">Producto</InputLabel>
          <Select
            disabled={successTransac}
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={itemSelected}
            label="Producto"
            onChange={handleChangeSelectedProd}
          >
            {products !== undefined
              ? products.map((item) => {
                  return (
                    <MenuItem
                      key={item.productNumber}
                      value={item.productNumber}
                    >
                      {item.typeProduct.name}({item.productNumber})
                    </MenuItem>
                  );
                })
              : null}
          </Select>
        </FormControl>
        <TextField
          sx={{ margin: 1 }}
          required
          disabled={successTransac}
          type="number"
          id="outlined-required"
          label="Valor depósito"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
        <TextField
          sx={{ margin: 1 }}
          id="outlined-multiline-flexible"
          label="Detalle(opcional)"
          disabled={successTransac}
          multiline
          value={additional}
          maxRows={4}
          onChange={(e) => setAdditional(e.target.value)}
        />
        <Button
          variant="outlined"
          disabled={successTransac}
          onClick={async () => handleCallDeposit()}
        >
          Depositar
        </Button>
      </Box>
    ) : <CircularProgress/>}

    <Divider sx={{ margin: 1 }} variant="middle" />
    {successTransac ? (
      <Suspense fallback={<CircularProgress />}>
        <Box sx={{ width: "50%" }}>
          <ResumTransaction respTransac={respTransac} />
        </Box>
      </Suspense>
    ) : null}
    <AlertComp />
  </Box>
  )
}
