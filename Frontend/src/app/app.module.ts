import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CadastroFuncionarioComponent } from './cadastro-funcionario/cadastro-funcionario.component';
import { ConsultaFuncionarioComponent } from './consulta-funcionario/consulta-funcionario.component';

import {RouterModule, Routes} from '@angular/router';

import{FormsModule, ReactiveFormsModule} from '@angular/forms';

import{ HttpClientModule } from '@angular/common/http';
import { AutenticarUsuarioComponent } from './autenticar-usuario/autenticar-usuario.component';
import { CriarContaUsuarioComponent } from './criar-conta-usuario/criar-conta-usuario.component';

import { CookieService } from 'ngx-cookie-service';

const appRoutes: Routes = [
  {path : 'cadastro-funcionario', component : CadastroFuncionarioComponent},
  {path : 'consulta-funcionario', component : ConsultaFuncionarioComponent},
  {path : 'autenticar-usuario', component : AutenticarUsuarioComponent},
  {path : 'criar-conta-usuario', component : CriarContaUsuarioComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    CadastroFuncionarioComponent,
    ConsultaFuncionarioComponent,
    AutenticarUsuarioComponent,
    CriarContaUsuarioComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
