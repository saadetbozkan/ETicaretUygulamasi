import { NgxSpinnerService } from 'ngx-spinner';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private jwtHelper: JwtHelperService, 
    private router: Router, 
    private toastrService: CustomToastrService, 
    private spinner: NgxSpinnerService){

  }
  canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.spinner.show(SpinnerType.BallTriangle);
    const token: string = localStorage.getItem("accessToken");

    let expired: boolean; 
    try{
      expired = this.jwtHelper.isTokenExpired(token);
    } catch {
      expired = true;
    }
    
    if(!token || expired ){
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
