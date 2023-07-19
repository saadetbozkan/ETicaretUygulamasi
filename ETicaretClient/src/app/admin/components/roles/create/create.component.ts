import { Component, EventEmitter, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { RoleService } from 'src/app/services/common/models/role.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent{

  constructor(spinner: NgxSpinnerService, 
    private roleService: RoleService, 
    private alertify: AlertifyService) {
    super(spinner)
   }
  @Output() createdRole: EventEmitter<string> = new EventEmitter();

  create(name: HTMLInputElement){
    this.showSpinner(SpinnerType.JellyBox)

    this.roleService.create(name.value, () => {
      this.hideSpinner(SpinnerType.JellyBox);
      this.alertify.message("Rol başarıyla eklenmiştir.",{
        messageType : MessageType.Success,
        position: Position.BottomLeft,
        dismissOthers: true
      });
      this.createdRole.emit(name.value);
    }, errorMessage => { 
      this.hideSpinner(SpinnerType.JellyBox);
      this.alertify.message("Bir hata oluştu.",
        {
        messageType : MessageType.Error,
        position:Position.BottomLeft,
        dismissOthers: true
      });
      this.hideSpinner(SpinnerType.JellyBox);
    });
  }
}
