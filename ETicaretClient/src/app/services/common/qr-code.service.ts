import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QrCodeService {

  constructor(private httpClientService: HttpClientService) { }

  async generateQRCode(productId: string, successCallBack?: () => void): Promise<any> {
    const getObservable: Observable<any> = this.httpClientService.get({
      action: "qrCode",
      controller: "products",
      responseType: "blob"
    }, productId);
    return await firstValueFrom(getObservable);
  }
}
