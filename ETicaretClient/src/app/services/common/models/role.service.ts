import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from './../http-client.service';
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClientService: HttpClientService) { }
  
  async getRoles(page: number, size: number, successCallBack?: () => void, errorCallBack?: (error) => void){
    const observable: Observable<any> = this.httpClientService.get({
      controller: "roles",
      queryString: `page=${page}&size=${size}`
    });

    firstValueFrom(observable).then(successCallBack)
    .catch(errorCallBack)
 
    return await firstValueFrom(observable);
  }

  async create(name: string, successCallBack?: () => void, errorCallBack?: (error) => void){
    const observable: Observable<any> = this.httpClientService.post({
      controller: "roles",
    },{name: name});


    firstValueFrom(observable).then(successCallBack)
    .catch(errorCallBack)

    return await firstValueFrom(observable) as {succeeded: boolean};
  }
}
