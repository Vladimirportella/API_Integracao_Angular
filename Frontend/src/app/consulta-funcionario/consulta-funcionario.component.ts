import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-consulta-funcionario',
  templateUrl: './consulta-funcionario.component.html',
  styleUrls: ['./consulta-funcionario.component.css']
})
export class ConsultaFuncionarioComponent implements OnInit {

  endpoint = "http://localhost:53733/api/Funcionario";
  mensagem:string;
  mensagemEdicao:string;

  errosNome=[];
  errosSalario=[];
  errosDataAdmissao=[];

  listagemDeFuncionarios = [];

  funcionarioEdicao= {
    idFuncionario : 0,
    nome : '',
    salario : 0,
    dataAdmissao: ''
  };

  access_token = "";

  constructor(private httpClient:HttpClient, private cookieService:CookieService) { }

  ngOnInit(): void {
    
    if(this.cookieService.get('ACCESS_TOKEN') == ''){
      window.location.href = 'autenticar-usuario';
    }

    this.access_token = this.cookieService.get('ACCESS_TOKEN');

    this.consultarFuncionarios()
  }

  consultarFuncionarios(){

    const headers = new HttpHeaders().set('Authorization', 
    'Bearer ' + this.access_token);

    this.httpClient.get(this.endpoint,{headers}).subscribe(
      (data:any[]) => {
        this.listagemDeFuncionarios = data;
      },
      e =>{
        this.mensagem = e.toString();
      }
    )
  }

  excluirFuncionario(idFuncionario){
    if(window.confirm('Deseja realmente excluir o funcionário?')){
      this.mensagem="Processando exclusão, por favor aguarde...";

      const headers = new HttpHeaders().set('Authorization', 
    'Bearer ' + this.access_token);

      this.httpClient.delete(this.endpoint + "/" + idFuncionario, { responseType : 'text', headers})
        .subscribe(
          data => {
          this.mensagem = data.toString();
          this.consultarFuncionarios();
        },
        e=>{
          this.mensagem = e.toString();
        }
      )
    }
  }

  obterFuncionario(idFuncionario){
    const headers = new HttpHeaders().set('Authorization', 
    'Bearer ' + this.access_token);
    
    this.httpClient.get(this.endpoint + "/" + idFuncionario,{headers})
      .subscribe(
        (data:any) => {
          this.funcionarioEdicao = data;
          this.mensagem="";
        },
        e=>{
          this.mensagem = e.toString();
        }
      )
  }

  atualizarFuncionario(formEdicao){
    this.mensagemEdicao = "Processando, por favor aguarde...";

    const headers = new HttpHeaders().set('Authorization', 
    'Bearer ' + this.access_token);

    this.httpClient.put(this.endpoint, formEdicao.value,{ responseType : 'text', headers})
      .subscribe(
        (data) => {
          this.mensagemEdicao = data.toString();
          this.consultarFuncionarios();
        },
        e=>{
          this.mensagemEdicao = e.toString();
        }

    )
  }
}
