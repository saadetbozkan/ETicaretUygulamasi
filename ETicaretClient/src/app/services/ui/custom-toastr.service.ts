import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(private toastr: ToastrService) { }

  message(message: string, title: string, toasterOptions: Partial<ToasterOptions>){
    this.toastr[toasterOptions.messageType](message,title,{
      positionClass: toasterOptions.position});
  }
}

export class ToasterOptions{
  messageType: ToasterMessageType;
  position : ToasterPosition;
}

export  enum ToasterMessageType{
  Error = "error" ,
  Info = "info" ,
  Success = "success",
  Warning = "warning"
}

export enum ToasterPosition{
  TopCenter = "toast-top-center",
  TopRight = "toast-top-right",
  TopLeft = "toast-top-left",
  BottomRight = "toast-bottom-right",
  BottomLeft = "toast-bottom-left",
  BottomCenter = "toast-bottom-center",
  TopFullWidth= "toast-top-full-width",
  BottomFullWidth= "toast-bottom-full-width",
 }
