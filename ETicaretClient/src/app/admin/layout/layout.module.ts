import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './../../app-routing.module';
import { ComponentsModule } from './components/components.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';



@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ComponentsModule,
  ],
  exports: [
    ComponentsModule
  ]
})
export class LayoutModule { }
