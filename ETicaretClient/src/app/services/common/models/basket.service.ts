import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { List_Basket_Item } from 'src/app/contracts/basket/list_basket_item';
import { Create_Basket_Item } from 'src/app/contracts/basket/create_basket_item';
import { Update_Basket_Item } from 'src/app/contracts/basket/update_basket_item';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private httpClientService: HttpClientService) { }
  
  async get():Promise<List_Basket_Item[]>{
    const observable: Observable<List_Basket_Item[]> = this.httpClientService.get<List_Basket_Item[]>({
      controller: "baskets"
    }) 
    return await firstValueFrom(observable);
  }

  async add(bascetItem: Create_Basket_Item): Promise<void>{
    const observable: Observable<any> = this.httpClientService.post({
      controller: "baskets"
    }, bascetItem);

    await firstValueFrom(observable);
  }

  async updateQuantity(basketItem: Update_Basket_Item): Promise<void>{
    const observable: Observable<any> = this.httpClientService.put({
      controller: "baskets"
    }, basketItem);

    await firstValueFrom(observable);
  }
  async remove(basketItemId: string) : Promise<void>{
    const observable: Observable<any> = this.httpClientService.delete({
      controller: "baskets"
    }, basketItemId);
    await firstValueFrom(observable);
  }
}
