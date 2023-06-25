import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';
import { ListComponent } from './list/list.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table'
import { DeleteDirectiveModule } from 'src/app/directives/admin/delete.directive.module';



@NgModule({
  declarations: [
    OrdersComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule, DeleteDirectiveModule,
    RouterModule.forChild([
      {path:"", component: OrdersComponent}
    ])
  ]
})
export class OrdersModule { }
