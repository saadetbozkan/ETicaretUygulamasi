import { UserService } from './../../../services/common/models/user.service';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { ToasterPosition } from 'src/app/services/ui/custom-toastr.service';
import { ToasterMessageType } from 'src/app/services/ui/custom-toastr.service';
import { CustomToastrService } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private userService: UserService, private customToastrService: CustomToastrService) { }

  frm: FormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      nameSurname: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)
      ]],
      userName: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(3)
      ]],
      email: ["", [
        Validators.required,
        Validators.maxLength(250),
        Validators.email
      ]],
      password: ["", [
        Validators.required,
      ]],
      passwordConfirm: ["", [
        Validators.required,
      ]]
    }, {
      validators: (group: AbstractControl): ValidationErrors | null => {

        let password = group.get("password").value;
        let passwordConfirm = group.get("passwordConfirm").value;
        return password === passwordConfirm ? null : { notSame: true };
      }
    });
  }

  get component() {
    return this.frm.controls;
  }

  submitted: boolean = false;

  async onSubmit(user: User) {
    this.submitted = true;

    if (this.frm.invalid)
      return;
    const result: Create_User = await this.userService.create(user);
    if (result.succeeded)
      this.customToastrService.message(result.message, "Kullanıcı Kaydı Başarılı",
        {
          messageType: ToasterMessageType.Success,
          position: ToasterPosition.TopCenter
        })
    else
      this.customToastrService.message(result.message, "Hata",
        {
          messageType: ToasterMessageType.Error,
          position: ToasterPosition.TopCenter
        })
  }
}