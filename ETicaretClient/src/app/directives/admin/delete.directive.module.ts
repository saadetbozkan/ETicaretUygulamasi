import { NgModule } from '@angular/core';
import { DeleteDirective } from './delete.directive';
import { DialogModule } from '@angular/cdk/dialog';



@NgModule({
  declarations: [DeleteDirective],
  imports: [
    DialogModule
  ],
  exports: [DeleteDirective]
})
export class DeleteDirectiveModule { }
