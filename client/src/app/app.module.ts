import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
    //Aqui se declaran los componentes que se van a usar en la aplicacion
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule //Modulo que permite hacer peticiones HTTP (a la API)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
