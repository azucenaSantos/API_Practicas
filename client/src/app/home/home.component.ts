import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any; //Variable para almacenar los usuarios, de tipo cualquiera, de momento

  constructor() {}

  ngOnInit(): void {

  }

  registerToggle() {
    this.registerMode = !this.registerMode; //cambia al valor opuesto
  }

  cancelRegisterMode(event:boolean){
    this.registerMode= event;
    //Recibe un evento, en este caso el que creamos en el hijo 
    //y lo asigna a la variable registerMode que determina si se muestra o no el formulario de registro
  }
}
