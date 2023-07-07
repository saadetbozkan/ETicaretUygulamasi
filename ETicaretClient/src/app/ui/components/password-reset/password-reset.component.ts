import { CustomToastrService, ToasterMessageType, ToasterPosition } from './../../../services/ui/custom-toastr.service';
import { AlertifyService } from './../../../services/admin/alertify.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { Component} from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.scss']
})
export class PasswordResetComponent extends BaseComponent {

  constructor(spinner: NgxSpinnerService, private userAuthService: UserAuthService, private toastr: CustomToastrService) { 
    super(spinner)
  }
  passwordReset(email: string){
    this.showSpinner(SpinnerType.BallTriangle)
    this.userAuthService.passwordReset(email,() => {
      this.hideSpinner(SpinnerType.BallTriangle);
      this.toastr.message("Şifre değiştirme talebiniz mailinize başarıyla gönderilmiştir.","Mail Gönderildi.",{
        messageType : ToasterMessageType.Warning,
        position:ToasterPosition.TopRight
      })
  });

  }
}
