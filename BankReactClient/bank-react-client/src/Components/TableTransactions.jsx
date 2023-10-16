import React, { useState, useEffect } from "react";
import {ProductService} from "../Services/product-service";
import { useDispatch, useSelector } from "react-redux";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { Box,Button } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from 'dayjs';
import ManageSearchOutlinedIcon from "@mui/icons-material/ManageSearchOutlined";
import { ConstValues } from "../clientsettings";
import { v4 as uuidv4 } from 'uuid';

function createData(name, calories, fat, carbs, protein) {
  return { name, calories, fat, carbs, protein };
}

const rows = [
  createData("Frozen yoghurt", 159, 6.0, 24, 4.0),
  createData("Ice cream sandwich", 237, 9.0, 37, 4.3),
  createData("Eclair", 262, 16.0, 24, 6.0),
  createData("Cupcake", 305, 3.7, 67, 4.3),
  createData("Gingerbread", 356, 16.0, 49, 3.9),
];

function TableTransactions({ clientUserName, productNumber }) {
  const dispatch = useDispatch();

  const [dateFrom, setDateFrom] = useState(dayjs().startOf('month'));
  const [dateUntil, setDateUntil] = useState(dayjs().endOf('month'));
  const [rowsTransactions, setRowsTransactions] =useState([]);

  useEffect(()=>{
    (async()=>{
      await handleSearchTransacs();
    })();
  },[clientUserName, productNumber]);

  const handleSearchTransacs=async()=>{
    var resp = await ProductService.GetListTransactionsByParams(dispatch, clientUserName, productNumber, 10, 
      dateFrom, dateUntil, [ConstValues.CodeDepositTransac, ConstValues.CodeFundsTransfTransac, ConstValues.CodeWithdrawTransac]);
    
      if(resp!==null)
      {setRowsTransactions(resp);}
  }

  return (
    <>
      <Box sx={{ display: "flex", gap: 5}}>
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
          <Button  variant="outlined" startIcon={<ManageSearchOutlinedIcon />}>
            Buscar
          </Button>
        </LocalizationProvider>
      </Box>
      <Box>
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
            <TableHead>
              <TableRow>
                <TableCell>
                  <b>Tipo transacción</b>
                </TableCell>
                <TableCell >
                  <b>Fecha</b>
                </TableCell>
                <TableCell >
                  <b>Valor</b>
                </TableCell>
                <TableCell >
                  <b>Producto origen</b>
                </TableCell>
                <TableCell >
                  <b>Producto destino</b>
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rowsTransactions.map((row) => (
                <TableRow
                  key={uuidv4()}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell component="th" scope="row">
                    {row.typeTransaction.name}
                  </TableCell>
                  <TableCell >{row.creationDate}</TableCell>
                  <TableCell >{row.explicitValue}</TableCell>
                  <TableCell >{row.originProductNumber}</TableCell>
                  <TableCell >{row.destinyProductNumber}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Box>
    </>
  );
}

export default TableTransactions;
