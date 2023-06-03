import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(private toastrService: CustomToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      switch (error.status) {
        case HttpStatusCode.Unauthorized:
          this.toastrService.message("Bu işlemi yapmaya yetkiniz bulunmamaktadır.", "Yetkisiz işlem!", {
            messageType: ToasterMessageType.Warning,
            position: ToasterPosition.BottomFullWidth
          });
          break;
        case HttpStatusCode.InternalServerError:
          this.toastrService.message("Sunucuya erişilemiyor.", "Sunucu Hatası!", {
            messageType: ToasterMessageType.Warning,
            position: ToasterPosition.BottomFullWidth
          });
          break;
        case HttpStatusCode.BadRequest:
          this.toastrService.message("Geçersiz istek yapıldı.", "Geçersiz istek!", {
            messageType: ToasterMessageType.Warning,
            position: ToasterPosition.BottomFullWidth
          });
          break;
        case HttpStatusCode.NotFound:
          this.toastrService.message("Sayfa bulunamamaktadır..", "Sayfa bulunamadı!", {
            messageType: ToasterMessageType.Warning,
            position: ToasterPosition.BottomFullWidth
          });
          break;
        default:
          this.toastrService.message("Beklenmeyen bir hata meydana gelmiştir.", "Beklenmeyen Hata!", {
            messageType: ToasterMessageType.Warning,
            position: ToasterPosition.BottomFullWidth
          });
          break;
      }
      return of(error);
    }));
  }
}
