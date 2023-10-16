import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Alert from "@mui/material/Alert";
import IconButton from '@mui/material/IconButton';
import { AlertActions } from "../Redux/Actions/alert-actions";
import CloseIcon from '@mui/icons-material/Close';

const AlertComp = (visible = true, width = "30%") => {
  //#region Variables
  const alert = useSelector((state) => state.alert.alert);
  const dispatch = useDispatch();
  //#endregion

  //#region Methods.
  useEffect(() => {
    ShowAlert();
  }, [visible, alert, dispatch]);

  function ShowAlert() {
    if (visible && alert !== undefined && Object.keys(alert).length) {
      setTimeout(Clear, alert.type === "error" ? 60000 : 6000);
    }
  }

  function Clear() {
    dispatch(AlertActions.clear());
  }
  //#endregion

  return (
    <>
      {alert ? (
        <Alert
          style={{
            position: "absolute",
            width: width,                     
            height: "7%",
            maxHeight: "15%",
            bottom: "20px",
            top: "initial",
            right: "20px",
            left: "initial",
          }}
          action={
            <IconButton
              aria-label="close"
              color="inherit"
              size="small"
              onClick={() => {
                Clear();
              }}
            >
              <CloseIcon fontSize="inherit" />
            </IconButton>
          }

          severity={alert.type}
        >
          {alert.message}
        </Alert>
      ) : null}
    </>
  );
};

export default AlertComp;
