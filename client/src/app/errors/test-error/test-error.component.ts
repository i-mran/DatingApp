import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { error } from 'console';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl='https://localhost:7034/api/';

  validationErrors:string[]=[];
     
  constructor(private htttp:HttpClient) { }

  ngOnInit(): void {
  }

  get404Error(){
    this.htttp.get(this.baseUrl + 'buggy/not-found').subscribe({
      next:response=>
        console.log(response),
      error:error=>console.log(error)
        
      
    })
  }

   get400Error(){
    this.htttp.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next:response=>
        console.log(response),
      error:error=>console.log(error)
        
      
    })
  }

   get500Error(){
    this.htttp.get(this.baseUrl + 'buggy/server-error').subscribe({
      next:response=>
        console.log(response),
      error:error=>console.log(error)
        
      
    })
  }

   get401Error(){
    this.htttp.get(this.baseUrl + 'buggy/auth').subscribe({
      next:response=>
        console.log(response),
      error:error=>console.log(error)
        
      
    })
  }

   get400ValidationError(){
    this.htttp.post(this.baseUrl + 'account/register',{}).subscribe({
      next:response=>
        console.log(response),
      error:error=>{
        console.log(error);
        this.validationErrors=error;
      }
        
      
    })
  }
}
