import Box from '@mui/material/Box';
import { createTheme } from '@mui/material/styles';
import DashboardIcon from '@mui/icons-material/Dashboard';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import { AppProvider, type Navigation } from '@toolpad/core/AppProvider';
import { DashboardLayout } from '@toolpad/core/DashboardLayout';
import { DemoProvider, useDemoRouter } from '@toolpad/core/internal';
import TableModel from '../../components/TableModel/TableModel';
import type { PermisosDto, TipoPermisoDto } from '../../Models/Response';
import { getAllPermisos } from '../../api/api';
import { useEffect, useState } from 'react';
import type { PropsTableModel } from '../../Models/Models';

const NAVIGATION: Navigation = [
  {
    segment: 'dashboardPermisos',
    title: 'Permisos',
    icon: <DashboardIcon />,
  },
  {
    segment: 'dashboardTipoPermisos',
    title: 'TipoPermisos',
    icon: <ShoppingCartIcon />,
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


function DemoPageContent( {tittle, columns, listPermisos, listTipoPermisos} :PropsTableModel) {

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
            <TableModel tittle={tittle} columns={columns} listPermisos={listPermisos} listTipoPermisos={listTipoPermisos}/>
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

  const router = useDemoRouter('/dashboard');

  // Remove this const when copying and pasting into your project.
  const demoWindow = window !== undefined ? window() : undefined;

  useEffect(() => {
    console.log('router', router);
    console.log('router.pathname', router.pathname);
    setPathName(router.pathname);

    async function GetAllPermisos() {
      try {
        const response = await getAllPermisos();
        console.log('response', response);
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

  const swicthPageContent = (pathname: string) => {
    if(pathname === '/dashboardPermisos') {
      //Data quemada por ejemplo
      let tittle = "Permisos";
      // let listPermisos:Array<PermisosDto> = [
      //   { id: 1, nombreEmpleado: 'Permiso 1', apellidoEmpleado: 'Descripcion 1', tipoPermiso: 1, fechaPermiso: '2023-01-01' },
      //   { id: 2, nombreEmpleado: 'Permiso 2', apellidoEmpleado: 'Descripcion 2', tipoPermiso: 2, fechaPermiso: '2023-01-02' },
      // ]
      let listTipoPermisos:Array<TipoPermisoDto> = [
        { Id: 1, Descripcion: 'TipoPermiso 1' },
        { Id: 2, Descripcion: 'TipoPermiso 2' }
      ]
      let columns:Array<string> = ['NombreEmpleado', 'ApellidoEmpleado', 'TipoPermiso', 'FechaPermiso'];
      return <DemoPageContent tittle={tittle} columns={columns} listPermisos={listItems} listTipoPermisos={listTipoPermisos} />;
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
          {swicthPageContent(pathName)}
        </DashboardLayout>
      </AppProvider>
      {/* preview-end */}
    </DemoProvider>
  );
}
