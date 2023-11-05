import React, { useState, useEffect } from "react";
import { ProductService } from "../Services/product-service";
import { useDispatch, useSelector } from "react-redux";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { Box, Button } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import ManageSearchOutlinedIcon from "@mui/icons-material/ManageSearchOutlined";
import { ConstValues } from "../clientsettings";
import { v4 as uuidv4 } from "uuid";
import LinearProgress from "@mui/material/LinearProgress";
import useSWR from "swr";

function TableTransactions({ clientUserName, productNumber }) {
  const [dateFrom, setDateFrom] = useState(dayjs().startOf("month"));
  const [dateUntil, setDateUntil] = useState(dayjs().endOf("month"));
  const [maxCount, setMaxCount] = useState(10);

  function generateKey(clientUserName, productNumber, maxCount) {
    return `${clientUserName}-${productNumber}-${maxCount}`;
  }

  const key = generateKey(clientUserName, productNumber, maxCount);

  const {
    data = [],
    isLoading,
    isValidating,
    error,
    mutate,
  } = useSWR(
    [key],
    () =>
      ProductService.GetListTransactionsByParams(
        clientUserName,
        productNumber,
        maxCount,
        dateFrom,
        dateUntil
      ),
    {
      revalidateOnFocus: false, // Configuración para evitar el re-fetch al enfocar la ventana
      revalidateOnReconnect: false, //para evitar que al cambiar parametros haga re-fetch
    }
  );

  const handleSearchTransacs = async () => {
    await mutate();
  };

  return (
    <>
      {(isLoading || isValidating) && <LinearProgress />}

      {error && !isLoading && !isValidating && (
        <div>Failed to load: {error.message}</div>
      )}

      {!isLoading && !isValidating && !error && (
        <>
          <Box sx={{ display: "flex", gap: 5 }}>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DatePicker
                label="Fecha desde"
                format="DD/MM/YYYY"
                value={dateFrom}
                onChange={setDateFrom}
              />
              <DatePicker
                label="Fecha hasta"
                format="DD/MM/YYYY"
                value={dateUntil}
                onChange={setDateUntil}
              />
              <Button
                variant="outlined"
                startIcon={<ManageSearchOutlinedIcon />}
                onClick={async () => handleSearchTransacs()}
              >
                Buscar
              </Button>
            </LocalizationProvider>
          </Box>
          <Box>
            <TableContainer component={Paper}>
              <Table
                sx={{ minWidth: 650 }}
                size="small"
                aria-label="a dense table"
              >
                <TableHead>
                  <TableRow>
                    <TableCell>
                      <b>Tipo transacción</b>
                    </TableCell>
                    <TableCell>
                      <b>Fecha</b>
                    </TableCell>
                    <TableCell>
                      <b>Valor</b>
                    </TableCell>
                    <TableCell>
                      <b>Producto origen</b>
                    </TableCell>
                    <TableCell>
                      <b>Producto destino</b>
                    </TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data &&
                    data.length > 0 &&
                    data.map((row) => (
                      <TableRow
                        key={uuidv4()}
                        sx={{
                          "&:last-child td, &:last-child th": { border: 0 },
                        }}
                      >
                        <TableCell component="th" scope="row">
                          {row.typeTransaction.name}
                        </TableCell>
                        <TableCell>{row.creationDate}</TableCell>
                        <TableCell>{row.explicitValue}</TableCell>
                        <TableCell>{row.originProductNumber}</TableCell>
                        <TableCell>{row.destinyProductNumber}</TableCell>
                      </TableRow>
                    ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Box>
        </>
      )}
    </>
  );
}

export default TableTransactions;
