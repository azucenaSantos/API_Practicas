import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';



@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent
    //Aqui se declaran los componentes que se van a usar en la aplicacion
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule, //Modulo que permite hacer peticiones HTTP (a la API)
    FormsModule, //Modulo que permite usar formularios en Angular
    BsDropdownModule.forRoot() //Modulo que permite usar el dropdown de Bootstrap    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
