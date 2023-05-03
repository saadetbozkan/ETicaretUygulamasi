import { List_Product_Image } from './../../../contracts/list_product_image';
import { firstValueFrom, Observable } from 'rxjs';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product } from 'src/app/contracts/list_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack: () => void , errorCallBack? : (errorMessage: string) => void)
  {
    this.httpClientService.post<Create_Product>({
      controller: "products"
    }, product)
      .subscribe({
        next : result => {
          successCallBack();
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

  async read(page: number = 0, size: number = 5, successCallBack?: () => void , errorCallBack? : (errorMessage: string) => void): Promise<{totalCount: number; products: List_Product[]}> {
    const promiseDate: Promise<{totalCount: number; products: List_Product[]}> = this.httpClientService.get<{totalCount: number; products: List_Product[]}>({
      controller:"products",
      queryString:`page=${page}&size=${size}`
    }).toPromise();

    promiseDate.then(d => successCallBack())
    .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))

    return await promiseDate;
  }

  async delete(id: string){
    const deleteObservable: Observable<any> = this.httpClientService.delete<any>({
      controller:"products"
    },id);
    await firstValueFrom(deleteObservable);
  }
  
  async readImages(id: string, successCallBack?: () => void): Promise<List_Product_Image[]>{
    const getObservable: Observable<List_Product_Image[]> = this.httpClientService.get<List_Product_Image[]>({
      action: "getproductimages",
      controller: "products",
    },id);
    const images: List_Product_Image[] = await firstValueFrom(getObservable);
    successCallBack();
    return images;
  }

 
  async deleteImage(id: string, imageId: string, successCallBack?: () => void) {
    const deleteObservable = this.httpClientService.delete({
      action: "deleteproductimage",
      controller: "products",
      queryString: `imageId=${imageId}`
    }, id)
    await firstValueFrom(deleteObservable);
    successCallBack();
  }
}