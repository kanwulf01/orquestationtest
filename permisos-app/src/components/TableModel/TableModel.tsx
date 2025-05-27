import * as React from 'react';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Button, CircularProgress, FormControl, InputLabel,TextField} from '@mui/material';
import type { PropsTableModel } from '../../Models/Models';
import MuiSelect from '../SelectModel/SelectModel';
import type { PermisosDtoPost } from '../../Models/Response';
import DatePickerModel from '../DatePicker/DatePicker';


function createData2(
  Id: number,
  nombreEmpleado: string,
  apellidoEmpleado: string,
  tipoPermiso: {
    descripcion: string,
    id: number
  },
  fechaPermiso: string,
  history:[]
) {
  return {
    Id,
    nombreEmpleado,
    apellidoEmpleado,
    tipoPermiso,
    fechaPermiso,
    history
  };
}


function Row(props: { 
  row: ReturnType<typeof createData2>, 
  listTipoPermisos?: Array<{value: number | string, label: string}>,
  UpdatePermisos: (permiso: PermisosDtoPost) => Promise<PermisosDtoPost>;
  // function:React.Node
}) {
  
  const { row, listTipoPermisos, UpdatePermisos } = props;
  const [open, setOpen] = React.useState(false);

  const [nombreEmpleado, setNombreEmpleado] = React.useState<string>('');
  const [apellidoEmpleado, setApellidoEmpleado] = React.useState<string>('');
  const [tipoPermiso, setTipoPermiso] = React.useState<number>(0);
  const [tipoPermisoV, setTipoPermisoV] = React.useState<string>("");
  const [fechaPermiso, setFechaPermiso] = React.useState<string>('');
  const [loading, setLoading] = React.useState<boolean>(false);

  React.useEffect(() => {
    setNombreEmpleado(row.nombreEmpleado);
    setApellidoEmpleado(row.apellidoEmpleado);
    setTipoPermiso(row.tipoPermiso.id);
    setFechaPermiso(row.fechaPermiso);
    setTipoPermisoV(row.tipoPermiso.id.toString());
    // console.log('useEffect row', row);
  }, [row]);


  const findTipoPermisoLabel = (id: number | string) => {
    const tipoPermisoItem = listTipoPermisos?.find(item => item.value === id);
    // console.log('findTipoPermisoLabel', tipoPermisoItem, id);
    return tipoPermisoItem ? tipoPermisoItem.label : '';
  };

  const SendUpdatePermisos = async () => {
    if(!nombreEmpleado || !apellidoEmpleado || !tipoPermiso || !fechaPermiso) {
      alert('Todos los campos son obligatorios');
      return;
    }
    const dataPost:PermisosDtoPost = {
      id: row.Id,
      nombreEmpleado: nombreEmpleado,
      apellidoEmpleado: apellidoEmpleado,
      tipoPermisoId: tipoPermiso,
      fechaPermiso: fechaPermiso,
      tipoPermiso: 0, // Este campo puede ser utilizado para indicar el tipo de permiso si es necesario
      tipoPermisoLabel: findTipoPermisoLabel(tipoPermisoV)
    };

    for(const i in dataPost) {
      if(dataPost[i as keyof PermisosDtoPost] === '' || dataPost[i as keyof PermisosDtoPost] === null) {
        console.error(`El campo ${i} es obligatorio`);
        alert(`El campo Tipo de Permiso es obligatorio seleccionarlo`);
        return;
      }
    }
    setLoading(true);
    const sendUpdateData = await UpdatePermisos(dataPost);

    if(sendUpdateData) {
      setLoading(false);
    }else {
      setLoading(false);
    }

  }

  const onChangeFechaPermiso = (value: any) => {
    // console.log('onChangeFechaPermiso', value);
    if(value && value.$d) {
      const date = new Date(value.$d);
      // const formattedDate = date.toLocaleString('es-CO', {
      //   timeZone: 'America/Bogota',
      //   year: 'numeric',
      //   month: '2-digit',
      //   day: '2-digit',
      //   hour: '2-digit',
      //   minute: '2-digit',
      //   second: '2-digit',
      //   hour12: false
      // }).replace(',', '');
      const formattedDate = date.toISOString();
      setFechaPermiso(formattedDate);
      console.log("Fecha formateada:", formattedDate);
    } else {
      console.error('Invalid date value', value);
    }
  }

  return (
    <React.Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.nombreEmpleado}
        </TableCell>
        <TableCell align="left">{row.apellidoEmpleado}</TableCell>
        <TableCell align="left">{row.tipoPermiso.descripcion}</TableCell>
        <TableCell align="left">{row.fechaPermiso}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Modificar Permiso
              </Typography>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell>
                      <TextField  
                      id="nombreEmpleado" 
                      label="NombreEmpleado" 
                      variant="outlined" 
                      value={nombreEmpleado} 
                      onChange={(e)=>setNombreEmpleado(e.target.value)} 
                      />
                    </TableCell>
                    <TableCell>
                      <TextField 
                      id="apellidoEmpleado" 
                      label="ApellidoEmpleado" 
                      variant="outlined" 
                      value={apellidoEmpleado} 
                      onChange={(e)=>setApellidoEmpleado(e.target.value)} />
                    </TableCell>
                    
                    <TableCell align="right">
                        <FormControl fullWidth required>
                        <InputLabel id="tipo-label">Tipo de Permiso</InputLabel>
                          <MuiSelect
                            label="Tipo Permiso"
                            options={listTipoPermisos?.map((item) => ({
                              value: item.value,
                              label: item.label,
                            })) || []}
                            value={tipoPermisoV}
                            onChange={(value) => {
                              console.log('onChange tipoPermiso', value);
                              setTipoPermiso(parseInt(value as string, 10));
                              setTipoPermisoV(value as string);
                            }}
                     >
                        </MuiSelect>
                        </FormControl>
                    </TableCell>
                    <TableCell align="right">
                      <FormControl fullWidth required>
                      {/* <InputLabel id="tipo-label">Tipo de Permiso</InputLabel> */}
                      {<DatePickerModel onChangeFechaPermiso={onChangeFechaPermiso} label={"Fecha de Permiso"}/>}
                      </FormControl>
                    </TableCell>
                    {loading?<>
                      <TableCell align="right">
                        <CircularProgress />
                      </TableCell>
                    </>:<>
                      <TableCell align="right"><Button onClick={()=>SendUpdatePermisos()}>Modificar</Button></TableCell>
                    </>}
                  </TableRow>
                </TableHead>
                
                <TableBody>
                  
                </TableBody>
                <>
                </>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}




