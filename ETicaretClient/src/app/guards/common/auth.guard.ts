import { AuthService, _isAuthenticated } from './../../services/common/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, 
    private toastrService: CustomToastrService, 
    private spinner: NgxSpinnerService,
    private authService: AuthService){

  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.spinner.show(SpinnerType.BallTriangle);
    //child componentlerin authorization'ı
    this.authService.identityCheck(); 

    if(!_isAuthenticated){
      localStorage.removeItem("accessToken");
      this.router.navigate(["login"],{queryParams :{returnUrl: state.url}});
      this.toastrService.message("Oturum açmanız gerekiyor!", "Yetkisiz Erişim",{
        messageType : ToasterMessageType.Warning,
        position: ToasterPosition.TopRight
      });
    }
    this.spinner.hide(SpinnerType.BallTriangle);
    return true;
  }
}
