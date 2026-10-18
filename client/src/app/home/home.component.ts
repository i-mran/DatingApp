  import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

  @Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
  })
  export class HomeComponent implements OnInit {
    registerMode=false;
    users:any;
    constructor(private http:HttpClient) { }

    ngOnInit(): void {
        this.GetUsers();
    }

    registerToggle(){
      this.registerMode=!this.registerMode;
    }
    GetUsers(){
          this.http.get('https://localhost:7034/api/users').subscribe({
        next:response =>this.users=response,
        complete:() => console.log('Request completed'),
        error:error => console.log(error)
      })
    }
    cancelRegisterMode(event:boolean){
      this.registerMode=event;
    }

  }  

