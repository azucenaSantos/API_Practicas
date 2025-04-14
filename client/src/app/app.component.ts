import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

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
  constructor(private accountService: AccountService) {
    //Aqui inyectamos el servicio AccountService
    //Recibimos el servicio HttpClient en el constructor
  }
  //Metodo que se ejecuta al iniciar el componente
  ngOnInit(): void {
    ///this.getUsers(); //Llamamos al metodo getUsers para obtener los usuarios
    this.setCurrentUser(); //Llamamos al metodo setCurrentUser para setear el usuario actual
  }

  //Metodo para setear el usuario actual
  setCurrentUser() {
    const userString = localStorage.getItem('user'); //Obtenemos el usuario del local storage
    if (!userString) return; //Si no hay usuario, no hacemos nada
    //Si si hay usuario, parseamos el string a un objeto de tipo User
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user); //Llamamos al metodo setCurrentUser del servicio AccountService
  }
}
