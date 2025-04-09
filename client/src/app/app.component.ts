import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  //Implementamos la interfaz OnInit para poder usar el metodo ngOnInit
  //Propiedades de la clase
  title = 'Dating App';
  users: any; //Variable para almacenar los usuarios, de tipo cualquiera, de momento

  //Constructor de la clase
  constructor(private htttp: HttpClient) {
    //Recibimos el servicio HttpClient en el constructor
  }
  //Metodo que se ejecuta al iniciar el componente
  ngOnInit(): void {
    //llamamos al servidor de la API para obtener los usuarios
    this.htttp.get('https://localhost:5001/api/users').subscribe({
      next: (response) => (this.users = response), //si la peticion es correcta, almacenamos los usuarios en la variable users
      error: (error) => console.log(error), //si hay un error, lo mostramos por consola
      complete: () => console.log('Peticion completa'), //cuando la peticion se completa, mostramos un mensaje por consola
    });
    //el get devuelve un "observable", por lo que debemos suscribirnos a el para obtener los datos
  }
}
