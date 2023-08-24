import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import {MatTabsModule} from '@angular/material/tabs';
import {MatTreeModule} from '@angular/material/tree';
import {MatIconModule} from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatListModule} from '@angular/material/list';
import { ChartsComponent } from './charts/charts.component';
import { OrdersComponent } from './orders/orders.component';
import {MatTableModule} from '@angular/material/table';


@NgModule({
  declarations: [
    DashboardComponent,
    ChartsComponent,
    OrdersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"", component: DashboardComponent},
    ]),
    MatCardModule,
    MatTabsModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    MatExpansionModule,
    MatListModule,
    MatTableModule
  ]
})
export class DashboardModule { }
