import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { List_Order } from 'src/app/contracts/order/list_order';
import { HttpErrorResponse } from '@angular/common/http';
import { Order_Detail } from 'src/app/contracts/order/order_detail';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async getAll(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ totalOrderCount: number; orders: List_Order[] }> {
    const observable: Observable<{ totalOrderCount: number; orders: List_Order[] }> = this.httpClientService.get({
      controller: "orders",
      queryString: `page=${page}&size=${size}`
    });

    firstValueFrom(observable).then(d => successCallBack())
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message));

    return await firstValueFrom(observable);
  }

  async create(order: Create_Order): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "orders"
    }, order);

    await firstValueFrom(observable);
  }

  async getById(id: string, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<Order_Detail> {
    const observable: Observable<Order_Detail> = this.httpClientService.get({
      controller: "orders"
    }, id);

    firstValueFrom(observable).then(d => successCallBack)
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))
    return await firstValueFrom(observable);
  }
}
