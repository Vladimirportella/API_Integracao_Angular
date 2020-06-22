import { Component, OnInit } from '@angular/core';
import{ HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-cadastro-funcionario',
  templateUrl: './cadastro-funcionario.component.html',
  styleUrls: ['./cadastro-funcionario.component.css']
})
export class CadastroFuncionarioComponent implements OnInit {

endPoint = "http://localhost:53733/api/Funcionario";
mensagem:string;

errosNome = [];
errosSalario = [];
errosDataAdmissao = [];

  access_token = "";

  constructor(private httpClient:HttpClient,
              private cookieService:CookieService) { }

  ngOnInit(): void {

    if(this.cookieService.get('ACCESS_TOKEN') == ''){
      window.location.href = 'autenticar-usuario';
    }
    
    this.access_token = this.cookieService.get('ACCESS_TOKEN');
  }

  cadastrarFuncionario(formCadastro){
    this.mensagem = "Processando, por favor aguarde..."
    this.errosNome = [];
    this.errosSalario = [];
    this.errosDataAdmissao = [];

    const headers = new HttpHeaders().set('Authorization', 'Bearer ' + this.access_token)

    this.httpClient.post(this.endPoint, formCadastro.value, {responseType : 'text', headers})
      .subscribe(
          data =>{
          this.mensagem = data.toString();
          formCadastro.reset();
        },
        e =>{
          const validacoes = JSON.parse(e.error);

          this.errosNome = validacoes.Nome;
          this.errosSalario = validacoes.Salario;
          this.errosDataAdmissao = validacoes.DataAdmissao;

          this.mensagem = "";
        }
      )
  }

}
