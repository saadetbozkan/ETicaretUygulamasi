import { HttpClientService } from 'src/app/services/common/http-client.service';
import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { BaseUrl } from 'src/app/contracts/base_url';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private httpClientService: HttpClientService) { }

async getBaseStorageUrl(): Promise<BaseUrl>{
    const getObservable: Observable<BaseUrl> = this.httpClientService.get<BaseUrl>({
        controller:"files",
        action: "GetBaseStorageUrl"
    });

    return await firstValueFrom(getObservable);
  }
}