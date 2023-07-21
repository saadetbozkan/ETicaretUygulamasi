import { UsersComponent } from './users.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { DeleteDirectiveModule } from 'src/app/directives/admin/delete.directive.module';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [
    UsersComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    MatTableModule, MatPaginatorModule, DeleteDirectiveModule, MatButtonModule,
    RouterModule.forChild([
      {path:"", component: UsersComponent}
    ])
  ]
})
export class UsersModule { }
