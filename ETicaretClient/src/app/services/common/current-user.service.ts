import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserService {

  constructor(private jwtHelperService:JwtHelperService) { }


  getUsername() {
    let token= localStorage.getItem('accessToken');
    const jwtData= this.jwtHelperService.decodeToken(token);
    const jwtNameKey= Object.keys(jwtData).find((key) =>{
      return key.includes("/name")?? key}
      );
    return jwtData[jwtNameKey];
  }
}
