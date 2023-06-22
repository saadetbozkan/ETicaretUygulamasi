import { Injectable } from '@angular/core';
import { Token } from 'src/app/contracts/token/token';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';
import { CustomToastrService, ToasterMessageType, ToasterPosition } from '../../ui/custom-toastr.service';
import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from '../http-client.service';
@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) { }
  
  async login(userNameOrEmail: string, password: string, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller:"auth",
      action: "login"
    }, {userNameOrEmail, password});

    const tokenResponse : TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if(tokenResponse){
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      this.toastrService.message("Kullanıcı girişi başarıyla sağlanmıştır.","Giriş başarılı",{
        messageType : ToasterMessageType.Success,
        position: ToasterPosition.TopRight
      });
    }
   
    callBackFunction();
  }
  
  async refreshTokenLogin(refreshToken: string, callBackFunction?: (state) => void): Promise<any> {
    const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: "auth",
      action: "RefreshTokenLogin"
    }, {refreshToken: refreshToken});

   try {
    const tokenResponse : TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if(tokenResponse){
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);
    }
   
    callBackFunction(tokenResponse ? true : false);
    
   } catch (error) {
    callBackFunction(false);
   }
  }

  async googleLogin(user: SocialUser, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<SocialUser | TokenResponse> = this.httpClientService.post<SocialUser | TokenResponse>({
      action: "google-login",
      controller: "auth"
    }, user);
    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      this.toastrService.message("Google üzerinden girişi başarıyla sağlanmıştır.", "Giriş başarılı", {
        messageType: ToasterMessageType.Success,
        position: ToasterPosition.TopRight
      });
    }
    callBackFunction();
  }

  async facebookLogin(user: SocialUser, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<SocialUser | TokenResponse> = this.httpClientService.post<SocialUser | TokenResponse>({
      action: "facebook-login",
      controller: "auth"
    }, user);
    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      this.toastrService.message("Facebook üzerinden girişi başarıyla sağlanmıştır.", "Giriş başarılı", {
        messageType: ToasterMessageType.Success,
        position: ToasterPosition.TopRight
      });
    }
    callBackFunction();
  }
}
