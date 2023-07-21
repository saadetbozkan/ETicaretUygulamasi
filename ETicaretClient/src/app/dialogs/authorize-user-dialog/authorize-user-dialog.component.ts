import { AlertifyService, MessageType, Position } from './../../services/admin/alertify.service';
import { Component, Inject, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RoleService } from 'src/app/services/common/models/role.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatSelectionList } from '@angular/material/list';
import { SpinnerType } from 'src/app/base/base.component';
import { List_Role } from 'src/app/contracts/role/list_role';
import { UserService } from 'src/app/services/common/models/user.service';

@Component({
  selector: 'app-authorize-user-dialog',
  templateUrl: './authorize-user-dialog.component.html',
  styleUrls: ['./authorize-user-dialog.component.scss']
})
export class AuthorizeUserDialogComponent extends BaseDialog<AuthorizeUserDialogComponent> implements OnInit {

  constructor(dialogRef: MatDialogRef<AuthorizeUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private roleService: RoleService,
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private alertify: AlertifyService) {
    super(dialogRef);
  }

  role_list: List_Role[] = [];
  allRoles: { totalRolesCount: number, datas: Map<string, string> };
  assignedRoles: string[];
  listRoles: { name: string, selected: boolean }[];

  async ngOnInit() {
    this.spinner.show(SpinnerType.JellyBox);
    this.allRoles = await this.roleService.getRoles(-1, -1);
    Object.entries(this.allRoles.datas).forEach((v) => {

      this.role_list.push({
        id: v[0],
        name: v[1]
      });
    });

    this.assignedRoles = await this.userService.getRolesToUser(this.data);
    this.listRoles = this.role_list.map((r: any) => {
      return {
        name: r.name,
        selected: this.assignedRoles.indexOf(r.name) > -1
      }
    });
    this.spinner.hide(SpinnerType.JellyBox);
  }

  assignRoles(roleComponent: MatSelectionList) {
    const roles: string[] = roleComponent.selectedOptions.selected.map(o => o._text.nativeElement.innerText);
    this.spinner.show(SpinnerType.JellyBox);
    this.userService.assignRoleUser(this.data, roles, () => {
      this.spinner.hide(SpinnerType.JellyBox);
      this.alertify.message("Rol Atama işlemi başarıyla gerçekleştirilmiştir.", {
        dismissOthers: true,
        messageType: MessageType.Success,
        position: Position.TopRight
      });
    }, error => {
      this.spinner.hide(SpinnerType.JellyBox);
      this.alertify.message(error, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.BottomLeft
      })
    });
  }

}