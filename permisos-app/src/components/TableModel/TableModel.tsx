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
import { Button, MenuItem, OutlinedInput, Select, TextField, useTheme, type SelectChangeEvent, type Theme } from '@mui/material';
import type { PropsTableModel } from '../../Models/Models';

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




function Row(props: { row: ReturnType<typeof createData2> }) {
  const { row } = props;
  const [open, setOpen] = React.useState(false);

  console.log('Row', row);

  const theme = useTheme();
  const [personName, setPersonName] = React.useState<string[]>([]);

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
                    <TableCell><TextField  id="nombreEmpleado" label="NombreEmpleado" variant="outlined" /></TableCell>
                    <TableCell><TextField id="apellidoEmpleado" label="ApellidoEmpleado" variant="outlined" /></TableCell>
                    <TableCell align="right"><TextField type='date' id="fechaPermiso" label="FechaPermiso"  /></TableCell>
                    <TableCell align="right">
                        <Select
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
                    <TableCell align="right"><Button>Modificar</Button></TableCell>
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
            <Row key={row.nombreEmpleado} row={row} />
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
