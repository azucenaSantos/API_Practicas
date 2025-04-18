import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  //implementa el OnInit por defecto

  model: any = {}; //Modelo para el formulario de busqueda
  //loggedIn= false;

  constructor(public accountService: AccountService, private router: Router, 
    private toaster: ToastrService) {} //INYECTAMOS EL SERVICIO EN EL CONSTRUCTOR!! + el ruter de navegacion + toaster

  ngOnInit(): void {
    //this.getCurrentUser(); //llamamos al metodo getCurrentUser para comprobar si hay un usuario iniciado
  }

  //Metodo de comprobacion de usuario iniciado
  /*getCurrentUser(){
    this.accountService.currentUser$.subscribe({ //nos hemos suscrito pero no desuscribimos, debemos hacerlo
      next: user=>this.loggedIn=!!user, //si el usuario existe, loggedIn es true, si no, es false
      error: error=>console.log(error) //si hay un error, lo mostramos por consola
    })
  }*/

  login() {
    //Cuando se envia el form, se llama a este metodo
    //Dentro de este metodo se llama a la funcion login del servicio
    //A la funcion login se le pasa el modelo
    //Como lo que devuelve la funcion de login es un obvservable, se puede subscribir a el
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'), //cuando iniciamos sesion se dirige a x pagina determinada
      //error: error => this.toaster.error(error.error) //en el toaster se mostrará el error -> borramos porque esta controlado por el interceptor ahora
    })
  }

  logout() {
    this.accountService.logout(); //elimina el usuario del local storage
    this.router.navigateByUrl('/');
  }
}
