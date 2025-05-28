import type { PermisosDto, PermisosDtoPost } from "./Response";

export interface PropsTableModel {
    tittle: string;
    columns: Array<string>;
    listPermisos: Array<PermisosDto>;
    listTipoPermisos: Array<PropsTableModelSelect>;
    UpdatePermisos: (permiso: PermisosDtoPost) => Promise<PermisosDtoPost>;
}

export interface PropsTableModelSelect {
    value: number | string;
    label: string;
}

export interface PropsForm {
  listSelectItems: Array<PropsTableModelSelect> | undefined;
}

