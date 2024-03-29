import { Component, OnInit, ViewChild } from '@angular/core';
import { ListComponent } from './list/list.component';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  @ViewChild(ListComponent) listComponent: ListComponent;

  createdRole(createdRole: string){
    this.listComponent.getRoles();
  }

  opened = false;
  createRoleOpen(){
    this.opened = !this.opened;
  }

}
