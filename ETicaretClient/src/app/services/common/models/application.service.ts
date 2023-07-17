import { Observable, firstValueFrom} from 'rxjs';
import { HttpClientService } from './../http-client.service';
import { Injectable } from '@angular/core';
import { Menu } from 'src/app/contracts/application-configuration/menu';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private httpClientService: HttpClientService) { }

  async getAuthorizeDefinitionEndPoints(): Promise<Menu[]>{
    const observable: Observable<Menu[]> = this.httpClientService.get({
      controller: "ApplicationServices"
    });

    return await firstValueFrom(observable);
  }
}
