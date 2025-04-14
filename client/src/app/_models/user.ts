//Almacen de token y usuario
export interface User{
    username: string;
    token: string;
    //El token es el que se usa para autenticar al usuario en la API
    //El username es el nombre de usuario que se usa para iniciar sesion
}