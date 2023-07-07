import { User } from 'src/app/entities/user';
import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';

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

}
