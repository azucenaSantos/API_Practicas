import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any; //Variable para almacenar los usuarios, de tipo cualquiera, de momento

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers(); //Llamamos al metodo getUsers para obtener los usuarios
  }

  registerToggle() {
    this.registerMode = !this.registerMode; //cambia al valor opuesto
  }

  getUsers() { //VAMOS A PASAR EL RESULTADO DE LOS USERS AL COMPONENTE HIJO, APP-REGISTER
    //llamamos al servidor de la API para obtener los usuarios
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response), //si la peticion es correcta, almacenamos los usuarios en la variable users
      error: (error) => console.log(error), //si hay un error, lo mostramos por consola
      complete: () => console.log('Peticion completa'), //cuando la peticion se completa, mostramos un mensaje por consola
    });
    //el get devuelve un "observable", por lo que debemos suscribirnos a el para obtener los datos
  }

  cancelRegisterMode(event:boolean){
    this.registerMode= event;
    //Recibe un evento, en este caso el que creamos en el hijo 
    //y lo asigna a la variable registerMode que determina si se muestra o no el formulario de registro
  }
}
