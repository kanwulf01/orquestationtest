import Box from '@mui/material/Box';
import { createTheme } from '@mui/material/styles';
import AddBoxIcon from '@mui/icons-material/AddBox';
import ListAltIcon from '@mui/icons-material/ListAlt';
import { AppProvider, type Navigation } from '@toolpad/core/AppProvider';
import { DashboardLayout } from '@toolpad/core/DashboardLayout';
import { DemoProvider, useDemoRouter } from '@toolpad/core/internal';
import TableModel from '../../components/TableModel/TableModel';
import type { PermisosDto, PermisosDtoPost } from '../../Models/Response';
import { getAllPermisos, getAllTipoPermisos, UpdatePermisoPost } from '../../api/api';
import { useEffect, useState } from 'react';
import type { PropsTableModel, PropsTableModelSelect } from '../../Models/Models';
import PermisoForm from '../../components/PermisoForm/PermisoForm';

const NAVIGATION: Navigation = [
  {
    segment: 'dashboardPermisos',
    title: 'Liste - Actualice Permisos',
    icon: <ListAltIcon />,
  },
  {
    segment: 'dashboardCrearPermiso',
    title: 'Registre Permiso',
    icon: <AddBoxIcon />,
    // action: <Typography>Orders</Typography>,
  },
];

const demoTheme = createTheme({
  cssVariables: {
    colorSchemeSelector: 'data-toolpad-color-scheme',
  },
  colorSchemes: { light: true, dark: true },
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 600,
      lg: 1900,
      xl: 2836,
    },
  },
});


function Page1Content( {tittle, columns, listPermisos, listTipoPermisos, UpdatePermisos} :PropsTableModel) {

    if(true) { 
        return (
            <Box
            sx={{
                py: 4,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                textAlign: 'center',
            }}
            >
            <div>Lista de Permisos</div>
            <br/>
            <TableModel tittle={tittle} columns={columns} listPermisos={listPermisos} listTipoPermisos={listTipoPermisos} UpdatePermisos={UpdatePermisos}/>
            </Box>
        );
    }
  
}

function Page2Content({listSelectItems} : {listSelectItems?: Array<PropsTableModelSelect>}) {

    if(true) { 
        return (
            <Box
            sx={{
                py: 4,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                textAlign: 'center',
            }}
            >
            <div>Crea Permiso</div>
            <br/>
            <PermisoForm listSelectItems={listSelectItems}/>
            </Box>
        );
    }
  
}



interface DemoProps {
  /**
   * Injected by the documentation to work in an iframe.
   * Remove this when copying and pasting into your project.
   */
  window?: () => Window;
}

export default function Layout(props: DemoProps) {
  const { window } = props;

  const [pathName, setPathName] = useState<string>('/dashboardPermisos');
  const [listItems, setListItems] = useState<Array<PermisosDto>>([]);
  const [listSelectItems, setListSelectItems] = useState<Array<PropsTableModelSelect>>([]);
  

  const router = useDemoRouter('/dashboard');

  const demoWindow = window !== undefined ? window() : undefined;

  useEffect(() => {
    async function getTipoPermisos() {
      try {
        const response = await getAllTipoPermisos();
        if (response) {
          const formattedResponse = response.map((item) => ({
            value: item.id,
            label: item.descripcion,
          }));
          setListSelectItems(formattedResponse);
        } else {
          console.error('No data found in response');
        }
      } catch (error) {
        console.error('Error fetching permisos:', error);
      }
    }
    getTipoPermisos();
  }, []);

  useEffect(() => {

    setPathName(router.pathname);

    async function GetAllPermisos() {
      try {
        const response = await getAllPermisos();
        if (response) {
          setListItems(response);
        } else {
          console.error('No data found in response');
        }
      } catch (error) {
        console.error('Error fetching permisos:', error);
      }
    }

    if(router.pathname === '/dashboardPermisos') {
      GetAllPermisos();
    }

    

    // eslint-disable-next-line react-hooks/exhaustive-deps

  }, [router]);

  const UpdatePermisos = async (permiso: PermisosDtoPost) : Promise<PermisosDtoPost> => {
      //UPDATE LISTA DE PERMISOS EN MEMORIA
      const response = await UpdatePermisoPost({
        id: permiso.id,
        nombreEmpleado: permiso.nombreEmpleado,
        apellidoEmpleado: permiso.apellidoEmpleado,
        tipoPermisoId: permiso.tipoPermisoId,
        fechaPermiso: permiso.fechaPermiso,
        tipoPermiso: 0, // Assuming tipoPermiso is not needed for the update
      });
      const updatedList = listItems.map(item => 
        item.id === response.id ? {
          ...item,
          nombreEmpleado: response.nombreEmpleado,
          apellidoEmpleado: response.apellidoEmpleado,
          tipoPermisoId: response.tipoPermisoId,
          fechaPermiso: response.fechaPermiso,
          tipoPermisoLabel: permiso.tipoPermisoLabel,  
          tipoPermiso:{ id: response.tipoPermisoId, descripcion: permiso.tipoPermisoLabel+"" } // fomatea a string casting hard
        } : item
      );
      setListItems(updatedList);
      return response;
    }

  const SwicthPageContent = (pathname: string) => {
    if(pathname === '/dashboardPermisos') {
      let tittle = "Permisos";
      
      let columns:Array<string> = ['Nombre Empleado', 'Apellido Empleado', 'Tipo Permiso', 'Fecha Permiso'];
      
      return <Page1Content tittle={tittle} columns={columns} listPermisos={listItems} listTipoPermisos={listSelectItems} UpdatePermisos={UpdatePermisos} />;
    }

    if(pathname === '/dashboardCrearPermiso') {
      return (<>
        <Page2Content listSelectItems={listSelectItems}/>
      </>);
    }

    if(pathname === '/dashboard') {
      return (<>
        <Box
            sx={{
                py: 4,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                textAlign: 'center',
            }}
            >
            <div>Bienvenido al APP de los PERMISOS version DEMO!</div>
            </Box>
      </>);
    }

    return <></>
  }

  return (
    // Remove this provider when copying and pasting into your project.
    <DemoProvider window={demoWindow}>
      {/* preview-start */}
      <AppProvider
        navigation={NAVIGATION}
        branding={{
          logo: <img src="https://mui.com/static/logo.png" alt="MUI logo" />,
          title: 'MUI',
          homeUrl: '/toolpad/core/introduction',
        }}
        router={router}
        theme={demoTheme}
        window={demoWindow}
      >
        <DashboardLayout>
          {SwicthPageContent(pathName)}
        </DashboardLayout>
      </AppProvider>
      {/* preview-end */}
    </DemoProvider>
  );
}
