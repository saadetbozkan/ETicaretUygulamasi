import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from './../http-client.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeEndpointService {

  constructor(private httpClientService: HttpClientService) { }

  async assignRoleEndoint(roles: string[], code: string, menu: string, successCallBack?: () => void, errorCallBack?: (error)=> void){
    const observable: Observable<any> = this.httpClientService.post({
      controller : "AuthorizationEndpoints"
    },{
       roles: roles,
       code: code,
       menu: menu
      });

      const promiseData = firstValueFrom(observable);
      promiseData.then(successCallBack).catch(errorCallBack);
      await promiseData;
  }

  async getRolesToEndpoint( code:string, menu:string, successCallBack?: () => void, errorCallBack?: (error)=> void): Promise<string[]>{
    const observable: Observable<any> = this.httpClientService.post({
      controller : "AuthorizationEndpoints",
      action : "get-roles-to-endpoint"
    },{
      code: code,
     menu: menu
    });
    
      const promiseData = firstValueFrom(observable);
      promiseData.then(successCallBack).catch(errorCallBack);
      return (await promiseData).roles;
  }
}
