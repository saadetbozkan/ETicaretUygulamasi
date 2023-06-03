import { AuthService } from './../../../services/common/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FacebookLoginProvider, SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

  constructor(private userAuthService: UserAuthService,
    spinner: NgxSpinnerService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private socialAuthService: SocialAuthService,
    private authService: AuthService) {
    super(spinner);
    this.socialAuthService.authState.subscribe(async (user: SocialUser) => {
      this.showSpinner(SpinnerType.JellyBox)
      switch (user.provider) {
        case "GOOGLE":
          await this.userAuthService.googleLogin(user, () => {
            this.checkRoute();
            this.hideSpinner(SpinnerType.JellyBox);
          });
          break;
        case "FACEBOOK":
          userAuthService.facebookLogin(user, () => {
            this.checkRoute();
            this.hideSpinner(SpinnerType.JellyBox);
          });
          break;
      }
    });
  }

  ngOnInit(): void {
  }

  facebookLogin() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  async login(userNameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.JellyBox);
    await this.userAuthService.login(userNameOrEmail, password, () => {
      this.checkRoute();
      this.hideSpinner(SpinnerType.JellyBox)
    });
  }

  checkRoute() {
    this.authService.identityCheck();
    this.activatedRoute.queryParams.subscribe(params => {
      const returnUrl: string = params["returnUrl"];

      if (returnUrl)
        this.router.navigate([returnUrl]);
      else
        this.router.navigate(["admin"]);
    });
  }

}
