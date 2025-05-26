
const URL = "https://localhost:44394/api/"

import axios from 'axios';
import type { PermisosDto } from '../Models/Response';

export async function getAllPermisos() : Promise<Array<PermisosDto>> { 
    const response = await axios(URL + "Permiso");
    if (response.status !== 200) {
        throw new Error(`Error fetching permisos: ${response.statusText}`);
    }
    return await response.data;
}