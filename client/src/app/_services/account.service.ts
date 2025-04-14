import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

//Los servicios de angular pueden ser injectables en los componentes o en otros servicios
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  //Servicio responsable de hacer las peticiones HTTP desde
  //el cliente al servidor --> centralizacion de las peticiones HTTP
  baseUrl = 'https://localhost:5001/api/'; //URL base de la API

  //Podemos acceder a este observable desde cualquier componente, cosa que con la almacenacion del localStorage solo podriamos acceder desde el nav 
  private currentUserSource= new BehaviorSubject<User | null>(null); //Creamos un observable que almacena el usuario actual 
  currentUser$= this.currentUserSource.asObservable(); //Creamos un observable que emite el usuario actual

  constructor(private http:HttpClient) {
    //Inyeccion del servicio HttpClient en el constructor
    //para poder hacer peticiones HTTP
  }

  login(model: any){
    //A lo que devuelve el post le especicifamos que es un User
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User)=>{ //la respuesta nos devolverá un objeto de tipo User que es una interfaz que almacena los datos del usuario
        const user= response;
        if(user){
          localStorage.setItem('user', JSON.stringify(user)); //Guardamos el usuario en el local storage si lo hay
          //Además de guardar en el local storage, lo guardamos en el observable
          this.currentUserSource.next(user); //Emitimos el usuario actual
        }
      })
    );
    //Estamos retornando un observable como un objeto
  }

  register(model:any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user=> {
        if(user){
          localStorage.setItem('user', JSON.stringify(user)); //Guardamos el usuario en el local storage si lo hay
          this.currentUserSource.next(user); //Emitimos el usuario actual
        }
        return user; //Devolvemos el usuario, si no es null en el primer null
      })
    )

  }

  //Metodo para setear el usuario actual
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user'); //Eliminamos el usuario del local storage al hacer logout
    //Aqui tabien debemoos  eliminar el usuario del observable
    this.currentUserSource.next(null); //Emitimos null para indicar que no hay usuario
  }

//Este servicio se instancia cuando se inicia la aplicaicion y se
//destruye cuando se cierra la aplicacion (un servicio está disponible para usar en cualquier punto de la aplicacion)
//Hasta aqui hemos creado el servicio, debemos inyectarlo en el componente indicado para poder usarlo.
}