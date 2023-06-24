import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Create_Order } from 'src/app/contracts/order/create_order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async create(order: Create_Order): Promise<void>{
    const observable: Observable<any> = this.httpClientService.post({
      controller:"orders"
    }, order); 

    await firstValueFrom(observable);
  }
 
}
