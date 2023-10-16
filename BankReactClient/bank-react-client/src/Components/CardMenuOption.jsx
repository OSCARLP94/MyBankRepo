import React, {useState} from 'react'
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import { Typography } from '@mui/material';

export default function CardMenuOption({icon, label, actionClick}) {
    const [isHovered, setIsHovered] = useState(false);

    const handleMouseEnter = () => {
      setIsHovered(true);
    };
  
    const handleMouseLeave = () => {
      setIsHovered(false);
    };

    const handleClick = ()=>{
        actionClick();
    }

  return (
    <>
     <Card sx={{ minWidth: 200, width: "20%", height:'100%', cursor: 'pointer',
            backgroundColor: isHovered ? '#f0f0f0' : 'white' }}
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
            onClick={handleClick}>
          <CardActions sx={{ justifyContent: "center" }}>
           {icon}
          </CardActions>
          <CardContent
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Typography sx={{ fontSize: 20 }} component="div">
             {label}
            </Typography>
          </CardContent>
    </Card>
    </>
  )
}