export default function TableModel({columns, listPermisos, listTipoPermisos, UpdatePermisos}:PropsTableModel) {

    let copyListPermisos = listPermisos.map((item) => {
      let descripcionTipoPermiso = "";
      let tipoPermisoId = 0;
      if(item.tipoPermiso && item.tipoPermiso) {
      descripcionTipoPermiso = item.tipoPermiso.descripcion;
      tipoPermisoId = item.tipoPermiso.id;
      } 
      return createData2(item.id, item.nombreEmpleado, item.apellidoEmpleado, {descripcion:descripcionTipoPermiso, id:tipoPermisoId  }, item.fechaPermiso, []);
    });

  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow>
            <TableCell />
            {/* <TableCell>Dessert (100g serving)</TableCell>
            <TableCell align="right">Calories</TableCell>
            <TableCell align="right">Fat&nbsp;(g)</TableCell>
            <TableCell align="right">Carbs&nbsp;(g)</TableCell>
            <TableCell align="right">Protein&nbsp;(g)</TableCell> */}
            {columns.map((column) => (
                <TableCell  key={column} align="left">
                    {column}
                </TableCell>
            ))}
          </TableRow>
        </TableHead>
        <TableBody>
          {copyListPermisos.map((row) => (
            <Row key={row.nombreEmpleado} 
              row={row} 
              listTipoPermisos={listTipoPermisos}
              UpdatePermisos={UpdatePermisos}
            />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
