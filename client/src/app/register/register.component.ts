import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  //Para recibir informacion del componente padre:
  //@Input() usersFromHomeComponents: any;

  //Variable de salida para mandar un dato al padre
  @Output() cancelRegister = new EventEmitter(); //creamos un evento para enviar datos al padre

  model: any = {}; //objeto vacio por defecto

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  //Metodo para registrar un nuevo usuario
  register() {
    //llamamos a la funcion registrar del servicio accountService
    this.accountService.register(this.model).subscribe({
      next: (response) => {
        console.log(response); //si la peticion es correcta, mostramos la respuesta por consola
        this.cancel(); //llamamos al metodo cancel para cerrar el formulario de registro
      },
      error: (error) => {
        console.log(error); //si hay un error, lo mostramos por consola
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false); //emitimos el evento cancelRegister y le pasamos el valor false
    //el valor false será el que desactive el modo de registro en el componente padre
  }
}
