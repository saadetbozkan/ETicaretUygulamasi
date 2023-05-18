import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './../../../services/common/auth.service';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { UserService } from './../../../services/common/models/user.service';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

  constructor(private userService: UserService,
    spinner: NgxSpinnerService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router) {
    super(spinner);
  }

  ngOnInit(): void {
  }
  async login(userNameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.JellyBox);
    await this.userService.login(userNameOrEmail, password, () => {
      //this.authService.identityCheck();
      this.activatedRoute.queryParams.subscribe(params => {
        const returnUrl: string = params["returnUrl"];
        if (returnUrl)
          this.router.navigate([returnUrl]);
        else
        this.router.navigate(["admin"]);
      });
      this.hideSpinner(SpinnerType.JellyBox)
    });
  }

}