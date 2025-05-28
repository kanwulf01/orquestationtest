
//const URL = "https://localhost:44394/api/"
//const URL = "http://localhost:32768/api/";
const URL = import.meta.env.VITE_API_URL
import axios from 'axios';
import type { PermisosDto, PermisosDtoPost, TipoPermisoDto } from '../Models/Response';

export async function getAllPermisos() : Promise<Array<PermisosDto>> { 
    const response = await axios(URL + "Permiso");
    if (response.status !== 200) {
        throw new Error(`Error fetching permisos: ${response.statusText}`);
    }
    return await response.data;
}

export async function UpdatePermisoPost(permiso: PermisosDtoPost) : Promise<PermisosDtoPost> {
    const response = await axios.put(URL + "Permiso", permiso);
    if (response.status !== 200) {
        throw new Error(`Error updating permiso: ${response.statusText}`);
    }
    return await response.data;
}

export async function PostPermiso(permiso: PermisosDtoPost) : Promise<PermisosDtoPost> {
    const response = await axios.post(URL + "Permiso", permiso);
    if (response.status !== 200) {
        throw new Error(`Error updating permiso: ${response.statusText}`);
    }
    return await response.data;
}

export async function getAllTipoPermisos() : Promise<Array<TipoPermisoDto>> { 
    const response = await axios(URL + "TipoPermiso");
    if (response.status !== 200) {
        throw new Error(`Error fetching tipo permisos: ${response.statusText}`);
    }
    return await response.data;
}

