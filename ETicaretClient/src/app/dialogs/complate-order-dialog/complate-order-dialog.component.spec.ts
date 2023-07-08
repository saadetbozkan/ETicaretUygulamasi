import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplateOrderDialogComponent } from './complate-order-dialog.component';

describe('ComplateOrderDialogComponent', () => {
  let component: ComplateOrderDialogComponent;
  let fixture: ComponentFixture<ComplateOrderDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComplateOrderDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ComplateOrderDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
