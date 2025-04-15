import { Injectable } from '@angular/core';
import {CanActivate} from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  //Interfaz que implementa que puede decidir si una ruta se activa o no
  // (en caso de no estar iniciada la sesion no podria acceder por la url a ningun apartado de la aplicacion) (devuelve true o false)

  constructor(
    private accountService: AccountService,
    private toaster: ToastrService
  ) {}

  //Lo que comprobaremos será que sin en el accountService hay un usuario
  //diferente de null, devuelva true y si no devuelva false y se detenga la navegacion
  //el AuthGuard se subscribe y desuscribe automaticamente al observable!!

  canActivate(): Observable<boolean> {//tipos de datos que puede devolver la funcion    
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user) return true;
        else {
          this.toaster.error('You shall not pass!!');
          return false;
        }
      })
    );
    //devolvemos true o false dependiendo de si el user es null o no
    //en app-routing se establece la navegación con esta condicion
  }
}
