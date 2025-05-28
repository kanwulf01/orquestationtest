import React, { useState } from 'react';
import {
  Box,
  Button,
  Card,
  CardContent,
  CircularProgress,
  FormControl,
  Grid,
  InputLabel,
  TextField,
  Typography
} from '@mui/material';

import DatePickerModel from '../DatePicker/DatePicker';
import type { PropsForm } from '../../Models/Models';
import MuiSelect from '../SelectModel/SelectModel';
import { PostPermiso } from '../../api/api';
import type { PermisosDtoPost } from '../../Models/Response';


interface PermisoData {
  nombre: string;
  apellido: string;
  tipo: string;
  fecha: Date | null;
}



const PermisoForm = ({listSelectItems}: PropsForm) => {
  const [tipoPermiso, setTipoPermiso] = React.useState<number>(0);
  const [tipoPermisoV, setTipoPermisoV] = React.useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const [data, setData] = useState<PermisoData>({
    nombre: '',
    apellido: '',
    tipo: '',
    fecha: null
  });

  const handleChange = (field: keyof PermisoData) => (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement> | any
  ) => {
    setData({ ...data, [field]: event.target.value });
  };

  const handleDateChange = (date: Date | null) => {
    setData({ ...data, fecha: date });
  };

  const handleSubmit = async(e: React.FormEvent) => {
    e.preventDefault();
    console.log('Guardar permiso:', data);

    
    const dataPsot:PermisosDtoPost = {
      id: 0, 
      nombreEmpleado: data.nombre,
      apellidoEmpleado: data.apellido,
      tipoPermisoId: tipoPermiso,
      fechaPermiso: data.fecha ? data.fecha.toISOString() : '',
      tipoPermiso: 0
    };

    for(const i in dataPsot) {
      if(dataPsot[i as keyof PermisosDtoPost] === '' || dataPsot[i as keyof PermisosDtoPost] === null) {
        console.error(`El campo ${i} es obligatorio`);
        alert(`El campo ${i} es obligatorio`);
        return;
      }
    }
    setLoading(true);
    const response = await PostPermiso(dataPsot);
    if(response) {
      setLoading(false)
    }else {
      setLoading(false)
    }
  };

  return (
    <Card sx={{ maxWidth: 800, mx: 'auto', mt: 4, boxShadow: 3, borderRadius: 2 }}>
      <CardContent>
        <Typography variant="h5" component="h2" gutterBottom>
          Registrar Permiso
        </Typography>
        <Box component="form" onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid >
              <TextField
                fullWidth
                label="Nombre Empleado"
                value={data.nombre}
                onChange={handleChange('nombre')}
                required
              />
            </Grid>
            <Grid>
              <TextField
                fullWidth
                label="Apellido Empleado"
                value={data.apellido}
                onChange={handleChange('apellido')}
                required
              />
            </Grid>
            <Grid>
              <FormControl fullWidth required>
                <InputLabel id="tipo-label">Tipo de Permiso</InputLabel>
                <MuiSelect
                    label="Tipo Permiso"
                    options={listSelectItems?.map((item) => ({
                      value: item.value,
                      label: item.label,
                    })) || []}
                    value={tipoPermisoV}
                    onChange={(value) => {
                      console.log('onChange tipoPermiso', value);
                      setTipoPermiso(parseInt(value as string, 10));
                      setTipoPermisoV(value as string);
                    }}>
                </MuiSelect>
              </FormControl>
            </Grid>
            <Grid >
              {<DatePickerModel onChangeFechaPermiso={handleDateChange} label={"Fecha de Permiso"}/>}
            </Grid>
            {loading?<>
              <CircularProgress />
            </>:
            <>
              <Grid >
              <Button
                type="submit"
                variant="contained"
                fullWidth
                sx={{ mt: 2 }}
              >
                Guardar Permiso
              </Button>
            </Grid>
            </>}
          </Grid>
        </Box>
      </CardContent>
    </Card>
  );
};

export default PermisoForm;
