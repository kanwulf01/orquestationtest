export type TipoPermiso = {
    id: number;
    descripcion: string;
}

export type PermisosDto = { 

    id: number;
    nombreEmpleado: string;
    apellidoEmpleado: string;
    tipoPermiso: TipoPermiso;
    fechaPermiso: string;
    tipoPermisoLabel:string | null | undefined;
}

export type PermisosDtoPost = { 

    id: number;
    nombreEmpleado: string;
    apellidoEmpleado: string;
    tipoPermisoId: number;
    tipoPermisoLabel?: string | null | undefined;
    fechaPermiso: string;
    tipoPermiso?: number; 

}

export type TipoPermisoDto = { 
    id: number;
    descripcion: string;
}