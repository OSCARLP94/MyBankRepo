import React from "react";
import { Typography } from "@mui/material";
import SentimentVeryDissatisfiedIcon from '@mui/icons-material/SentimentVeryDissatisfied';

export default function NotFound() {
  return (
    <Container
      style={{
        justifyContent: "center",
        alignItems: "center",
        marginTop: "15%",
      }}
    >
      <Typography variant="h1" style={{ fontSize: "80px" }}>
        404
      </Typography>
      <SentimentVeryDissatisfiedIcon sx={{ fontSize: 60 }} />
      <Typography variant="h2" component="h2">
        Error! Page not found ...
      </Typography>
      <p>Sorry, the page not exists or is not available in this moment.</p>
      <p>Update for try again or return...</p>
    </Container>
  );
}
