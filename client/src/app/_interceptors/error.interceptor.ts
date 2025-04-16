import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable() //Es inyectable pero no vamos a inyectarlo en ningun sitio
export class ErrorInterceptor implements HttpInterceptor { //INTERCEPTA Y MANEJA UNA PETICION HTTP

  //Inyectamos el router para redirigir al usuario a otro lugar en  caso de x error
  //Inyectamos el toaster para los errores
  constructor(private router: Router, private toastr:ToastrService) {}

  //Metodo de intercepción
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      //cacheamos los errores dentro 
      catchError((error:HttpErrorResponse)=>{
        if(error){
          //Si tenemos un error 
          switch(error.status){
            case 400:
              if(error.error.errors){ //comprobar si es un array, porque de caso 400 hay request donde devuelve el error y otro donde devuelve un error que es un array (contraseña y nombre requeridos)
                const modelStateErrors=[];
                for(const key in error.error.errors){
                  if(error.error.errors[key]){
                    modelStateErrors.push(error.error.errors[key]);
                    //Estamos construyendo un array con los errores de la request de validacion
                  }
                }
                throw modelStateErrors.flat(); //lanzamos el array con los errores
              }else{
                //En caso de un request 404 normal
                this.toastr.error(error.error, error.status.toString()); //pasamos el error y el codigo del error
              }
              break;
            case 401:
              this.toastr.error("Unauthorized", error.status.toString());
              break;
            case 404:
              //En este caso, redirigimos al usuario con el router
              this.router.navigateByUrl('/not-foud');
              break;
            case 500:
              const navigationExtras: NavigationExtras= {state: {error: error.error}};
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error("Something unexpected went wrong");
              console.log(error);
              break;
          }
        }
        //Necesitamos lanzar el error si o si para que el catchError funcione
        throw error;
      })
    )
  }
}
