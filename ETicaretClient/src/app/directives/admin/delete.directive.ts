import { AlertifyService, MessageType, Position } from './../../services/admin/alertify.service';
import { SpinnerType } from 'src/app/base/base.component';
import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { HttpErrorResponse } from '@angular/common/http';
import { DeleteDialogComponent, DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
declare var $: any;
@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(
    private element: ElementRef,
    private _renderer: Renderer2,
    private httpClientService: HttpClientService, 
    private spinner: NgxSpinnerService,
    public dialog: MatDialog,
    private alertifyService: AlertifyService, 
    private dialogService:DialogService
  ) 
  {
    const img = _renderer.createElement("img");
    img.setAttribute("src","../../../../../assets/delete.png");
    img.setAttribute("style","cursor:pointer;")
    img.width = 20;
    img.height = 20;
    _renderer.appendChild(element.nativeElement, img);
  }
  @Input() controller: string;
  @Input() id  :string;
  @Output() callback : EventEmitter<any> = new EventEmitter();
  @HostListener("click")
  async onclick() {
    this.dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      options: {width: "230px", height: "230px"},
      afterClosed:  async () =>{
      this.spinner.show(SpinnerType.JellyBox);
      const td: HTMLTableCellElement= this.element.nativeElement;
      this.httpClientService.delete({
        controller: this.controller
        },this.id).subscribe(data =>{
          $(td.parentElement).animate({
          opacity: 0,
          left: "+=50",
          height:"toogle"
          }, 700, ()=> 
          {
            this.callback.emit();
            this.alertifyService.message("Silme işlemi başarıyla gerçekleştirilmiştir.",{
              dismissOthers: true,
              messageType: MessageType.Success,
              position: Position.BottomRight
            });
          });
        }, (errorResponse: HttpErrorResponse) => {
          this.spinner.hide(SpinnerType.JellyBox)
            this.alertifyService.message("Silme işlemi yapılırken beklenmeyen bir hata ile karşılaşılmıştır.",{
              dismissOthers: true,
              messageType: MessageType.Error,
              position: Position.BottomRight
            });
        });
     }
    });     
  }

  // openDialog(afterClosed: any ): void {
  //   const dialogRef = this.dialog.open(DeleteDialogComponent, {
  //     width: '230px',
  //     height: '230px',
  //     data: DeleteState.Yes,
  //    });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if(result == DeleteState.Yes){
  //       afterClosed();
  //     }
  //   });
  //}
}