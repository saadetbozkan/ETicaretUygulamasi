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
    var promiseData = firstValueFrom(observable);
    promiseData.then(successCallBack)
    .catch(errorCallBack)
 
    return await promiseData;
  }

  async create(name: string, successCallBack?: () => void, errorCallBack?: (error) => void){
    const observable: Observable<any> = this.httpClientService.post({
      controller: "roles",
    },{name: name});
    
    var data : { succeeded: boolean} = await firstValueFrom(observable);
    var funk;
    if(data.succeeded)
      funk = successCallBack;
    else
      funk = errorCallBack;
    firstValueFrom(observable).then(funk)
    .catch(funk)

    return data ;
  }
  async update(id: string, name: string, successCallBack?: () => void, errorCallBack?: (error) => void){
    const observable: Observable<any> = this.httpClientService.put({
      controller: "roles",
    },{id: id, name: name});

    var promiseData = firstValueFrom(observable);
    promiseData.then(successCallBack)
    .catch(errorCallBack)
    return await promiseData;
  }
}
