import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: any, errorCallBack? : (errorMessage: string) => void
  ){
    this.httpClientService.post<Create_Product>({
      controller: "products"
    }, product)
      .subscribe({
        next : result => {
          successCallBack();
          alert("Başarılı");
        },
        error : (errorResponse: HttpErrorResponse) =>{
          const _error : Array<{key: string; value: Array<string>}> = errorResponse.error;
          let message = "";
          _error.forEach((v, index)=>{
            v.value.forEach( (_v, _index) =>{
              message += `${_v}<br>`;
            });
          });
          errorCallBack(message);
        },
        complete: () => console.info("Başarılı")
      });
  }
}
