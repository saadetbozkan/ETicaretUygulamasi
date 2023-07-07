import { CustomToastrService, ToasterMessageType, ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { UserService } from 'src/app/services/common/models/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss']
})
export class UpdatePasswordComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, 
    private userAuthService: UserAuthService, 
    private userService: UserService, 
    private activatedRoute: ActivatedRoute, 
    private toastrService: CustomToastrService,
    private router: Router) {
    super(spinner);
  }

  state: any = {};

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallTriangle);
    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId: string = params["userId"];
        const resetToken: string = params["resetToken"];
        this.state = await this.userAuthService.verifyResetToken(resetToken, userId, () => {
          this.hideSpinner(SpinnerType.BallTriangle);
        });
      }
    });
  }

  updatePassword(password: string, passwordConfirm: string) {

 this.showSpinner(SpinnerType.BallTriangle);
    if (password != passwordConfirm) {
      this.toastrService.message("Şifrenizi doğrulayınız.", "Şifre Doğrulama", {
        messageType: ToasterMessageType.Error,
        position: ToasterPosition.TopRight
      });
      this.hideSpinner(SpinnerType.BallTriangle);
      return;
    }

    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId: string = params["userId"];
        const resetToken: string = params["resetToken"];
        await this.userService.updatePassword(userId, resetToken, password, passwordConfirm, 
          () => {
            this.toastrService.message("Şifre başarıyla değiştirilmiştir.", "Şifre Değiştirildi.", {
              messageType: ToasterMessageType.Success,
              position: ToasterPosition.TopRight
            });
            this.router.navigate(["/login"]);
          },
          error => {
            console.log(error);
            
          });
        this.hideSpinner(SpinnerType.BallTriangle);
      }
    });

  }
}
