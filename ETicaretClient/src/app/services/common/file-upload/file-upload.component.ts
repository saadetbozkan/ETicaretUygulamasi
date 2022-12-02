import { DialogService } from 'src/app/services/common/dialog.service';
import { FileUploadDialogComponent, FileUploadDialogState } from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import { ToasterPosition } from '../../ui/custom-toastr.service';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { HttpClientService } from '../http-client.service';
import { Component, Input, OnInit } from '@angular/core';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { CustomToastrService, ToasterMessageType } from '../../ui/custom-toastr.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {

  constructor(private httpClientService: HttpClientService,
    private alertifyService: AlertifyService,
    private customToastrService: CustomToastrService,
    public dialog: MatDialog, private dialogService:DialogService) {

  }

  @Input() options: Partial<FileUploadOptions>;
  public files: NgxFileDropEntry[];

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();

    for(const file of files){
      const fileEntry = file.fileEntry as FileSystemFileEntry;
      fileEntry.file((_file:File)=> {
        fileData.append(_file.name,_file, file.relativePath);
      });
    }
    
    this.dialogService.openDialog({
      componentType:  FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      afterClosed: () => () =>{
        this.httpClientService.post({
          controller: this.options.controller,
          action: this.options.action,
          queryString: this.options.queryString,
          headers: new HttpHeaders({"responseType":"blob"})
        }, fileData).subscribe(data => {
    
          const message: string = "Dosyalar başarıyla yüklenmiştir.";
          if(this.options.isAdminPage){
            this.alertifyService.message(message, {
    
              dismissOthers:true,
              messageType:MessageType.Success,
              position: Position.BottomRight
            })
          } else{
            this.customToastrService.message(message,"Başarılı.",{
              messageType: ToasterMessageType.Success,
              position:ToasterPosition.BottomLeft
            });
          }
    
        },(errorResponse: HttpErrorResponse) => {
          const message: string = "Dosyalar yüklenirken beklenmeyen bir hatayla karşılaşılmıştır.";
          if(this.options.isAdminPage){
            this.alertifyService.message(message, {
    
              dismissOthers:true,
              messageType:MessageType.Error,
              position: Position.BottomRight
            })
          } else{
            this.customToastrService.message(message,"Başarısız.",{
              messageType: ToasterMessageType.Error,
              position:ToasterPosition.BottomLeft
            });
          }
    
        });
      }

    });
  }

  // openDialog(afterClosed: any ): void {
  //   const dialogRef = this.dialog.open(FileUploadDialogComponent, {
  //     width: '230px',
  //     height: '230px',
  //     data: FileUploadDialogState.Yes,
  //    });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if(result == FileUploadDialogState.Yes){
  //       afterClosed();
  //     }
  //   });
  // }
}
export class FileUploadOptions{
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}