import { User } from 'src/app/entities/user';
import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';
import { List_User } from 'src/app/contracts/users/list_user';
import { HttpErrorResponse } from '@angular/common/http';
import { Order_Detail } from 'src/app/contracts/order/order_detail';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService,) { }

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller: "users"
    }, user);

    return await firstValueFrom(observable) as Create_User;
  }

  async updatePassword(userId: string, resetToken: string, password: string, passwordConfirm: string, successCallBackFunction?: () => void, errorCallBackFunction?: (error) => void){
    const observable: Observable<any> = this.httpClientService.post({
      controller: "users",
      action: "update-password"
    }, {userId:userId, resetToken: resetToken, password: password, passwordConfirm: passwordConfirm});

    debugger;
    const promiseData: Promise<any> = firstValueFrom(observable);
    promiseData.then(value => successCallBackFunction()).catch(error => errorCallBackFunction(error));
    return await promiseData;
  }

  async getAll(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ totalUsersCount: number; users: List_User[] }> {
    const observable: Observable<{ totalUsersCount: number; users: List_User[] }> = this.httpClientService.get({
      controller: "users",
      queryString: `page=${page}&size=${size}`
    });

    var promiseData = firstValueFrom(observable);
    promiseData.then(d => successCallBack())
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message));

    return await promiseData;
  }

  async assignRoleUser(userId: string, roles: string[], successCallBack?: () => void, errorCallBack?: (error)=> void){
    const observable: Observable<any> = this.httpClientService.post({
      controller : "Users",
      action: "assign-role-to-user"
    },{
        userId: userId,
        roles: roles
      });

      const promiseData = firstValueFrom(observable);
      promiseData.then(successCallBack).catch(errorCallBack);
      await promiseData;
  }

  async getRolesToUser(userId: string, successCallBack?: () => void, errorCallBack?: (error)=> void): Promise<string[]>{
    const observable: Observable<any> = this.httpClientService.get({
      controller: "Users",
      action: "get-roles-to-user"
    },userId);

    var promiseData = firstValueFrom(observable);
    promiseData.then(successCallBack).catch(errorCallBack);

    return (await promiseData).roles;
  }

  async getOrdersToCurrentUser( successCallBack?: () => void, errorCallBack?: (error)=> void){
    const observable: Observable<any> = this.httpClientService.get({
      controller: "Users",
      action: "get-orders-to-current-user"
    });
    const promiseData = firstValueFrom(observable);
    promiseData.then(successCallBack).catch(errorCallBack);
    return (await promiseData).orderListWithBasketItemList;
  }

}
