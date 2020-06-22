import { Component, OnInit } from '@angular/core';
import{ HttpClient} from '@angular/common/http'

@Component({
  selector: 'app-criar-conta-usuario',
  templateUrl: './criar-conta-usuario.component.html',
  styleUrls: ['./criar-conta-usuario.component.css']
})
export class CriarContaUsuarioComponent implements OnInit {

  endpoint = "http://localhost:53733/api/Usuario";
  mensagem:string;

  erroNome=[];
  erroEmail=[];
  erroSenha=[];
  erroSenhaConfirmacao=[];

  constructor(private httpClient:HttpClient) { }

  ngOnInit(): void {
  }

  cadastrarUsuario(formCadastro){
    this.mensagem = "Processando, por favor aguarde..."

    this.erroNome=[];
    this.erroEmail=[];
    this.erroSenha=[];
    this.erroSenhaConfirmacao=[];

    this.httpClient.post(this.endpoint, formCadastro.value, {responseType : 'text'})
        .subscribe(
          data =>{
            this.mensagem = data.toString();
            formCadastro.reset();
          },
          e => {
            const errosValidacao = JSON.parse(e.error)
            this.erroNome = errosValidacao.Nome;
            this.erroEmail = errosValidacao.Email;
            this.erroSenha = errosValidacao.Senha;
            this.erroSenhaConfirmacao = errosValidacao.SenhaConfirmacao;

            this.mensagem = "";
          }
        );
  }

}
