import { Component, Inject } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { RoleService } from 'src/app/services/common/models/role.service';
import { SpinnerType } from 'src/app/base/base.component';

@Component({
  selector: 'app-role-update-dialog',
  templateUrl: './role-update-dialog.component.html',
  styleUrls: ['./role-update-dialog.component.scss']
})
export class RoleUpdateDialogComponent extends BaseDialog<RoleUpdateDialogComponent> {

  constructor(dialogRef: MatDialogRef<RoleUpdateDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService,
    private roleService: RoleService) {
    super(dialogRef);

  }

  async update(txtName: HTMLInputElement) {
    this.spinner.show(SpinnerType.JellyBox);
    let roleName: string = txtName.value != null ? txtName.value : this.data.roleName;
    await this.roleService.update(this.data.roleId, roleName, () => {

      this.alertify.message(`${this.data.roleName} role bilgisi ${roleName} olarak güncellenmişir.`,
        {
          messageType: MessageType.Success,
          position: Position.TopRight
        });
    });
    this.spinner.hide(SpinnerType.JellyBox);
    this.dialogRef.close();
  }

}
