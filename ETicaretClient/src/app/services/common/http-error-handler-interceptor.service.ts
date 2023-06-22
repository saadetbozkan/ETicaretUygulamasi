import { Router } from '@angular/router';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';


@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(private toastrService: CustomToastrService, private userAuthService: UserAuthService, private router: Router,private spinner: NgxSpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      switch (error.status) {
        case HttpStatusCode.Unauthorized:
          this.userAuthService.refreshTokenLogin(localStorage.getItem("refreshToken"), (state) => {
            if(!state){ 
              const url = this.router.url;
              if(url == "/products"){
                this.toastrService.message("Bu işlemi yapmak için oturum açmanız gerekmektedir..", "Oturum Açınız!", {
                  messageType: ToasterMessageType.Warning,
                  position: ToasterPosition.TopRight
                });
              }
              else
              this.toastrService.message("Bu işlemi yapmaya yetkiniz bulunmamaktadır.", "Yetkisiz işlem!", {
                messageType: ToasterMessageType.Warning,
                position: ToasterPosition.BottomFullWidth
              });        
            }
          }).then(data =>{
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
          this.toastrService.message("Sayfa bulunamamaktadır.", "Sayfa bulunamadı!", {
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
      this.spinner.hide(SpinnerType.BallTriangle);
      this.spinner.hide(SpinnerType.JellyBox);
      this.spinner.hide(SpinnerType.ScaleMuliple);
      
      return of(error);
    }));
  }
}
