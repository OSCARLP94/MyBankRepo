import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { Box } from "@mui/material";
import { useNavigate } from "react-router-dom";
import RequestQuoteOutlinedIcon from "@mui/icons-material/RequestQuoteOutlined";
import SendOutlinedIcon from "@mui/icons-material/SendOutlined";
import GetAppOutlinedIcon from "@mui/icons-material/GetAppOutlined";
import InputOutlinedIcon from "@mui/icons-material/InputOutlined";
import CardMenuOption from "../../Components/CardMenuOption";
import { AlertActions } from "../../Redux/Actions/alert-actions";
import AlertComp from "../../Components/AlertComp";

export default function Home() {
  let navigation = useNavigate();
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(AlertActions.clear());
    dispatch(AlertActions.showWarning("Por fines practicos se utiliza el usuario oscalp por defecto"));
  }, []);

  return (
    <>
      <AlertComp />
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
          gap: "2%",
        }}
      >
        <Box
          sx={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-around",
            width: "50%",
            marginTop: "5%",
          }}
        >
          <CardMenuOption
            icon={<RequestQuoteOutlinedIcon sx={{ fontSize: 40 }} />}
            label="Saldos y movimientos"
            actionClick={() => navigation("/balancefunds")}
          />
          <CardMenuOption
            icon={<GetAppOutlinedIcon sx={{ fontSize: 40 }} />}
            label="Retiro saldo"
            actionClick={() => navigation("/withdrawal")}
          />
        </Box>
        <Box
          sx={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-around",
            width: "50%",
            marginTop: "5%",
          }}
        >
          <CardMenuOption
            icon={<InputOutlinedIcon sx={{ fontSize: 40 }} />}
            label="Depositar saldo"
            actionClick={() => navigation("/deposit")}
          />
          <CardMenuOption
            icon={<SendOutlinedIcon sx={{ fontSize: 40 }} />}
            label="Transferir saldo"
            actionClick={() => navigation("/fundstransfer")}
          />
        </Box>
      </Box>
    </>
  );
}
