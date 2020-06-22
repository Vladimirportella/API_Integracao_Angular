import { Component, OnInit } from '@angular/core';
import{ HttpClient} from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-autenticar-usuario',
  templateUrl: './autenticar-usuario.component.html',
  styleUrls: ['./autenticar-usuario.component.css']
})
export class AutenticarUsuarioComponent implements OnInit {

  endpoint = "http://localhost:53733/api/Login";
  mensagem:string;

  erroEmail=[];
  erroSenha=[];

  constructor(private httpClient:HttpClient,
              private cookieService: CookieService) { }

  ngOnInit(): void {
  }

  autenticarUsuario(formAcesso){
    this.mensagem = "Processando, por favor aguarde..."

    this.erroEmail=[];
    this.erroSenha=[];

    this.httpClient.post(this.endpoint, formAcesso.value, {responseType : 'text'})
        .subscribe(
          data =>{
            this.cookieService.set('ACCESS_TOKEN', data.toString());
            formAcesso.reset();

            window.location.href = 'consulta-funcionario';
          },
          e => {

            if(e.status==401){
              this.mensagem = "Acesso Negado.";
            }
            else{
              const errosValidacao = JSON.parse(e.error)
              
              this.erroEmail = errosValidacao.Email;
              this.erroSenha = errosValidacao.Senha;
  
              this.mensagem = "";
            }
          }
        );

  }

  

}
