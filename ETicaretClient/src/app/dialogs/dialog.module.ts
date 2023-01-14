import { FileUploadModule } from 'src/app/services/common/file-upload/file-upload.module';
import { MatButtonModule } from '@angular/material/button';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';


@NgModule({
  declarations: [
    DeleteDialogComponent,
    SelectProductImageDialogComponent
      ],
  imports: [
    CommonModule,
    MatDialogModule, MatButtonModule,
    FileUploadModule
  ]
})
export class DialogModule { }
