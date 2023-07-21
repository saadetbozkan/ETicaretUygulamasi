import { MatTableDataSource } from '@angular/material/table';
import { RoleService } from './../../../../services/common/models/role.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { DialogService } from 'src/app/services/common/dialog.service';
import { MatPaginator } from '@angular/material/paginator';
import { List_Role } from 'src/app/contracts/role/list_role';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService,
    private roleService: RoleService,
    private alertifyService: AlertifyService,
    private dialogService: DialogService) {
    super(spinner)
  }

  displayedColumns: string[] = ['name', 'edit', 'delete',];
  dataSource: MatTableDataSource<List_Role> = null;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getRoles() {
    this.showSpinner(SpinnerType.JellyBox);
    const allRoles: { totalRolesCount: number; datas: Map<string,string>} = await this.roleService
      .getRoles(this.paginator ? this.paginator.pageIndex : 0,
        this.paginator ? this.paginator.pageSize : 5,
        () => this.hideSpinner(SpinnerType.JellyBox),
        errorMessage => this.alertifyService.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.BottomLeft
        }));
        
    const list_role: List_Role[] = [];
    Object.entries(allRoles.datas).forEach((v)=>{
          list_role.push({
            id: v[0],
            name: v[1]
          });
         });
    this.dataSource = new MatTableDataSource<List_Role>(list_role);
    this.paginator.length = allRoles.totalRolesCount;
  }

  async pageChanged() {
    await this.getRoles()
  }

  async ngOnInit() {
    await this.getRoles();
  }
}