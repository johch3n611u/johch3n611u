import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceReturnDetailComponent } from './device-return-detail.component';

describe('DeviceReturnDetailComponent', () => {
  let component: DeviceReturnDetailComponent;
  let fixture: ComponentFixture<DeviceReturnDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceReturnDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceReturnDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
