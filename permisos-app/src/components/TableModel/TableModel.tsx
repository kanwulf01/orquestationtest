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
import { Button, MenuItem, OutlinedInput, Select, Tab, TextField, useTheme, type SelectChangeEvent, type Theme } from '@mui/material';
import type { PropsTableModel } from '../../Models/Models';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';

function getStyles(name: string, personName: string[], theme: Theme) {
  return {
    fontWeight: personName.includes(name)
      ? theme.typography.fontWeightMedium
      : theme.typography.fontWeightRegular,
  };
}

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};



function createData2(
  Id: number,
  nombreEmpleado: string,
  apellidoEmpleado: string,
  tipoPermiso: number,
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



interface SelectItems {
    key: number;
    value: string;
}
const types:Array<SelectItems> = [
  {key:1, value: "Vacaciones"},
  {key:2, value: "Enfermedad"},
];



function Row(props: { 
  row: ReturnType<typeof createData2>, 
  // setNombreEmpleado?: (value: string) => void, 
  // setApellidoEmpleado?: (value: string) => void, 
  // setTipoPermiso?: (value: number) => void, 
  // setFechaPermiso?: (value: string) => void,
  // nombreEmpleado?: string,
  // apellidoEmpleado?: string,
  // tipoPermiso?: number,
  // fechaPermiso?: string
}) {
  
  const { row } = props;
  const [open, setOpen] = React.useState(false);

  const [nombreEmpleado, setNombreEmpleado] = React.useState<string>('');
  const [apellidoEmpleado, setApellidoEmpleado] = React.useState<string>('');
  const [tipoPermiso, setTipoPermiso] = React.useState<number>(0);
  const [fechaPermiso, setFechaPermiso] = React.useState<string>('');

  console.log('Row', row);
  // if(!setNombreEmpleado) {
  //   console.error('setNombreEmpleado is not defined');
  //   return null;
  // }

  const theme = useTheme();
  const [personName, setPersonName] = React.useState<string[]>([]);

  React.useEffect(() => {
    setNombreEmpleado(row.nombreEmpleado);
    setApellidoEmpleado(row.apellidoEmpleado);
    setTipoPermiso(row.tipoPermiso);
    setFechaPermiso(row.fechaPermiso);
  }, [row]);

  const handleChange = (event: SelectChangeEvent<typeof personName>) => {
    console.log('handleChange', event);
    const {
      target: { value },
    } = event;
    setPersonName(
      // On autofill we get a stringified value.
      typeof value === 'string' ? value.split(',') : value,
    );
  };

  const SendUpdatePermisos = () => {
    console.log('SendUpdatePermisos', row);
  }

  const onChangeFechaPermiso = (value: any) => {
    console.log('onChangeFechaPermiso', value);
    if(value && value.$d) {
      const date = new Date(value.$d);
      const formattedDate = date.toLocaleString('es-CO', {
        timeZone: 'America/Bogota',
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12: false
      }).replace(',', '');
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
        <TableCell align="left">{row.tipoPermiso}</TableCell>
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
                    <TableCell><TextField  id="nombreEmpleado" label="NombreEmpleado" variant="outlined" value={nombreEmpleado} onChange={(e)=>setNombreEmpleado(e.target.value)} /></TableCell>
                    <TableCell><TextField id="apellidoEmpleado" label="ApellidoEmpleado" variant="outlined" value={apellidoEmpleado} onChange={(e)=>setApellidoEmpleado(e.target.value)} /></TableCell>
                    
                    <TableCell align="right">
                        <Select
                            label="TipoPermiso"
                            labelId="demo-multiple-name-label"
                            id="demo-multiple-name"
                            value={personName}
                            onChange={handleChange}
                            input={<OutlinedInput label="Name" />}
                            MenuProps={MenuProps}
                            >
                            {types.map((name) => (
                                <MenuItem
                                key={name.key}
                                value={name.value}
                                style={getStyles(name.value, personName, theme)}
                                >
                                {name.value}
                                </MenuItem>
                            ))}
                            </Select>
                    </TableCell>
                    {/* <TableCell align="right"><TextField type='date' id="fechaPermiso" label="FechaPermiso" value={"2023-10-10 01:00:10"} /></TableCell> */}
                    <TableCell align="right">
                      <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <DemoContainer components={['DateTimePicker']}>
                          <DateTimePicker label="Basic date time picker" onChange={(e)=>onChangeFechaPermiso(e)}/>
                        </DemoContainer>
                      </LocalizationProvider>
                    </TableCell>
                    <TableCell align="right"><Button onClick={()=>SendUpdatePermisos()}>Modificar</Button></TableCell>
                  </TableRow>
                </TableHead>
                
                <TableBody>
                  {/* {row.history.map((historyRow) => (
                    <TableRow key={historyRow.date}>
                      <TableCell component="th" scope="row">
                        {historyRow.date}
                      </TableCell>
                      <TableCell>{historyRow.customerId}</TableCell>
                      <TableCell align="right">{historyRow.amount}</TableCell>
                      <TableCell align="right">
                        {Math.round(historyRow.amount * row.price * 100) / 100}
                      </TableCell>
                    </TableRow>
                  ))} */}
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




export default function TableModel({tittle, columns, listPermisos, listTipoPermisos}:PropsTableModel) {
    console.log('TableModel lista permisos', listPermisos);
    console.log('TableModel', tittle);
    console.log('TableModel', columns);
    console.log('TableModel', listTipoPermisos);

    

    let copyListPermisos = listPermisos.map((item) => {
        return createData2(item.id, item.nombreEmpleado, item.apellidoEmpleado, item.tipoPermiso, item.fechaPermiso, []);
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
              // setNombreEmpleado={setNombreEmpleado} 
              // setApellidoEmpleado={setApellidoEmpleado} 
              // setTipoPermiso={setTipoPermiso} 
              // setFechaPermiso={setFechaPermiso} 
              // nombreEmpleado={nombreEmpleado}
              // apellidoEmpleado={apellidoEmpleado}
              // tipoPermiso={tipoPermiso}
              // fechaPermiso={fechaPermiso}
            />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
