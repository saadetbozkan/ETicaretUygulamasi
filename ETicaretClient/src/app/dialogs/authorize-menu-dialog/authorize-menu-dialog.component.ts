import { AuthorizeEndpointService } from './../../services/common/models/authorize-endpoint.service';
import { Component, Inject, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RoleService } from 'src/app/services/common/models/role.service';
import { List_Role } from 'src/app/contracts/role/list_role';
import { MatSelectionList } from '@angular/material/list';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';

@Component({
  selector: 'app-authorize-menu-dialog',
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrls: ['./authorize-menu-dialog.component.scss']
})
export class AuthorizeMenuDialogComponent extends BaseDialog<AuthorizeMenuDialogComponent> implements OnInit{

  constructor(dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private roleService: RoleService,
    private authorizeEndpointService: AuthorizeEndpointService,
    private spinner: NgxSpinnerService) {
    super(dialogRef);
  }

  role_list: List_Role[]= [];
  allRoles: {totalRolesCount : number, datas: Map<string,string>};
  assignedRoles: string[];
  listRoles: {name: string, selected:boolean }[];

   async ngOnInit() {
    this.allRoles = await this.roleService.getRoles(-1, -1) as {totalRolesCount : number, datas: Map<string,string> };
    Object.entries(this.allRoles.datas).forEach((v)=>{
      
     this.role_list.push({
       id: v[0],
       name: v[1]
     });
    });
    
    this.assignedRoles = await this.authorizeEndpointService.getRolesToEndpoint(this.data.code, this.data.menuName);
    
    this.listRoles = this.role_list.map((r: any) => {
      return{
        name: r.name,
        selected: this.assignedRoles.indexOf(r.name)> -1
      }
    });
  }
  assignRoles(roleComponent: MatSelectionList){
    const roles: string[] = roleComponent.selectedOptions.selected.map(o => o._text.nativeElement.innerText);
    this.spinner.show(SpinnerType.JellyBox);          
    this.authorizeEndpointService.assignRoleEndoint(roles, this.data.code, this.data.menuName, () => {
    this.spinner.hide(SpinnerType.JellyBox);
       
    }, error => {
      this.spinner.hide(SpinnerType.JellyBox);

    });
  }
}

export enum AuthorizeMenuState {
  Yes,
  No
}