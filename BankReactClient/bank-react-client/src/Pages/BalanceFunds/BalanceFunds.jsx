import React, { useState, useEffect, lazy, Suspense } from "react";
import { useDispatch, useSelector } from "react-redux";
import { ProductService } from "../../Services/product-service";
import { AlertActions } from "../../Redux/Actions/alert-actions";
import AlertComp from "../../Components/AlertComp";
import { Divider } from "@mui/material";
import Box from "@mui/material/Box";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import CircularProgress from "@mui/material/CircularProgress";
import { ConstValues } from "../../clientsettings";
import LinearProgress from "@mui/material/LinearProgress";

//lazyload
const TableTransactions = lazy(() =>
  import("../../Components/TableTransactions")
);

export default function BalanceFunds() {
  const dispatch = useDispatch();
  const products = useSelector((state) => state.clientProducts.products);

  const [itemSelected, setItemSelected] = useState("");
  const [productSelected, setProductSelected] = useState(null);
  const [showTransacSection, setShowTransacSection] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    (async function () {
      setIsLoading(true);

      dispatch(AlertActions.clear());
      //cargar productos en redux
      await ProductService.GetListProductsClient(
        dispatch,
        ConstValues.DefaultUserTest
      );

      setIsLoading(false);
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
  };

  return (
    <>
      {isLoading && <LinearProgress />}
      {!isLoading && (
        <>
          <Box sx={{ marginTop: "2%", minWidth: 120 }}>
            <FormControl fullWidth>
              <InputLabel id="demo-simple-select-label">Producto</InputLabel>
              <Select
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
          </Box>
          <Divider variant="middle" />
          <Box
            sx={{
              width: "100%", // Cambiado de 50% a 100%
              display: "flex",
              flexDirection: "row",
              alignItems: "center",
              justifyContent: "center",
              gap: "2%",
            }}
          >
            <Card sx={{ width: "100%" }}>
              <CardContent>
                <Typography
                  sx={{ fontSize: 16 }}
                  color="text.secondary"
                  gutterBottom
                >
                  Tipo producto:{" "}
                  {productSelected !== null
                    ? productSelected.typeProduct.name
                    : ""}
                </Typography>
                <Typography variant="h4" component="div">
                  Saldo actual: ${" "}
                  {productSelected !== null
                    ? productSelected.moneyAccount.currentBalance
                    : ""}
                </Typography>
                <Typography sx={{ mb: 1.5 }} color="text.secondary">
                  # Cuenta:{" "}
                  {productSelected !== null
                    ? productSelected.productNumber
                    : ""}
                </Typography>
              </CardContent>
              <CardActions>
                {productSelected && <Button
                  size="small"
                  onClick={() => setShowTransacSection(true)}
                >
                  Transacciones
                </Button>}
              </CardActions>
            </Card>
          </Box>
          <Divider variant="middle" />
          {showTransacSection ? (
            <Suspense fallback={<CircularProgress />}>
              <Box sx={{ marginTop: "2%", minWidth: 120 }}>
                <TableTransactions
                  clientUserName={ConstValues.DefaultUserTest}
                  productNumber={productSelected.productNumber}
                />
              </Box>
            </Suspense>
          ) : null}
        </>
      )}

      <AlertComp />
    </>
  );
}
