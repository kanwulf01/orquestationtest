import type { PermisosDto, TipoPermisoDto } from "./Response";

export interface PropsTableModel {
    tittle: string;
    columns: Array<string>;
    listPermisos: Array<PermisosDto>;
    listTipoPermisos: Array<TipoPermisoDto>;
}