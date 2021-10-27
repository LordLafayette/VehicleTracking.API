import { useEffect, useState } from 'react';
// formik
import { useFormik } from 'formik';
// materials
import { TextField, Card, CardContent, CardHeader, Paper, Typography, Skeleton, Stack, Box, Grid } from '@mui/material';
// utils
import axios from '../utils/axios';

// ? ----------------------------- Main Component --------------------------------

export default function InfoPage() {
  return (
    <Stack spacing={2}>
      <DriverInfo />
      <VehicleList />
    </Stack>
  );
}

// ? ------------------------------- Components ----------------------------------

function DriverInfo() {
  // * ---------------------------------- State ------------------------------------
  const [driverInfo, setDriverInfo] = useState(null);
  const [ready, setReady] = useState(false);

  // * ------------------------ Handler Fetch Driver Info --------------------------

  useEffect(() => {
    async function fetch() {
      try {
        const resp = await axios.get('/api/driver');
        setDriverInfo(resp.data);
      } finally {
        setReady(true);
      }
    }

    fetch();
  }, []);

  return (
    <Card>
      <CardHeader title="Driver Info" />
      <CardContent>{renderDriverInfo()}</CardContent>
    </Card>
  );

  function renderDriverInfo() {
    if (!ready) {
      return <LoadingSkeleton />;
    }

    return (
      <Stack spacing={2}>
        <TextField label="Name" value={driverInfo.name} fullWidth />
        <TextField label="Last Name" value={driverInfo.lastName} fullWidth />
        <TextField label="License Number" value={driverInfo.licenseNumber} fullWidth />
      </Stack>
    );
  }
}

function VehicleList() {
  const [vehicles, setVehicles] = useState([]);
  const [ready, setReady] = useState(false);
  // * ------------------------- Handler Fetch Vehicles ----------------------------
  useEffect(() => {
    async function fetch() {
      try {
        const resp = await axios.get('/api/driver/vehicle');
        setVehicles(resp.data || []);
      } finally {
        setReady(true);
      }
    }

    fetch();
  }, []);

  return (
    <Card>
      <CardHeader title="Vehicles" />
      <CardContent>{renderVehiclesList()}</CardContent>
    </Card>
  );

  function renderVehiclesList() {
    if (!ready) {
      return <LoadingSkeleton />;
    }

    return (
      <Stack Spacing={2}>
        {vehicles.map(({ vehicleType, licensePlate, lat, lng, lastLocationName, lastSpeed }, idx) => (
          <Grid key={idx} spacing={2} container>
            <Grid xs={12} sm={6} item>
              <Paper elevation={3} sx={{ p: 2 }}>
                <Typography variant="subtitle2" gutterBottom>
                  Plate Number {licensePlate}
                </Typography>
                <Box sx={{ pl: 2 }}>
                  <Typography variant="body2" gutterBottom>
                    Vehicle Type : {vehicleType}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Lat : {lat}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Lng : {lng}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Location : {lastLocationName || 'unknown'}
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    Speed : {lastSpeed} Km/Hr
                  </Typography>
                </Box>
              </Paper>
            </Grid>

            <Grid xs={12} sm={6} item></Grid>
          </Grid>
        ))}
      </Stack>
    );
  }
}

function VehicleFormUpdate() {}

function LoadingSkeleton() {
  return (
    <>
      <Skeleton height={50} />
      <Skeleton height={50} />
      <Skeleton height={50} />
      <Skeleton height={50} />
    </>
  );
}
