import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isAuthenticated = false;

  constructor(private cookieService:CookieService, private router:Router){}

  ngOnInit(){
    this.isAuthenticated = this.cookieService.get('ACCESS_TOKEN') != "";
  }
  logout(){
    this.cookieService.delete('ACCESS_TOKEN');
    this.isAuthenticated = false;
    this.router.navigate(['/autenticar-usuario']);
  }
 
}
