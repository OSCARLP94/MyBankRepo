import React, { useEffect } from "react";
import { Typography, Card, CardContent, CardActions } from "@mui/material";

export default function ResumTransaction({ respTransac }) {
  useEffect(() => {}, [respTransac]);

  return (
    <div>
      <Card sx={{ width: "100%" }}>
        <CardContent>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            Id Transacción: {respTransac !== null ? respTransac.id : ""}
          </Typography>
          <Typography sx={{ fontSize: 16 }} color="text.secondary" gutterBottom>
            Tipo transacción:{" "}
            {respTransac !== null ? respTransac.typeTransaction : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            #Producto origen:{" "}
            {respTransac !== null ? respTransac.originProductNumber : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            #Producto destino:{" "}
            {respTransac !== null ? respTransac.destinyProductNumber : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            Fecha registro:{" "}
            {respTransac !== null ? respTransac.creationDate : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            Fecha efectiva:{" "}
            {respTransac !== null ? respTransac.effectDate : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            Valor solicitado:{" "}
            {respTransac !== null ? respTransac.explicitValue : ""}
          </Typography>
          <Typography sx={{ mb: 1.5 }} color="text.secondary">
            Detalle: {respTransac !== null ? respTransac.additional : ""}
          </Typography>
          <Typography variant="h5" color="text.primary">
            Estado: {respTransac !== null ? respTransac.state : ""}
          </Typography>
        </CardContent>
      </Card>
    </div>
  );
}
