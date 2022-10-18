import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Create_Product } from 'src/app/contracts/create_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: any){
    this.httpClientService.post<Create_Product>({
      controller: "products"
    }, product)
      .subscribe(reutlt =>{
        successCallBack();
    });
  }
}
